using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    static class FaceDictionary
    {
        static Dictionary<string, Person> dict;

        static FaceDictionary()
        {
            dict = new Dictionary<string, Person>();
            Set("lune", "normal", TextureID.MenuLune, dict);
            Set("lune", "smile", TextureID.MenuStatusL, dict);
            Set("sunny", "normal", TextureID.MenuSun, dict);
            void Set(string name, string faceName, TextureID id, Dictionary<string, Person> _dict)
            {
                var target = _dict.ContainsKey(name) ? _dict[name] : new Person();
                target.Add(faceName, id);
                _dict[name] = target;
            }
        }

        public static TextureID Get(string name, string faceName) => dict[name].Get(faceName);
        public static string[] GetFaces(string name) => dict[name].GetFaces();

        class Person
        {
            Dictionary<string, TextureID> faceDict;

            public Person() => faceDict = new Dictionary<string, TextureID>();

            public void Add(string faceName, TextureID id)
            {
                faceDict.Add(faceName, id);
            }

            public TextureID Get(string name) => faceDict[name];
            public string[] GetFaces() => faceDict.Keys.ToArray();
        }
    }
}
