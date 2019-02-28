using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// 動きなど機能を与えたSpriteの基底クラス
    /// </summary>
    [Obsolete(nameof(UIImage) + "を使おう．")]
    class Image : ImageBase
    {
        Texture2D tex;
        bool origin;
        public int Id { get; private set; }
        public Rectangle Rectangle { get; set; }
        public Vector Size { get; set; } = Vector.One;
        public override Vector GetSize => new Vector(tex.Width,tex.Height);


        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public Image(int id, Texture2D tex, Vector pos,DepthID depth,bool centerOrigin = true,bool isStatic = true, float alpha = 1)
            :base(pos, depth, centerOrigin, isStatic, alpha)
        {
            Id = id;
            this.tex = tex;
            Rectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = centerOrigin;
        }

        public override void Draw(Drawing d)
        {
            var tmp = d.CenterBased;
            d.CenterBased = origin;
            if (IsStatic) d.DrawUI(Pos, tex, Rectangle, Color.White, DepthID, Size, Alpha, Angle);
            else d.DrawWithColor(Pos, tex, Rectangle, DepthID, Color.White * Alpha, Size, Angle);
            d.CenterBased = tmp;
        }
    }
}
