using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
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
            SetFlag();
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

        /// <summary>
        /// 通行可・不可のフラグ設定
        /// </summary>
        void SetFlag()
        {
            flags = new bool[tex.Width, tex.Height];
            Color[] tmp = new Color[tex.Width * tex.Height];
            tex.GetData(tmp);
            for (int i = 0; i < tex.Width*tex.Height; i++)
            {
                flags[i % tex.Width, i / tex.Width]
                    = tmp[i].R < 128;
            }
        }


        public bool GetData(int x, int y)
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
