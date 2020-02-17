using Microsoft.Xna.Framework;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    using EFunc = Func<double, double, double, double, double>;
    /// <summary>
    /// 画像やテキストの表示に関する抽象クラス
    /// </summary>
    abstract class ImageBase : IComponent
    {
        /// <summary>
        /// イージングの基準点となる座標．
        /// </summary>
        public virtual Vector InitPos { get; set; }
        protected DepthID DepthID { get; set; }
        protected int Frame { get; private set; }
        public Color Color { get; set; } = Color.White;
        private Vector pos;
        public bool IsVisible { get; set; } = true;
        public virtual Vector Pos
        {
            get => pos;
            set
            {
                Vector diff = value - Pos;
                startPos += diff;
                targetPos += diff;
                pos = value;
            }
        }
        public float Angle { get; set; }
        protected bool IsStatic;
        public abstract Vector ImageSize { get; }
        private float alpha;
        public float Alpha {
            get => alpha;
            set {
                alpha = value;
                // イージング処理中であればイージング後の透明度も合わせる．
                alphaTarget = value;
            }
        }

        // 透明度に関するイージング処理に用いる変数．
        private float alphaStart;
        private float alphaTarget;
        private int alphaFrame;
        private int alphaDuration;
        private EFunc alphaEaseFunc;
        public int FadeSpeed { get; set; } = MenuSystem.FadeSpeed;

        /// <summary>
        /// イージングアニメーションのウェイト量
        /// </summary>
        public virtual int FrameWait { private get; set; } = 0;
        private int alphaFrameWait;
        private int moveFrameWait;

        // 位置に関するイージング処理に用いる変数．
        private Vector targetPos;
        private Vector startPos;
        private int easeFrame;
        private int easeDuration;
        private EFunc easeFunc;

        /// <summary>
        /// イージングの位置の移動量
        /// </summary>
        protected readonly Vector PosDiff;

        public ImageBase(Vector pos, DepthID id, bool centerOrigin = true, bool isStatic = true, float alpha = 0)
            : this(pos, Vector.Zero, id, centerOrigin, isStatic, alpha) { }

        public ImageBase(Vector _pos, Vector posDiff, DepthID _depthID, bool centerOrigin = true, bool isStatic = true, float alpha = 0)
        {
            pos = _pos + posDiff;
            InitPos = _pos;
            PosDiff = posDiff;
            DepthID = _depthID;
            Frame = 0;
            Alpha = alpha;
            IsStatic = isStatic;
        }

        public void Fade(int duration, EFunc _easeFunc, bool isFadeIn)
        {
            alphaStart = alpha;
            alphaFrame = 0;
            alphaFrameWait = FrameWait;
            alphaDuration = duration;
            alphaEaseFunc = _easeFunc;
            alphaTarget = isFadeIn ? 1 : 0;
        }

        public void MoveTo(Vector target, int duration, EFunc _easeFunc)
        {
            targetPos = target;
            startPos = Pos;
            easeFrame = 0;
            moveFrameWait = FrameWait;
            easeDuration = duration;
            easeFunc = _easeFunc;
        }

        public virtual void Update()
        {
            Easing();
            FadeUpdate();
            Frame++;
        }

        public abstract void Draw(Drawing d);

        public virtual void Call() => Call(true);
        public virtual void Quit() => Quit(true);

        public void Call(bool move = true, bool alpha = true)
        {
            if (alpha) Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            if (move) MoveToDefault();
        }

        public void MoveToDefault() => MoveTo(InitPos, FadeSpeed, MenuSystem.EaseFunc);


        public void Quit(bool move = true, bool alpha = true)
        {
            if (alpha) Fade(FadeSpeed, MenuSystem.EaseFunc, false);
            if (move) MoveToBack();
        }

        public void MoveToBack() => MoveTo(InitPos + PosDiff, FadeSpeed, MenuSystem.EaseFunc);

        private void Easing()
        {
            if (moveFrameWait > 0)
            {
                moveFrameWait--;
                return;
            }
            if (easeFrame >= easeDuration) return;
            if (easeFunc == null) return;
            var x = easeFunc(easeFrame, easeDuration, targetPos.X, startPos.X);
            var y = easeFunc(easeFrame, easeDuration, targetPos.Y, startPos.Y);
            pos = new Vector(x, y);
            easeFrame++;

            if (easeFrame >= easeDuration) pos = targetPos;
        }

        private void FadeUpdate()
        {
            if (alphaFrameWait > 0)
            {
                alphaFrameWait--;
                return;
            }
            if (alphaFrame >= alphaDuration) return;
            if (alphaEaseFunc == null) return;
            alpha = (float)alphaEaseFunc(alphaFrame, alphaDuration, alphaTarget, alphaStart);
            alphaFrame++;

            if (alphaFrame >= alphaDuration) alpha = alphaTarget;
        }
    }
}
