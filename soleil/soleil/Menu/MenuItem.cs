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
        const int StartY = 300; // 0個目の項目のy座標
        const int SpaceSize = 80; // 項目ごとの感覚
        MenuName menuName;
        Vector pos;
        bool isSelected;
        Image unselectedImg;
        Image selectedImg;

        public MenuItem(MenuName _menuName, bool select)
        {
            menuName = _menuName;
            // menuNameのenumの割当てintegerを利用して位置を決定
            pos = new Vector(StartX, StartY + SpaceSize * (int)menuName);
            // 選択されているかどうか
            isSelected = select;

            TextureID texID = 0;
            TextureID untexID = 0;
            switch (menuName)
            {
                case MenuName.Items:
                    texID = TextureID.MenuItem1;
                    untexID = TextureID.MenuItem2;
                    break;
                case MenuName.Magic:
                    texID = TextureID.MenuMagic1;
                    untexID = TextureID.MenuMagic2;
                    break;
                case MenuName.Equip:
                    texID = TextureID.MenuEquip1;
                    untexID = TextureID.MenuEquip2;
                    break;
                case MenuName.Status:
                    texID = TextureID.MenuStatus1;
                    untexID = TextureID.MenuStatus2;
                    break;
                case MenuName.Option:
                    texID = TextureID.MenuOption1;
                    untexID = TextureID.MenuOption2;
                    break;
                case MenuName.Save:
                    texID = TextureID.MenuSave1;
                    untexID = TextureID.MenuSave2;
                    break;
            }
            selectedImg = new Image(0, Resources.GetTexture(texID), pos, DepthID.MessageBack, false, true);
            unselectedImg = new Image(0, Resources.GetTexture(untexID), pos, DepthID.MessageBack, false, true);
        }

        public void Update()
        {
            selectedImg.Update();
            unselectedImg.Update();
        }

        public void Draw(Drawing d)
        {
            if (isSelected)
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
