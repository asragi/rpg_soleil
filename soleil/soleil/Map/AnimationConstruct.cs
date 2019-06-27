using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// マップの固定オブジェクトのうち，animationするもの．Fadeで切り替わり，ループする．
    /// </summary>
    class FadeAnimationConstruct : MapConstruct
    {
        UIImage[] images;
        int frame, interval;
        int nowNum, nextNum;
        public FadeAnimationConstruct(Vector _pos, TextureID[] textureIDs, int _interval, MapDepth depth, ObjectManager om)
            : base(_pos, TextureID.White, depth, om)
        {
            interval = _interval;
            nowNum = 0;
            nextNum = 1;
            images = new UIImage[textureIDs.Length];
            for (int i = 0; i < textureIDs.Length; i++)
            {
                images[i] = new UIImage(textureIDs[i], _pos, LowerLayer, false, false, (i == 0) ? 1 : 0);
            }
        }

        public override void Update()
        {
            if (images.Length < 2) return;
            frame++;
            if (frame >= interval)
            {
                frame = 0;
                nowNum = nextNum;
                nextNum++;
                nextNum %= images.Length;
            }
            images[nowNum].Alpha = 1;
            images[nextNum].Alpha = (frame % interval) / (float)interval;
            base.Update();
        }

        public override void Draw(Drawing d)
        {
            // baseをDrawしない
            images[nowNum].Draw(d);
            if (images.Length < 2) return;
            images[nextNum].Draw(d);
        }
    }
}
