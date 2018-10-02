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
    class Image : ImageBase
    {
        Texture2D tex;
        Vector origin;
        public int Id { get; private set; }

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public Image(int id, Texture2D tex, Vector pos,DepthID depth,bool centerOrigin = true,bool isStatic = true, float alpha = 1)
            :base(pos, depth, centerOrigin, isStatic, alpha)
        {
            Id = id;
            this.tex = tex;
            origin = (centerOrigin) ? Vector.Zero : new Vector(tex.Width, tex.Height) / 2;
        }

        public override void Draw(Drawing d)
        {
            if (IsStatic) d.DrawUI(Pos + origin, tex, DepthID, 1, Alpha, Angle);
            else d.DrawWithColor(Pos + origin, tex, DepthID, Color.White * Alpha, 1, Angle);
        }
    }
}
