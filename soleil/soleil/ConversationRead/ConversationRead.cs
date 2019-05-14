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
        
        public static EventSet[] ActionFromData(string place, string conversationName, ConversationSystem cs)
        {
            // 会話に登場する人物のリスト
            var _personList = new List<ConversationPerson>();
            // YAMLからデータを気合で取り出す
            var convListInPlace = conversations.Find(s => s.place == place);
            var targetConv = convListInPlace.conversations.Find(s => s.name == conversationName);
            var _events = targetConv.events;
            return CreateEventSet(_events, _personList);

            EventSet[] CreateEventSet(
                List<ConversationYaml.ConversationSet.YamlEvent> events,
                List<ConversationPerson> personList)
            {
                var tmpEventSets = new List<EventBase>();
                var result = new List<EventSet>();
                foreach (var e in events)
                {
                    if (e.eventName == "person")
                    {
                        string name = e.person;
                        // int position = e.position;
                        personList.Add(new ConversationPerson(name));
                    }
                    if (e.eventName == "talk")
                    {
                        var talker = personList.Find(s => s.Name == e.person);
                        string face = e.face;
                        string text = e.text;
                        tmpEventSets.Add(new ConversationTalk(talker, text, face, cs));
                        continue;
                    }
                    if (e.eventName == "branch")
                    {
                        var boolSet = GlobalBoolSet.GetBoolSet(BoolObject.Global, GlobalBoolSet.GlobalBoolSize);
                        var key = (GlobalBoolKey)Enum.Parse(typeof(GlobalBoolKey), e.boolKey);
                        Func<bool> func = () => boolSet[(int)key];

                        // EventSetの作成を終了し，分岐用EventSetを追加．新しいEventSetの作成を始める．
                        result.Add(new EventSet(tmpEventSets.ToArray()));
                        var onTrueEvents = CreateEventSet(e.onTrue, personList);
                        var onFalseEvents = CreateEventSet(e.onFalse, personList);
                        var branchSet = new BoolEventBranch(null, func, null, null);
                        tmpEventSets = new List<EventBase>();
                    }
                }
                if (tmpEventSets.Count > 0) result.Add(new EventSet(tmpEventSets.ToArray()));
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
                    public string face { get; set; }
                    public string text { get; set; }
                    [YamlMember(Alias = "bool-key")]
                    public string boolKey { get; set; }
                    public List<YamlEvent> onTrue { get; set; }
                    public List<YamlEvent> onFalse { get; set; }
                }
            }
        }
    }
}
