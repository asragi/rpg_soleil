﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Soleil.Event.Conversation
{
    static class ConversationRead
    {
        const string Path = "conversations.yaml";
        static List<ConversationYaml> conversations;
        static ConversationRead()
        {
            conversations = Deserialize(Path);
        }

        static List<ConversationYaml> Deserialize(string path)
        {
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            var input = new StringReader(text);
            var deserializer = new Deserializer();
            var deserializeObject = deserializer.Deserialize<List<ConversationYaml>>(input);
            return deserializeObject;
        }

        public static EventSet[] ActionFromData(string place, string conversationName, ConversationSystem cs, EventSequence es)
        {
            // 会話に登場する人物のリスト
            var _personList = new List<ConversationPerson>();
            // YAMLからデータを気合で取り出す
            var convListInPlace = conversations.Find(s => s.place == place);
            var targetConv = convListInPlace.conversations.Find(s => s.name == conversationName);
            var _events = targetConv.events;
            return CreateEventSet(_events, _personList, es);

            EventSet[] CreateEventSet(
                List<ConversationYaml.ConversationSet.YamlEvent> events,
                List<ConversationPerson> personList,
                EventSequence eventSequence)
            {
                var tmpEventSets = new List<EventBase>();
                var result = new List<EventSet>();
                string cacheName = "";
                string cacheFace = "";
                foreach (var e in events)
                {
                    if (e.eventName == "person")
                    {
                        string name = e.person;
                        int position = e.position;
                        personList.Add(new ConversationPerson(name, position));
                    }
                    if (e.eventName == "talk")
                    {
                        string target = e.person ?? cacheName;
                        var talker = personList.Find(s => s.Name == target);
                        string face = e.face ?? cacheFace;
                        string text = e.text;
                        cacheName = target;
                        cacheFace = face;
                        tmpEventSets.Add(new ConversationTalk(talker, text, face, cs));
                        continue;
                    }
                    if (e.eventName == "branch")
                    {
                        var boolSet = GlobalBoolSet.GetBoolSet(BoolObject.Global, (int)GlobalBoolKey.size);
                        var key = (GlobalBoolKey)Enum.Parse(typeof(GlobalBoolKey), e.boolKey);
                        Func<bool> func = () => boolSet[(int)key];

                        // EventSetの作成を終了し，分岐用EventSetを追加．新しいEventSetの作成を始める．
                        result.Add(new EventSet(tmpEventSets.ToArray()));
                        var onTrueEvents = CreateEventSet(e.onTrue, personList, eventSequence);
                        var onFalseEvents = CreateEventSet(e.onFalse, personList, eventSequence);
                        var branchSet = new BoolEventBranch(eventSequence, func, onTrueEvents, onFalseEvents);
                        result.Add(branchSet);
                        tmpEventSets = new List<EventBase>();
                    }
                    if (e.eventName == "setbool")
                    {
                        var boolSet = GlobalBoolSet.GetBoolSet(BoolObject.Global, (int)GlobalBoolKey.size);
                        var key = (int)Enum.Parse(typeof(GlobalBoolKey), e.boolKey);
                        tmpEventSets.Add(new BoolSetEvent(boolSet, key, e.flag));
                    }
                    if (e.eventName == "select")
                    {
                        // 選択肢表示ウィンドウを生成．
                        tmpEventSets.Add(new ConversationSelect(e.options));
                        // EventSetの作成を一旦終了し結果に追加．
                        result.Add(new EventSet(tmpEventSets.ToArray()));
                        // 分岐先のイベントを生成する．
                        var eventSets = new EventSet[e.options.Length][];
                        for (int i = 0; i < e.options.Length; i++)
                        {
                            eventSets[i] = CreateEventSet(e.events[i], personList, eventSequence);
                        }
                        Func<int> func = () => WindowManager.GetInstance().GetDecideIndex();
                        result.Add(new NumEventBranch(eventSequence, func, eventSets.ToArray()));
                        tmpEventSets = new List<EventBase>();
                    }
                }
                // 余ったイベントを末尾に追加
                if (tmpEventSets.Count > 0) result.Add(new EventSet(tmpEventSets.ToArray()));
                // キャラクターを一律に生み出す処理を先頭に追加
                result.Insert(0, new EventSet(new ConversationPersonSet(personList, cs)));
                return result.ToArray();
            }
        }

        class ConversationYaml
        {
            public string place { get; set; }
            [YamlMember(Alias = "conversations")]
            public List<ConversationSet> conversations { get; set; }
            public class ConversationSet
            {
                public string name { get; set; }
                [YamlMember(Alias = "events")]
                public List<YamlEvent> events { get; set; }

                public class YamlEvent
                {
                    [YamlMember(Alias = "event")]
                    public string eventName { get; set; }
                    public string person { get; set; }
                    public int position { get; set; }
                    public string face { get; set; }
                    public string text { get; set; }
                    [YamlMember(Alias = "bool-key")]
                    public string boolKey { get; set; }
                    public bool flag { get; set; }
                    public List<YamlEvent> onTrue { get; set; }
                    public List<YamlEvent> onFalse { get; set; }

                    // 選択肢ウィンドウのイベント
                    public string[] options { get; set; }
                    public List<YamlEvent>[] events { get; set; }
                }
            }
        }
    }
}
