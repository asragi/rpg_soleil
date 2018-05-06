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

        ImageManager imageManager;
        int backImageID;
        public MapIndicator()
        {
            imageManager = new ImageManager();
            backImageID = imageManager.Create(TextureID.IndicatorBack, upperRight + new Vector(-10,0), DepthID.Frame);
        }

        public void Update()
        {
            imageManager.Get(backImageID).Angle += 0.002f;
            imageManager.Update();
        }

        public void Draw(Drawing d)
        {
            imageManager.Draw(d);
            var textPos = upperRight;
            d.DrawStaticText(textPos + new Vector(-65, 30), Resources.GetFont(FontID.Test), "Day 4", Color.AliceBlue, DepthID.Frame, Vector.One);
            d.DrawStaticText(textPos + new Vector(-30, 70), Resources.GetFont(FontID.Test), "朝", Color.AliceBlue, DepthID.Frame, Vector.One);
        }
    }
}
