using Soleil.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class DescriptionWindow : MenuDescription
    {
        const int Y = 80;
        const int X = 40;
        BackBarImage backBar;

        /// <summary>
        /// ウィンドウつきMenuDescriptionコンポーネント．
        /// </summary>
        public DescriptionWindow()
            :base(new Vector(X,Y))
        {
            var barWidth = Game1.VirtualWindowSizeX - 2 * X;
            backBar = new BackBarImage(new Vector(X, Y), barWidth, false);
        }

        public override void Call()
        {
            base.Call();
            backBar.Call();
        }

        public override void Quit()
        {
            base.Quit();
            backBar.Quit();
        }

        public override void Update()
        {
            base.Update();
            backBar.Update();
        }

        public override void Draw(Drawing d)
        {
            backBar.Draw(d);
            base.Draw(d);
        }
    }
}
