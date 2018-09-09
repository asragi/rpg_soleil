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
    class MenuSystem : MenuComponent
    {
        /// <summary>
        /// メニューを閉じたかどうかのフラグを伝える
        /// </summary>
        public bool IsQuit { get; private set; }
        
        Image backImage, frontImage;
        MenuItem[] menuItems;
        MenuLine menuLineUpper, menuLineLower;
        int index;
        Transition transition;

        // MenuChildren
        MenuChild[] menuChildren;

        // 入力を良い感じにする処理用
        const int InputWait = 8;
        int waitFrame;

        // Transition
        const int FadeSpeed = 23;
        readonly Func<double, double, double, double, double> func = Easing.OutQuad;

        // Menuの項目の対応用配列
        public static readonly TextureID[] optionTextures =
        {
            TextureID.MenuItem1,
            TextureID.MenuItem2,
            TextureID.MenuMagic1,
            TextureID.MenuMagic2,
            TextureID.MenuEquip1,
            TextureID.MenuEquip2,
            TextureID.MenuStatus1,
            TextureID.MenuStatus2,
            TextureID.MenuOption1,
            TextureID.MenuOption2,
            TextureID.MenuSave1,
            TextureID.MenuSave2
        };

        public MenuSystem()
        {
            index = 0;
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
            IsActive = false;

            // MenuChildren
            menuChildren = new MenuChild[] { new ItemMenu(this) };
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public void CallMenu()
        {
            transition.SetDepth(DepthID.Effect);
            ImageTransition(TransitionMode.FadeOut);
            IsActive = true;
            IsQuit = false;
        }

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        public void QuitMenu()
        {
            // Set bools
            IsActive = false;
            IsQuit = true;
            //transition.SetDepth(DepthID.Debug);
            ImageTransition(TransitionMode.FadeIn);
        }

        private void ImageTransition(TransitionMode mode)
        {
            // Transition
            transition.SetMode(mode);
            var isFadeOut = mode == TransitionMode.FadeOut;
            // Transition Images
            backImage.Fade(FadeSpeed, func, isFadeOut);
            frontImage.Fade(FadeSpeed, func, isFadeOut);
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Fade(FadeSpeed, func, isFadeOut);
            }
            menuLineLower.Fade(FadeSpeed-3, func, isFadeOut);
            menuLineUpper.Fade(FadeSpeed-3, func, isFadeOut);
        }

        /// <summary>
        /// 入力を受けメニューを操作する。
        /// </summary>
        public void Input(ObjectDir dir, Dictionary<Key, bool> inputs)
        {
            // IsActiveなら自身の項目を動かす
            if (IsActive)
            {
                InputSmoother(dir);
                if (inputs[Key.A]) Decide();
                else if (inputs[Key.B]) QuitMenu();
                return;
            }
            // Activeな子ウィンドウに入力を送る
            foreach (var child in menuChildren)
            {
                if (!child.IsActive) continue;
                child.Input(dir, inputs);
            }
        }

        void Decide()
        {
            IsActive = false;
            menuChildren[index].IsActive = true;
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

        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].MoveToBack(Vector.Zero, FadeSpeed, func);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].MoveToDefault(Vector.Zero, FadeSpeed, func);
            }
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
