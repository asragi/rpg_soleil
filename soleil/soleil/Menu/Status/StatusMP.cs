using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMP : MenuComponent
    {
        FontImage valText, slashText, maxText;
        const int SpaceToSlash = 70;
        const int SpaceToMax = 30;
        FontImage[] texts;
        public StatusMP(Vector pos, int val, int max)
        {
            valText = new FontImage(FontID.Test, pos, DepthID.MenuTop);
            valText.Text = val.ToString();
            slashText = new FontImage(FontID.Test, pos + new Vector(SpaceToSlash,0), DepthID.MenuTop);
            slashText.Text = "/";
            maxText = new FontImage(FontID.Test, pos + new Vector(SpaceToSlash + SpaceToMax, 0), DepthID.MenuTop);
            maxText.Text = max.ToString();
            texts = new[] { valText, slashText, maxText };
            foreach (var item in texts)
            {
                item.Color = ColorPalette.DarkBlue;
            }
        }

        public override void Call()
        {
            base.Call();
            foreach (var item in texts)
            {
                item.Call();
            }
        }

        public override void Quit()
        {
            base.Quit();
            foreach (var item in texts)
            {
                item.Quit();
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (var item in texts)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            foreach (var item in texts)
            {
                item.Draw(d);
            }
        }
    }
}
