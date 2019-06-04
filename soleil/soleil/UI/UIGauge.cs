using Microsoft.Xna.Framework;
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
        int width, height;
        double rate;
        bool isRightSideEnd;

        DepthID depth;

        Vector drawsize;

        public UIGauge(Vector pos, int _width, int _height, bool _isRightRideRnd, double initRate = 1.0)
        {
            Pos = pos;
            (width, height, isRightSideEnd, rate) = (_width, _height, _isRightRideRnd, initRate);
        }

        public void Refresh()
        {
            drawsize = new Vector(width * rate, height);
        }

        public void Refresh(double _rate)
        {
            rate = _rate;
            Refresh();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            d.DrawBox(Pos, drawsize, Color.White, depth);
        }
    }
}
