using Microsoft.Xna.Framework.Graphics;

namespace Soleil.Map
{
    /// <summary>
    /// Mapのデータをまとめるクラス. SetMapFlagを呼び出してFlagの設定をすること.
    /// </summary>
    class MapData
    {
        MapName mapName;
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
            switch (mapName)
            {
                case MapName.Flare1:
                    flags = CSVIO.GetMapData("flare1", 8090, 2895);
                    break;
                case MapName.Somnia1:
                    flags = CSVIO.GetMapData("somnia1", 1881, 1323);
                    break;
                case MapName.Somnia2:
                    flags = CSVIO.GetMapData("somnia2", 1054, 741);
                    break;
                case MapName.Somnia4:
                    flags = CSVIO.GetMapData("somnia4", 960, 540);
                    break;
                case MapName.MagistolRoom:
                    flags = CSVIO.GetMapData("magistol1", 960, 540);
                    break;
                case MapName.MagistolCol1:
                    flags = CSVIO.GetMapData("magistol2", 1500, 1500);
                    break;
                case MapName.MagistolShop:
                    flags = CSVIO.GetMapData("magistol3", 1000, 1000);
                    break;
                default:
                    break;
            }
        }

        public int GetFlagLengthX() => flags.GetLength(0);
        public int GetFlagLengthY() => flags.GetLength(1);

        public bool GetFlagData(int x, int y)
        {
            return flags[x,y];
        }

    }
}
