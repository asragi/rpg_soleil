using Soleil.Event;
using Soleil.Event.Conversation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.ConversationRead
{
    class ConversationRead
    {
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
                    => new ConversationTalk();

                ConversationChangeFace ChangeFace(string line, string target, ConversationPerson person)
                    => new ConversationChangeFace();

                ConversationActivate Activate(string line, string target, ConversationPerson person) 
                    => new ConversationActivate();
            }
        }
    }
}
