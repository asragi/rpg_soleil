using Soleil.Map;
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
        MenuLine menuLineUpper, menuLineLower;
        int index;
        Transition transition;

        // 入力を良い感じにする処理用
        const int InputWait = 8;
        int waitFrame;

        public MenuSystem()
        {
            index = 0;
            isActive = false;
            // Image初期化
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuBack), Vector.Zero, DepthID.MessageBack, false, true, 0);
            frontImage = new Image(0, Resources.GetTexture(TextureID.MenuFront), Vector.Zero, DepthID.MessageBack, false, true, 0);
            menuItems = new MenuItem[(int)MenuName.size];
            for (int i = 0; i < menuItems.Length; i++)
            {
                // i==0 のみ selected=trueとする
                menuItems[i] = new MenuItem((MenuName)i, i == 0);
            }
            // Image line
            menuLineUpper = new MenuLine(70, true);
            menuLineLower = new MenuLine(470, false);
            // Transition
            transition = Transition.GetInstance();
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public void CallMenu()
        {
            transition.SetDepth(DepthID.Effect);
            ImageTransition(TransitionMode.FadeOut);
            isActive = true;
            IsQuit = false;
        }

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        public void QuitMenu()
        {
            //transition.SetDepth(DepthID.Debug);
            ImageTransition(TransitionMode.FadeIn);
            // Set bools
            isActive = false;
            IsQuit = true;
        }

        private void ImageTransition(TransitionMode mode)
        {
            // Transition
            transition.SetMode(mode);
            var isFadeOut = mode == TransitionMode.FadeOut;
            // Transition Images
            backImage.Fade(23, Easing.OutQuad, isFadeOut);
            frontImage.Fade(23, Easing.OutQuad, isFadeOut);
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Fade(23, Easing.OutQuad, isFadeOut);
            }
            menuLineLower.Fade(20, Easing.OutQuad, isFadeOut);
            menuLineUpper.Fade(20, Easing.OutQuad, isFadeOut);
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

        /// <summary>
        /// 入力押しっぱなしでも毎フレーム移動しないようにする関数
        /// </summary>
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
            menuLineUpper.Update();
            menuLineLower.Update();

            // Update Selected
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].IsSelected = i == index;
            }

            // Debug
            if (KeyInput.GetKeyPush(Key.B)) QuitMenu();
        }

        public void Draw(Drawing d)
        {
            // 背景描画
            backImage.Draw(d);
            // Line描画
            menuLineUpper.Draw(d);
            menuLineLower.Draw(d);
            // 選択肢描画
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(d);
            }
            // 文章描画

            // 前景描画
            frontImage.Draw(d);
        }
    }
}
