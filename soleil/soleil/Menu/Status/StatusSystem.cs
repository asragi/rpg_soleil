using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusSystem: MenuComponent
    {
        MenuComponent[] components;
        Image backImage, frontImage;
        Image[] images;
        public StatusSystem()
            : base()
        {
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuBack), Vector.Zero, DepthID.MessageBack, false, true, 0);
            frontImage = new Image(0, Resources.GetTexture(TextureID.MenuFront), Vector.Zero, DepthID.MessageBack, false, true, 0);
            images = new Image[] { backImage, frontImage };
        }

        public override void Call()
        {
            base.Call();
        }

        public override void Quit()
        {
            base.Quit();
        }

        public override void Update()
        {
            base.Update();
            foreach (var item in components)
            {
                item.Update();
            }
            foreach (var item in images)
            {
                item.Update();
            }
        }



    }
}
