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
    class Image
    {
        Texture2D tex;
        DepthID depth;
        Vector origin;
        protected int frame;
        protected bool isUI;
        protected float angle;
        public int Id { get; private set; }
        public bool IsDead { get; set; }
        public float Alpha { get; set; }
        public Vector Pos { get; set; }

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public Image(int id, Texture2D tex, Vector startPos,DepthID depth,bool centerOrigin = true,bool isStatic = true)
        {
            Id = id;
            this.tex = tex;
            this.depth = depth;
            Pos = startPos;
            frame = 0;
            origin = (centerOrigin) ? new Vector(tex.Width, tex.Height) / 2 : Vector.Zero;
            IsDead = false;
            this.isUI = isStatic;
        }

        public virtual void Update()
        {
            frame++;
        }

        public virtual void Draw(Drawing d)
        {
            if (isUI) d.DrawUI(Pos, tex, depth, angle: angle);
            else d.Draw(Pos, tex, depth, origin, 1, angle);
        }
    }
}
