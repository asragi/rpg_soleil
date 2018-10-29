using Soleil.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class DescriptionWindow
    {
        const int Y = 80;
        const int X = 40;
        BackBarImage backBar;
        MenuDescription description;
        public String Text { set => description.Text = value; }

        /// <summary>
        /// ウィンドウつきMenuDescriptionコンポーネント．
        /// </summary>
        public DescriptionWindow()
        {
            var barWidth = Game1.VirtualWindowSizeX - 2 * X;
            backBar = new BackBarImage(new Vector(X, Y), barWidth, false);
            description = new MenuDescription(new Vector(X, Y));
        }

        public void Update()
        {
            backBar.Update();
            description.Update();
        }

        public void Draw(Drawing d)
        {
            backBar.Draw(d);
            description.Draw(d);
        }
    }
}
