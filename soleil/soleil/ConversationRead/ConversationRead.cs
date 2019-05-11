using Soleil.Event;
using Soleil.Event.Conversation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Soleil.ConversationRead
{
    static class ConversationRead
    {
        const string Path = "conversations.yaml";
        static List<ConversationYaml> conversations;
        public static void YamlRead()
        {
            conversations = Deserialize(Path);
        }

        class ConversationYaml
        {
            public string place { get; set; }
            [YamlMember(Alias = "conversations")]
            public List<ConversationSet> conversations { get; set; }
            public class ConversationSet
            {
                public string name { get; set; }
                [YamlMember(Alias="events")]
                public List<YamlEvent> events { get; set; }

                public class YamlEvent
                {
                    [YamlMember(Alias = "event")]
                    public string eventName { get; set; }
                    public string person { get; set; }
                    public string face { get; set; }
                    public string text { get; set; }
                    [YamlMember(Alias = "branch-key")]
                    public string branchKey { get; set; }
                    public List<YamlEvent> onTrue { get; set; }
                    public List<YamlEvent> onFalse { get; set; }
                }
            }
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
        
        public static EventBase[] ActionFromData(string place, string conversationName)
        {
            var convListInPlace = conversations.Find(s => s.place == place);
            var targetConv = convListInPlace.conversations.Find(s => s.name == conversationName);
            var events = targetConv.events;
            var result = new List<EventBase>();
            foreach (var e in events)
            {
                if (e.eventName == "talk")
                {
                    // result.Add(new ConversationTalk());
                    continue;
                }
                if (e.eventName == "branch") continue;
            }
            return result.ToArray();
        }

        /*
        public ConversationRead(string path)
        {
            var _data = File.ReadAllLines(path);
            ActionFromData(_data);

            (ConversationPerson[], EventBase[]) ActionFromData(string[] data){
                var personList = new List<ConversationPerson>();
                var happeningList = new List<EventBase>();
                var eventsToPerson = new Dictionary<string, Func<string, string, ConversationPerson, EventBase>>()
                {
                    { "t:", Talk }, {"face:", ChangeFace}, {"active:", Activate}
                };
                for (int i = 0; i < data.Length; i++)
                {
                    var line = data[i];
                    if (line.Length < 1) continue;
                    if (line.StartsWith("person:")) personList.Add(CreatePersonFromLine(line));
                    // Person List にある名前で判定
                    for (int j = 0; j < personList.Count; j++)
                    {
                        string target = personList[i].ToString() + ":";
                        if (!line.StartsWith(target)) continue;

                        foreach (var key in eventsToPerson.Keys)
                        {
                            if (line.StartsWith(target + key))
                                happeningList.Add(eventsToPerson[key](line, target + key, personList[i]));
                        }
                    }
                }
                return (personList.ToArray(), happeningList.ToArray());

                ConversationPerson CreatePersonFromLine(string _line)
                {
                    var dataList = _line.Split(' ');
                    var name = dataList[1];
                    var position = int.Parse(dataList[2]);
                    return new ConversationPerson(name, position);
                }

                ConversationTalk Talk(string line, string target, ConversationPerson person)
                    => new ConversationTalk(person, line.Remove(0, target.Length));

                ConversationChangeFace ChangeFace(string line, string target, ConversationPerson person)
                    => new ConversationChangeFace(person, line.Remove(0, target.Length));

                ConversationActivate Activate(string line, string target, ConversationPerson person) 
                    => new ConversationActivate(person, line.Remove(0, target.Length) == "1");
            }
            
        }
        */
    }
}
