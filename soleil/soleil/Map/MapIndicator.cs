﻿using Microsoft.Xna.Framework;
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
        Image backImage;
        GameDateTime gameDate;
        public MapIndicator()
        {
            backImage = new Image(TextureID.IndicatorBack, upperRight + new Vector(-10, 0), Vector.Zero, DepthID.Frame, true, alpha: 0.5f);
            backImage.Alpha = 0.5f;
            gameDate = GameDateTime.GetInstance();
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
            d.DrawStaticText(textPos + new Vector(-59, 23), Resources.GetFont(font), $"Day {gameDate.NowDay}", Color.AliceBlue, DepthID.Frame, Vector.One);
            d.DrawStaticText(textPos + new Vector(-55, 50), Resources.GetFont(font), gameDate.NowTimeStr(), Color.AliceBlue, DepthID.Frame, Vector.One);
            d.DrawStaticText(textPos + new Vector(-30, 75), Resources.GetFont(font), gameDate.IsDaytime ? "昼" : "夜", Color.AliceBlue, DepthID.Frame, Vector.One);
        }
    }
}
