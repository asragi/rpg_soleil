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
    class UIGauge: MenuComponent
    {
        public Vector Pos { get; set; }
        bool isRightSideEnd;

        BarBlock barBlock;

        public UIGauge(Vector pos, Vector size, bool _isRightRideRnd, DepthID _depth, double initRate = 1.0)
        {
            Pos = pos;
            barBlock = new BarBlock(pos, Vector.Zero, size, _depth);
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

            public BarBlock(Vector pos, Vector posdiff, Vector _size, DepthID depth)
                :base(pos, posdiff, depth, false, true, 0)
            {
                size = _size;
                Rate = 1;
            }

            public override Vector GetSize => size;

            public override void Draw(Drawing d)
            {
                d.DrawBox(Pos, drawsize, Color.White * Alpha, DepthID);
            }
        }
    }
}
