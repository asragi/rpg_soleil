using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class CharacterPicture: IComponent
    {
        public static DepthID Depth = DepthID.PlayerFront;
        public bool IsDark;

        UIImage image;
        UIImage grayImage;

        public CharacterPicture(TextureID id, Vector pos, Vector? pos_diff, bool isDark)
        {
            IsDark = isDark;
            image = new UIImage(id, pos, pos_diff, Depth);
            grayImage = new UIImage(id, pos, pos_diff, Depth);
            grayImage.Color = ColorPalette.DarkBlue;
        }

        public void Call()
        {
            image.Call();
            grayImage.Call();
        }

        public void Quit()
        {
            image.Quit();
            grayImage.Quit();
        }

        public void Update()
        {
            image.Update();
            grayImage.Update();
        }

        public void Draw(Drawing d)
        {
            image.Draw(d);
            if (!IsDark) return;
            grayImage.Draw(d);
        }
    }
}
