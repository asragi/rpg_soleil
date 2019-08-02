using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// SaveやLoadを行うクラス．
    /// </summary>
    static class SaveLoad
    {
        private static readonly string FilePath = "./savedata";
        private static SaveData data;

        public static void Save(PersonParty party)
        {
            var save = new Misc.SaveData(party);
            string path = FilePath;
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, save);
            }
        }

        public static void Load()
        {
            // 読み込み
            string path = FilePath;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bf = new BinaryFormatter();
                data = (SaveData)bf.Deserialize(fs);
            }
        }

        // 以下Load用
        public static PersonParty GetParty()
        {
            throw new NotImplementedException();
        }
    }
}
