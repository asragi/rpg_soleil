using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCP
{
    //Animation設定に使う
    struct AnimationData
    {
        public AnimationID AnimationID;
        public ColorDictionaryID ColorID;
        public int Interval;
        public bool Repeat;
        public Vector Position; //プレイヤーの座標とのずれ
        public DepthID DepthID;

        public AnimationData(AnimationID animationID, bool repeat = false, int interval = 4, DepthID depthID = DepthID.Player)
        {
            AnimationID = animationID;
            ColorID = ColorDictionaryID.Default;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = new Vector(0, 0);
        }

        public AnimationData(AnimationID animationID, ColorDictionaryID colorID = ColorDictionaryID.Default, bool repeat = false, int interval = 4, DepthID depthID = DepthID.Player)
        {
            AnimationID = animationID;
            ColorID = colorID;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = new Vector(0, 0);
        }

        public AnimationData(AnimationID animationID, Vector position, bool repeat = false, int interval = 4, DepthID depthID = DepthID.Player)
        {
            AnimationID = animationID;
            ColorID = ColorDictionaryID.Default;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = position;
        }

        public AnimationData(AnimationID animationID, ColorDictionaryID colorID, Vector position, bool repeat = false, int interval = 4, DepthID depthID = DepthID.Player)
        {
            AnimationID = animationID;
            ColorID = colorID;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = position;
        }
    }

    struct EffectAnimationData
    {
        public EffectAnimationID AnimationID;
        public int Interval;
        public bool Repeat;
        public Vector Position; //プレイヤーの座標とのずれ
        public DepthID DepthID;

        public EffectAnimationData(EffectAnimationID animationID, bool repeat = false, int interval = 4, DepthID depthID = DepthID.Effect)
        {
            AnimationID = animationID;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = new Vector(0, 0);
        }

        public EffectAnimationData(EffectAnimationID animationID, Vector position, bool repeat = false, int interval = 2, DepthID depthID = DepthID.Effect)
        {
            AnimationID = animationID;
            Interval = interval;
            Repeat = repeat;
            DepthID = depthID;
            Position = position;
        }
    }
}
