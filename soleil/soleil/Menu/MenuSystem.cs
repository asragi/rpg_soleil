﻿using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    enum MenuName
    {
        Items = 0,
        Magic,
        Equip,
        Status,
        Option,
        Save,
        size,
    }
    class MenuSystem
    {
        /// <summary>
        /// メニューを閉じたかどうかのフラグを伝える
        /// </summary>
        public bool IsQuit { get; private set; }
        bool isActive;
        Image backImage, frontImage;
        MenuItem[] menuItems;
        int index;

        // 入力を良い感じにする処理用
        const int InputWait = 8;
        int waitFrame;

        public MenuSystem()
        {
            index = 0;
            isActive = false;
            // Image初期化
            backImage = new Image(0, Resources.GetTexture(TextureID.WhiteWindow), Vector.Zero, DepthID.MessageBack, false, true);
            frontImage = new Image(0, Resources.GetTexture(TextureID.MenuFront), Vector.Zero, DepthID.MessageBack, false, true);
            menuItems = new MenuItem[(int)MenuName.size];
            for (int i = 0; i < menuItems.Length; i++)
            {
                // i==0 のみ selected=trueとする
                menuItems[i] = new MenuItem((MenuName)i, i == 0);
            }
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public void CallMenu()
        {
            isActive = true;
        }

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        public void QuitMenu()
        {
            isActive = false;
        }

        /// <summary>
        /// 入力を受けメニューを操作する。
        /// </summary>
        public void MoveCursor(ObjectDir dir)
        {
            // Activeな子ウィンドウに入力を送る

            // 自身の項目を動かす
            InputSmoother(dir);
        }

        private void InputSmoother(ObjectDir dir)
        {
            waitFrame--;
            if (dir.IsContainUp())
            {
                if (waitFrame > 0) return;
                index--;
                waitFrame = InputWait;
            }
            else if (dir.IsContainDown())
            {
                if (waitFrame > 0) return;
                index++;
                waitFrame = InputWait;
            }
            else{ waitFrame = 0; }
            index = (index + menuItems.Length) % menuItems.Length; // -1 to 5, 6 to 0
        }

        public void Update()
        {
            // ImageUpdate
            backImage.Update();
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Update();
            }
            frontImage.Update();

            // Update Selected
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].IsSelected = i == index;
            }
        }

        public void Draw(Drawing d)
        {
            // Activeでなければ実行しない
            if (!isActive) return;
            // 背景描画
            backImage.Draw(d);
            // 選択肢描画
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(d);
            }
            // 前景描画
            frontImage.Draw(d);
        }
    }
}
