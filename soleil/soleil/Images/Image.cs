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
    class Image
    {
        Texture2D tex;
        DepthID depth;
        Vector origin;
        protected int frame;
        protected bool isStatic;
        protected float angle;
        public int Id { get; private set; }
        public bool IsDead { get; set; }
        public Vector Pos { get; set; }

        // Easing移動実現用位置保持変数
        private Vector targetPos;
        private Vector startPos;
        private int easeFrame;
        private int easeDuration;
        private Func<double, double, double, double, double> easeFunc;

        // Alpha
        public float Alpha { get; set; }
        private int alphaFrame;
        private int alphaDuration;
        private Func<double, double, double, double, double> alphaEaseFunc;
        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public Image(int id, Texture2D tex, Vector pos,DepthID depth,bool centerOrigin = true,bool isStatic = true)
        {
            Id = id;
            this.tex = tex;
            this.depth = depth;
            Pos =pos;
            targetPos = Pos;
            startPos = Pos;
            frame = 0;
            Alpha = 1;
            origin = (centerOrigin) ? new Vector(tex.Width, tex.Height) / 2 : Vector.Zero;
            IsDead = false;
            this.isStatic = isStatic;
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            targetPos = target;
            startPos = Pos;
            easeFrame = 0;
            easeDuration = duration;
            easeFunc = _easeFunc;
        }

        public void FadeIn(int duration, Func<double, double, double, double, double> _easeFunc)
        {
            alphaFrame = 0;
            alphaDuration = duration;
            alphaEaseFunc = _easeFunc;
        }

        public virtual void Update()
        {
            Easing();
            FadeIn();
            frame++;
        }

        private void Easing()
        {
            if (easeFrame >= easeDuration) return;
            if (easeFunc == null) return;
            var x = easeFunc(easeFrame, easeDuration, targetPos.X, startPos.X);
            var y = easeFunc(easeFrame, easeDuration, targetPos.Y, startPos.Y);
            Pos = new Vector(x, y);
            easeFrame++;
        }

        private void FadeIn()
        {
            if (alphaFrame >= alphaDuration) return;
            if (alphaEaseFunc == null) return;
            Alpha = (float)alphaEaseFunc(alphaFrame, alphaDuration, 1, 0);
            alphaFrame++;
        }

        public virtual void Draw(Drawing d)
        {
            if (isStatic) d.DrawUI(Pos, tex, depth, 1,Alpha,angle);
            else d.DrawWithColor(Pos, tex, depth, Color.White * Alpha, 1, angle);
        }
    }
}
