using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MenuCursor: MenuComponent
    {
        Image img;
        Vector[] positions;
        public MenuCursor(TextureID id, Vector[] destinations)
        {
            positions = destinations;
            img = new Image(id, destinations[0], Vector.Zero, DepthID.MenuBottom);
        }

        public void MoveTo(int index)
        {
            //img.MoveTo(positions[index], 15, MenuSystem.EaseFunc);
            img.Pos = positions[index];
        }

        public override void Call()
        {
            base.Call();
            img.Call(false);
        }

        public override void Quit()
        {
            base.Quit();
            img.Quit(false);
        }

        public override void Update()
        {
            base.Update();
            img.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            img.Draw(d);
        }
    }
}
