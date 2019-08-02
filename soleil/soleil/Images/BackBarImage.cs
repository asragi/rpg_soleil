using Microsoft.Xna.Framework;
using Soleil.Menu;
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
    class BackBarImage: MenuComponent
    {
        // 画像の端からの切り出し量
        public const int EdgeSize = 36;
        public Vector Pos { get; set; }
        Image[] images;

        public BackBarImage(Vector _pos, Vector posDiff, int _length, bool centerBased, DepthID depth = DepthID.MenuBottom)
        {
            Pos = _pos;
            images = new Image[3];
            var texID = TextureID.BackBar;
            var tex = Resources.GetTexture(texID);
            // 位置設定
            var vecs = new Vector[3];
            vecs[0] = Pos;
            vecs[1] = Pos + new Vector(EdgeSize, 0);
            vecs[2] = Pos + new Vector(EdgeSize + (_length - 2 * EdgeSize), 0);

            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new Image(texID, vecs[i], posDiff, depth);
            }
            // 画像切り出し設定
            images[0].Rectangle = new Rectangle(0, 0, EdgeSize, tex.Height);
            images[1].Rectangle = new Rectangle(EdgeSize, 0, tex.Width - 2 * EdgeSize, tex.Height);
            images[2].Rectangle = new Rectangle(tex.Width - EdgeSize, 0, EdgeSize, tex.Height);

            // 拡大率設定
            var size = (_length - 2 * EdgeSize) / (float)(tex.Width - 2 * EdgeSize);
            images[1].Size = new Vector(size, 1);

            AddComponents(images);
        }
    }
}
