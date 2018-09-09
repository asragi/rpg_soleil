using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// 動きなど機能を与えたFontSpriteの基底クラス
    /// </summary>
    class FontImage
    {
        FontID font;
        DepthID depth;
        protected int frame;
        protected bool isStatic;
        public float Angle { get; set; }
        public bool IsDead { get; set; }
        public Vector Pos { get; set; }
        public String Text { get; set; }
        public Color Color { get; set; }
        public bool EnableShadow { get; set; }
        public Color ShadowColor { get; set; }
        public Vector ShadowPos { get; set; }

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
        bool fadeIn;
        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public FontImage(FontID fontID, Vector pos, DepthID depth, bool isStatic = true, float alpha = 1)
        {
            font = fontID;
            this.depth = depth;
            Pos = pos;
            targetPos = Pos;
            startPos = Pos;
            frame = 0;
            Alpha = alpha;
            IsDead = false;
            this.isStatic = isStatic;

            EnableShadow = false;
            ShadowColor = Color.Black;
            ShadowPos = Vector.Zero;
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            targetPos = target;
            startPos = Pos;
            easeFrame = 0;
            easeDuration = duration;
            easeFunc = _easeFunc;
        }

        public void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            Alpha = (isFadeIn) ? 0 : 1;
            alphaFrame = 0;
            alphaDuration = duration;
            alphaEaseFunc = _easeFunc;
            fadeIn = isFadeIn;
        }

        public virtual void Update()
        {
            Easing();
            Fade();
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

        private void Fade()
        {
            if (alphaFrame >= alphaDuration) return;
            if (alphaEaseFunc == null) return;
            Alpha = fadeIn ? (float)alphaEaseFunc(alphaFrame, alphaDuration, 1, 0) : (float)alphaEaseFunc(alphaFrame, alphaDuration, 0, 1);
            alphaFrame++;
        }

        public virtual void Draw(Drawing d)
        {
            d.DrawStaticText(Pos, Resources.GetFont(font), Text, Color * Alpha, depth, Vector2.One, Angle, false);
            if(EnableShadow) d.DrawStaticText(Pos + ShadowPos, Resources.GetFont(font), Text, ShadowColor * Alpha, depth, Vector2.One, Angle, false);
        }
    }
}
