using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCP
{
    /// <summary>
    /// 動きなど機能を与えたSpriteの基底クラス
    /// </summary>
    class Image
    {
        Texture2D tex;
        DepthID depth;
        protected Vector pos;
        Vector origin;
        protected int frame;
        protected bool isUI;
        protected float angle;
        public Image(Texture2D tex, Vector startPos,DepthID depth,bool centerOrigin = true,bool isUI = true)
        {
            this.tex = tex;
            this.depth = depth;
            pos = startPos;
            frame = 0;
            origin = (centerOrigin) ? new Vector(tex.Width, tex.Height) / 2 : Vector.Zero;
            this.isUI = isUI;
        }

        public virtual void Move()
        {
            frame++;
        }

        public virtual void Draw(Drawing d)
        {
            if (isUI) d.DrawUI(pos, tex, depth, angle: angle);
            else d.Draw(pos, tex, depth, origin, 1, angle);
        }
    }
}
