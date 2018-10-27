using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Images
{
    /// <summary>
    /// 長さの変わる文字背景用の汎用Image．
    /// </summary>
    class BackBarImage
    {
        public Vector Pos { get; set; }
        Image[] images;

        public BackBarImage(Vector _pos, int _length, bool centerBased)
        {
            Pos = _pos;
            images = new[]
            {
                new Image(0, Resources.GetTexture(TextureID.BackBar), Pos, DepthID.MessageBack, false, true, 0),
            };
        }

        public void Update()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Update();
            }
        }

        public void Draw(Drawing d)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Draw(d);
            }
        }
    }
}
