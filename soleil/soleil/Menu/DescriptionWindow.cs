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
        const int Y = 40;
        const int X = 40;
        const int MessagePosDiffX = 49;
        const int MessagePosDiffY = -10;
        BackBarImage backBar;

        /// <summary>
        /// ウィンドウつきMenuDescriptionコンポーネント．
        /// </summary>
        public DescriptionWindow()
            : base(new Vector(X + MessagePosDiffX, Y + MessagePosDiffY))
        {
            var barWidth = Game1.VirtualWindowSizeX - 2 * X;
            backBar = new BackBarImage(new Vector(X, Y), Vector.Zero, barWidth, false);
            fontImage.Font = FontID.CorpM;
            fontImage.Color = ColorPalette.AliceBlue;
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
