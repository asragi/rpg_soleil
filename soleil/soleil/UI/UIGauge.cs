using Microsoft.Xna.Framework;
using Soleil.Images;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.UI
{
    /// <summary>
    /// 値によって伸び縮みするゲージ状UIのクラス
    /// </summary>
    class UIGauge: MenuComponent
    {
        public Vector Pos { get; set; }
        bool isRightSideEnd;

        BarBlock barBlock;
        public int FrameWait { set => barBlock.FrameWait = value; }

        public UIGauge(Vector pos, Vector posdiff, Vector size, bool _isRightRideRnd, DepthID _depth, double initRate = 1.0)
        {
            Pos = pos;
            barBlock = new BarBlock(pos, posdiff, size, _depth);
            barBlock.Rate = initRate;
        }

        public void Refresh(double _rate) => barBlock.Rate = _rate;

        public override void Call()
        {
            base.Call();
            barBlock.Call();
        }

        public override void Quit()
        {
            base.Quit();
            barBlock.Quit();
        }

        public override void Update()
        {
            base.Update();
            barBlock.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            barBlock.Draw(d);
        }

        class BarBlock : UIImageBase
        {
            Vector size;
            double rate;
            public double Rate { get => rate; set { rate = value; drawsize = new Vector(size.X * rate, size.Y); } }
            Vector drawsize;
            bool centerBased;

            public BarBlock(Vector pos, Vector posdiff, Vector _size, DepthID depth)
                :base(pos, posdiff, depth, false, true, 0)
            {
                size = _size;
                centerBased = false;
            }

            public override Vector ImageSize => size;

            public override void Draw(Drawing d)
            {
                var flag = d.CenterBased;
                d.CenterBased = centerBased;
                d.DrawBoxStatic(Pos, drawsize, Color.White * Alpha, DepthID);
                d.CenterBased = flag;
            }
        }
    }
}
