using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    using EFunc = Func<double, double, double, double, double>;
    abstract class ImageBase
    {
        protected DepthID DepthID;
        protected int Frame;
        private Vector pos;
        public virtual Vector Pos {
            get => pos;
            set {
                Vector diff = value - Pos;
                startPos += diff;
                targetPos += diff;
                pos = value;
            }
        }
        public float Angle { get; set; }
        protected bool IsStatic;
        public bool IsDead { get; set; }
        public abstract Vector ImageSize { get; }
        // Alpha
        public float Alpha { get; set; }
        private int alphaFrame;
        private int alphaDuration;
        private EFunc alphaEaseFunc;
        bool fadeIn;

        // Easing
        public virtual int FrameWait { private get; set; } = 0;
        private int alphaFrameWait;
        private int moveFrameWait;
        private Vector targetPos;
        private Vector startPos;
        private int easeFrame;
        private int easeDuration;
        private EFunc easeFunc;

        public ImageBase(Vector pos, DepthID _depthID, bool centerOrigin = true, bool isStatic = true, float alpha = 1)
        {
            Pos = pos;
            DepthID = _depthID;
            Frame = 0;
            Alpha = alpha;
            IsStatic = isStatic;
        }

        public void Fade(int duration, EFunc _easeFunc, bool isFadeIn)
        {
            Alpha = (isFadeIn) ? 0 : 1;
            alphaFrame = 0;
            alphaFrameWait = FrameWait;
            alphaDuration = duration;
            alphaEaseFunc = _easeFunc;
            fadeIn = isFadeIn;
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
            Alpha = fadeIn ? (float)alphaEaseFunc(alphaFrame, alphaDuration, 1, 0) : (float)alphaEaseFunc(alphaFrame, alphaDuration, 0, 1);
            alphaFrame++;

            if (alphaFrame >= alphaDuration) Alpha = fadeIn ? 1 : 0;
        }
    }
}
