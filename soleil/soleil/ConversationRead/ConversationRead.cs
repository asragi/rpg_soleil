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

            (ConversationPerson[], object[]) ActionFromData(string[] data){
                var personList = new List<ConversationPerson>();
                var happeningList = new List<object>();
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
                        if (line.StartsWith(target + "t:")) happeningList.Add(Talk(line));
                        if (line.StartsWith(target + "face:")) happeningList.Add(ChangeFace(line));
                        if (line.StartsWith(target + "active:")) happeningList.Add(Activate(line));
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

                object Talk(string line) { return new object(); }
                object ChangeFace(string line) { return new object(); }
                object Activate(string line) { return new object(); }
            }
        }
    }
}
