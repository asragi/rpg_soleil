using Soleil.Misc;
using Soleil.Map;
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
        private static SaveData data = new SaveData();
        //#ObjectManagerを持っとくのは御法度だったりしないだろうか...?
        private static ObjectManager objManager;
        public static void Save(PersonParty party)
        {
            var player = objManager.GetPlayer();
            data.SetDatas(party,player.GetPosition(), player.Direction);
            string path = FilePath;
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }
        }

        /// <summary>
        /// MAP遷移時に呼び出す
        /// </summary>
        public static void mapTransition(MapName newmap,ObjectManager om)
        {
            data.SetMapData(newmap);
            objManager = om;
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
            // New Game
            if (isNew) return new PersonParty();
            return data.GetParty();
        }
    }
}
