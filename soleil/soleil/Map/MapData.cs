using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    /// <summary>
    /// Mapのデータをまとめるクラス. SetMapFlagを呼び出してFlagの設定をすること.
    /// </summary>
    class MapData
    {
        MapName mapName;
        bool[,] flags;
        Texture2D tex;
        Texture2D flagTex;
        public MapData(MapName _name)
        {
            mapName = _name;
            SetData();
        }


        /// <summary>
        /// 通行可・不可のフラグ設定. 1F程度処理にかかるので呼び出しタイミングを管理するため分けて処理.
        /// </summary>
        public void SetMapFlag()
        {
            // Debug
            flags = CSVIO.GetMapData("somnia2", 1505, 1058);
        }

        void SetData()
        {
            switch (mapName)
            {
                case MapName.test:
                    tex = Resources.GetTexture(TextureID.White);
                    flagTex = Resources.GetTexture(TextureID.White);
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

        public void Update()
        {

        }

        public void Draw(Drawing sb)
        {

        }
    }
}
