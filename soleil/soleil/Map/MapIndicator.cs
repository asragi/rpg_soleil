using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 画面右上に日付を表示するクラス．
    /// </summary>
    class MapIndicator
    {
        static Vector upperRight = new Vector(Game1.VirtualWindowSizeX, 0);
        FontID font = FontID.CorpM;
        ImageManager imageManager;
        Image backImage;
        GameDateTime gameDate;
        public MapIndicator()
        {
            imageManager = new ImageManager();
            backImage = imageManager.CreateImg(TextureID.IndicatorBack, upperRight + new Vector(-10,0), DepthID.Frame);
            backImage.Alpha = 0.5f;
            gameDate = GameDateTime.GetInstance();
        }

        public void Update()
        {
            backImage.Angle += 0.002f;
            imageManager.Update();
        }

        public void Draw(Drawing d)
        {
            imageManager.Draw(d);
            var textPos = upperRight;
            d.DrawStaticText(textPos + new Vector(-55, 30), Resources.GetFont(font), $"Day {gameDate.NowDay}", Color.AliceBlue, DepthID.Frame, Vector.One);
            d.DrawStaticText(textPos + new Vector(-30, 65), Resources.GetFont(font), gameDate.IsDaytime ? "昼" : "夜", Color.AliceBlue, DepthID.Frame, Vector.One);
        }
    }
}
