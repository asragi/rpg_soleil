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

        Image image;
        Image grayImage;

        public Vector Pos {
            get => image.Pos;
            set
            {
                image.Pos = value;
                grayImage.Pos = value;
            }
        }

        public CharacterPicture(TextureID id, Vector pos, Vector pos_diff, bool isDark)
        {
            IsDark = isDark;
            image = new Image(id, pos, pos_diff, Depth);
            grayImage = new Image(id, pos, pos_diff, Depth);
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
