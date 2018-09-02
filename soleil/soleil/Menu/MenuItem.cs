using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MenuItem
    {
        const int StartX = 100; // 0個目の項目のx座標
        const int StartY = 100; // 0個目の項目のy座標
        const int SpaceSize = 55; // 項目ごとの感覚
        MenuName menuName;
        Vector pos;
        public bool IsSelected { get; set; }
        Image unselectedImg;
        Image selectedImg;

        public MenuItem(MenuName _menuName, bool select)
        {
            menuName = _menuName;
            // menuNameのenumの割当てintegerを利用して位置を決定
            pos = new Vector(StartX, StartY + SpaceSize * (int)menuName);
            // 選択されているかどうか
            IsSelected = select;

            TextureID unselectedTexID = 0;
            TextureID texID = 0;
            switch (menuName)
            {
                case MenuName.Items:
                    unselectedTexID = TextureID.MenuItem1;
                    texID = TextureID.MenuItem2;
                    break;
                case MenuName.Magic:
                    unselectedTexID = TextureID.MenuMagic1;
                    texID = TextureID.MenuMagic2;
                    break;
                case MenuName.Equip:
                    unselectedTexID = TextureID.MenuEquip1;
                    texID = TextureID.MenuEquip2;
                    break;
                case MenuName.Status:
                    unselectedTexID = TextureID.MenuStatus1;
                    texID = TextureID.MenuStatus2;
                    break;
                case MenuName.Option:
                    unselectedTexID = TextureID.MenuOption1;
                    texID = TextureID.MenuOption2;
                    break;
                case MenuName.Save:
                    unselectedTexID = TextureID.MenuSave1;
                    texID = TextureID.MenuSave2;
                    break;
            }
            unselectedImg = new Image(0, Resources.GetTexture(unselectedTexID), pos, DepthID.MessageBack, false, true, 0);
            selectedImg = new Image(0, Resources.GetTexture(texID), pos, DepthID.MessageBack, false, true, 0);
        }

        public void Update()
        {
            selectedImg.Update();
            unselectedImg.Update();
        }

        public void Fade(int duration, Func<double, double, double, double, double> easing, bool isFadein)
        {
            selectedImg.Fade(duration, easing, isFadein);
            unselectedImg.Fade(duration, easing, isFadein);
        }

        public void Draw(Drawing d)
        {
            if (IsSelected)
            {
                selectedImg.Draw(d);
            }
            else
            {
                unselectedImg.Draw(d);
            }
        }
    }
}
