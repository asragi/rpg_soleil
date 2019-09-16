using Soleil.Misc;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Soleil
{
    /// <summary>
    /// SaveやLoadを行うクラス．
    /// </summary>
    static class SaveLoad
    {
        private static readonly string FilePath = "./savedata";
        public static SaveRefs SaveRefs { get; private set; }
        private static SaveData data;

        static SaveLoad()
        {
            SaveRefs = new SaveRefs();
        }

        public static void Save()
        {
            SaveData save = new SaveData(SaveRefs);
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

        public static bool FileExist()
        {
            return File.Exists(FilePath);
        }

        // 以下Load用
        public static PersonParty GetParty(bool isNew)
        {
            PersonParty party;
            // New Game
            if (isNew) party = new PersonParty();
            else party = data.GetParty();
            SaveRefs.Party = party;
            return party;
        }
    }
}
