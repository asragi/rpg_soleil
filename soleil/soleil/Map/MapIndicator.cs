using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class MapIndicator
    {
        static Vector upperRight = new Vector(Game1.VirtualWindowSizeX, 0);
        FontID font = FontID.CorpM;
        UIImage backImage;
        public MapIndicator()
        {
            backImage = new UIImage(TextureID.IndicatorBack, upperRight + new Vector(-10, 0), Vector.Zero, DepthID.Frame, true, alpha: 0.5f);
        }

        public void Update()
        {
            backImage.Angle += 0.002f;
            backImage.Update();
        }

        public void Draw(Drawing d)
        {
            backImage.Draw(d);
            var textPos = upperRight;
            d.DrawStaticText(textPos + new Vector(-55, 30), Resources.GetFont(font), "Day 4", Color.AliceBlue, DepthID.Frame, Vector.One);
            d.DrawStaticText(textPos + new Vector(-30, 65), Resources.GetFont(font), "朝", Color.AliceBlue, DepthID.Frame, Vector.One);
        }
    }
}
