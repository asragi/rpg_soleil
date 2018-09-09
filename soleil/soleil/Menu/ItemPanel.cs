﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel
    {
        public readonly Vector PanelSize = new Vector(300, 36);
        /// <summary>
        /// パネルの文字描画を左上からどれだけの位置にするか
        /// </summary>
        readonly Vector Spacing = new Vector(8, 4);
        readonly FontID ItemFont = FontID.Test;

        Vector pos;
        public Vector Pos {
            get { return pos; }
            set
            {
                pos = value;
                itemNameImage.Pos = pos + Spacing;
                selectedBack.Pos = pos;
                unselectedBack.Pos = pos;
            }
        }

        FontImage itemNameImage;
        readonly public String ItemName;

        // 当該アイテムの所持数
        int itemNum;
        FontImage itemNumImage;

        // 選択状態の背景（これCursorとしてくらすにしたほうがよいきがする）
        Image selectedBack;
        Image unselectedBack;

        public ItemPanel(String itemName, int num)
        {
            ItemName = itemName;
            pos = Vector.Zero;
            // itemNum
            itemNum = num;
            itemNumImage = new FontImage(ItemFont, Vector.Zero, DepthID.Message, true, 0);
            itemNumImage.Color = ColorPalette.DarkBlue;
            itemNumImage.Text = itemNum.ToString();
            // Set Font Image
            itemNameImage = new FontImage(ItemFont, Vector.Zero, DepthID.Message, true, 0);
            itemNameImage.Color = ColorPalette.DarkBlue;
            itemNameImage.EnableShadow = false;
            itemNameImage.ShadowPos = new Vector(3, 3);
            itemNameImage.ShadowColor = ColorPalette.GlayBlue;
            itemNameImage.Text = itemName;

            // 選択状態を示すやつ
            selectedBack = new Image(0, Resources.GetTexture(TextureID.MenuSelected), Vector.Zero, DepthID.Message, false, true, 0);
            unselectedBack = new Image(0, Resources.GetTexture(TextureID.MenuUnselected), Vector.Zero, DepthID.Message, false, true, 0);
        }

        public void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            itemNameImage.Fade(duration, _easeFunc, isFadeIn);
            itemNumImage.Fade(duration, _easeFunc, isFadeIn);
            selectedBack.Fade(duration, _easeFunc, isFadeIn);
            unselectedBack.Fade(duration, _easeFunc, isFadeIn);
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            itemNameImage.MoveTo(target + Spacing, duration, _easeFunc);
            itemNumImage.MoveTo(target + new Vector(300,0), duration, _easeFunc);
            selectedBack.MoveTo(target, duration, _easeFunc);
            unselectedBack.MoveTo(target, duration, _easeFunc);
        }

        public void SetSelected(bool select)
        {
            if (select)
            {
                selectedBack.Fade(20, Easing.OutCubic, true);
                unselectedBack.Fade(20, Easing.OutCubic, false);
                itemNameImage.Color = ColorPalette.AliceBlue;
            }
            else
            {
                selectedBack.Fade(20, Easing.OutCubic, false);
                unselectedBack.Fade(20, Easing.OutCubic, true);
                itemNameImage.Color = ColorPalette.DarkBlue;
            }
        }

        public void Update()
        {
            itemNameImage.Update();
            itemNumImage.Update();
            selectedBack.Update();
            unselectedBack.Update();
        }

        public void Draw(Drawing d)
        {
            selectedBack.Draw(d);
            //unselectedBack.Draw(d);
            itemNameImage.Draw(d);
            itemNumImage.Draw(d);
        }
    }
}
