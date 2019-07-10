using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    class MenuItem
    {
        const int StartX = 100; // 0個目の項目のx座標
        const int StartY = 100; // 0個目の項目のy座標
        const int SpaceSize = 60; // 項目ごとの感覚
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

            TextureID unselectedTexID = MenuSystem.optionTextures[(int)menuName * 2];
            TextureID texID = MenuSystem.optionTextures[(int)menuName * 2 + 1];
            unselectedImg = new Image(unselectedTexID, pos, new Vector(-50,0), DepthID.MenuBottom);
            selectedImg = new Image(texID, pos, new Vector(-50, 0), DepthID.MenuBottom);
        }

        public void Update()
        {
            selectedImg.Update();
            unselectedImg.Update();
        }

        public void Call()
        {
            unselectedImg.Call();
            selectedImg.Call();
        }

        public void Quit()
        {
            unselectedImg.Quit();
            selectedImg.Quit();
        }

        public void MoveToDefault()
        {
            unselectedImg.MoveToDefault();
            selectedImg.MoveToDefault();
        }

        public void MoveToBack()
        {
            unselectedImg.MoveToBack();
            selectedImg.MoveToBack();
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
