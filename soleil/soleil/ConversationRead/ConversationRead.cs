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

            (object[], object[]) ActionFromData(string[] data){
                var personList = new List<object>();
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

                object CreatePersonFromLine(string _line)
                {
                    return new object();
                }

                object Talk(string line) { return new object(); }
                object ChangeFace(string line) { return new object(); }
                object Activate(string line) { return new object(); }
            }
        }
    }
}
