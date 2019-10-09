using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// アイテムやスキルの入手などを通知するトースト表示を行うクラス．
    /// </summary>
    class ToastMaster
    {
        private readonly Vector BasePos = new Vector(645, 480);
        private readonly Vector PositionDiff = new Vector(0, -53);
        private readonly Vector PosDiff = new Vector(40, 0);
        private readonly DepthID Depth = DepthID.Message;
        private const int ToastMax = 6;
        private readonly Toast[] toasts;

        public ToastMaster()
        {
            toasts = new Toast[ToastMax];
            for (int i = 0; i < ToastMax; i++)
                toasts[i] = new Toast(BasePos + PositionDiff * i, PosDiff, Depth);
        }

        public void Invoke(TextureID icon, string text, int num = -1)
        {
            for (int i = 0; i < toasts.Length; i++)
            {
                var target = toasts[i];
                if (target.InUse) continue;
                target.Invoke(icon, text, num);
                return;
            }
        }

        public void Update()
        {
            toasts.ForEach2(t => t.Update());
        }

        public void Draw(Drawing d) => toasts.ForEach2(t => t.Draw(d));


        /// <summary>
        /// トースト単体のクラス．
        /// </summary>
        class Toast: MenuComponent
        {
            private const int DisplayTime = 120;
            private static Vector IconPos = new Vector(60, 15);
            private static Vector TextPos = new Vector(83, 12);
            private static Vector NumPos = new Vector(288, 12);
            private const FontID Font = FontID.CorpM;
            private static int EasingFrame = MenuSystem.FadeSpeed;

            private readonly Image backImg;
            private readonly Image iconImg;
            private readonly TextImage textImg;
            private readonly TextImage numText;

            private int displayTime;

            public Toast(Vector pos, Vector posDiff, DepthID depth)
            {
                backImg = new Image(TextureID.ToastBack, pos, posDiff, depth);
                iconImg = new Image(TextureID.IconAccessary, pos + IconPos, posDiff, depth);
                textImg = new TextImage(Font, pos + TextPos, posDiff, depth);
                numText = new RightAlignText(Font, pos + NumPos, posDiff, depth);
                displayTime = int.MaxValue;
                AddComponents(backImg, iconImg, textImg, numText);
            }

            public bool InUse => displayTime < DisplayTime + EasingFrame;

            public void Invoke(TextureID icon, string text, int num = -1)
            {
                iconImg.TexChange(icon);
                textImg.Text = text;
                numText.Text = (num != -1) ? num.ToString() : string.Empty;
                displayTime = 0;
                Call();
            }

            public override void Update()
            {
                base.Update();

                if (displayTime > DisplayTime + EasingFrame) return;
                displayTime++;
                if (displayTime == DisplayTime) Quit();
            }
        }
    }
}
