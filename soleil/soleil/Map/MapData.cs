using Microsoft.Xna.Framework.Graphics;

namespace Soleil.Map
{
    /// <summary>
    /// Mapのデータをまとめるクラス. SetMapFlagを呼び出してFlagの設定をすること.
    /// </summary>
    class MapData
    {
        public MapName mapName { private set; get; }
        bool[,] flags;
        public MapData(MapName _name)
        {
            mapName = _name;
        }


        /// <summary>
        /// 通行可・不可のフラグ設定. 1F程度処理にかかるので呼び出しタイミングを管理するため分けて処理.
        /// </summary>
        public void SetMapFlag()
        {
            flags = GetFlag(mapName);
            bool[,] GetFlag(MapName name)
            {
                switch (name)
                {
                    case MapName.Flare1:
                        return CSVIO.GetMapData("flare1", 8090, 2895);
                    case MapName.Somnia1:
                        return CSVIO.GetMapData("somnia1", 1881, 1323);
                    case MapName.Somnia2:
                        return CSVIO.GetMapData("somnia2", 1054, 741);
                    case MapName.Somnia4:
                        return CSVIO.GetMapData("somnia4", 960, 540);
                    case MapName.MagistolRoom:
                        return CSVIO.GetMapData("magistol1", 960, 540);
                    case MapName.MagistolCol1:
                        return CSVIO.GetMapData("magistol2", 1500, 1500);
                    case MapName.MagistolShop:
                        return CSVIO.GetMapData("magistol3", 1000, 1000);
                    case MapName.MagistolCol3:
                        return CSVIO.GetMapData("magistol4", 2000, 1777);
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        public int GetFlagLengthX() => flags.GetLength(0);
        public int GetFlagLengthY() => flags.GetLength(1);

        public bool GetFlagData(int x, int y)
        {
            return flags[x, y];
        }

    }
}
