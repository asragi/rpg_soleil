﻿using Microsoft.Xna.Framework;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Images
{
    /// <summary>
    /// 長さの変わる文字背景用の汎用Image．
    /// </summary>
    class BackBarImage
    {
        // 画像の端からの切り出し量
        const int EdgeSize = 36;
        public Vector Pos { get; set; }
        Image[] images;

        public BackBarImage(Vector _pos, int _length, bool centerBased)
        {
            Pos = _pos;
            images = new Image[3];
            var tex = Resources.GetTexture(TextureID.BackBar);
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new Image(0, tex, Pos, DepthID.MessageBack, false, true, 1);
            }
            // 画像切り出し設定
            images[0].Rectangle = new Rectangle(0, 0, EdgeSize, tex.Height);
            images[1].Rectangle = new Rectangle(EdgeSize, 0, tex.Width - 2 * EdgeSize, tex.Height);
            images[2].Rectangle = new Rectangle(tex.Width - EdgeSize, 0, EdgeSize, tex.Height);

            // 拡大率設定
            var size = (_length - 2 * EdgeSize) / (float)(tex.Width - 2 * EdgeSize);
            images[1].Size = new Vector(size, 1);
            // 位置設定
            images[0].Pos = Pos;
            images[1].Pos = Pos + new Vector(EdgeSize, 0);
            images[2].Pos = Pos + new Vector(EdgeSize + (_length - 2 * EdgeSize), 0);
        }

        public void Call()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            }
        }

        public void Quit()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            }
        }

        public void Update()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Update();
            }
        }

        public void Draw(Drawing d)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Draw(d);
            }
        }
    }
}
