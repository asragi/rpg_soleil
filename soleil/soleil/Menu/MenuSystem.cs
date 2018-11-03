﻿using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
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
    class MenuSystem : SystemBase
    {
        /// <summary>
        /// メニューを閉じたかどうかのフラグを伝える
        /// </summary>
        public bool IsQuit { get; private set; }
        readonly String[] Descriptions = new String[]
        {
            "アイテムを確認・選択して使用します。",
            "魔法を確認・選択して使用します。",
            "装備を確認・変更します。",
            "ステータスを確認します。",
            "音量などの設定を行います。",
            "ゲームデータのセーブを行います。"
        };

        UIImage backImage, frontImage;
        MenuItem[] menuItems;
        MenuLine menuLineUpper, menuLineLower;
        MenuDescription menuDescription;
        public int Index { get; private set; }
        Transition transition;

        // MenuChildren
        MenuChild[] menuChildren;
        // ItemMenu
        ItemMenu itemMenu;
        // MagicMenu
        MagicMenu magicMenu;
        // Status 表示
        StatusMenu statusMenu;
        // 詳細ステータス
        StatusSystem statusSystem;

        // Transition
        public const int FadeSpeed = 23;
        public static readonly EFunc EaseFunc = Easing.OutQuad;

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
            :base(null)
        {
            Index = 0;
            // Image初期化
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuBack);
            frontImage = new UIImage(TextureID.MenuFront, Vector.Zero, Vector.Zero, DepthID.MenuTop);

            // 選択肢たち
            menuItems = new MenuItem[(int)MenuName.size];
            for (int i = 0; i < menuItems.Length; i++)
            {
                // i==0 のみ selected=trueとする
                menuItems[i] = new MenuItem((MenuName)i, i == 0);
            }
            // Image line
            menuLineUpper = new MenuLine(70, true);
            menuLineLower = new MenuLine(470, false);
            // MenuDescription
            menuDescription = new MenuDescription(new Vector(125, 35));
            // Item Menu
            itemMenu = new ItemMenu(this, menuDescription);
            // Status Menu
            statusMenu = new StatusMenu(this);
            // Magic Menu
            magicMenu = new MagicMenu(statusMenu, menuDescription);
            // 詳細ステータス
            statusSystem = new StatusSystem(statusMenu);
            // MenuChildren(foreach用. 描画順に．)
            menuChildren = new MenuChild[] { statusMenu, itemMenu, magicMenu, statusSystem };

            Components = new MenuComponent[]
            {
                menuLineLower,
                menuLineUpper,
                menuDescription,
            };
            Images = new UIImage[]
            {
                backImage,
                frontImage,
            };

            // Transition
            transition = Transition.GetInstance();

            // 入力処理
            IsActive = false;
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public override void Call()
        {
            base.Call();
            transition.SetDepth(DepthID.Effect);
            ImageTransition(TransitionMode.FadeOut);
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Call();
            }
            IsActive = true;
            IsQuit = false;

            // statusMenu召喚
            statusMenu.FadeIn();
        }

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        public override void Quit()
        {
            base.Quit();
            // Set bools
            IsActive = false;
            IsQuit = true;
            //transition.SetDepth(DepthID.Debug);
            ImageTransition(TransitionMode.FadeIn);
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Quit();
            }
            // statusMenu退散
            statusMenu.FadeOut();
        }

        private void ImageTransition(TransitionMode mode)
        {
            // Transition
            transition.SetMode(mode);
            var isFadeOut = mode == TransitionMode.FadeOut;
        }

        /// <summary>
        /// 入力を受けメニューを操作する。
        /// </summary>
        public void Input(Direction dir)
        {
            var input = dir;
            // IsActiveなら自身の項目を動かす
            if (IsActive)
            {
                if (dir == Direction.U) Index--;
                if (dir == Direction.D) Index++;
                Index = (Index + menuItems.Length) % menuItems.Length;
                menuDescription.Text = Descriptions[Index];
                if (KeyInput.GetKeyPush(Key.A)) Decide();
                else if (KeyInput.GetKeyPush(Key.B)) Quit();
                return;
            }
            // Activeな子ウィンドウに入力を送る
            foreach (var child in menuChildren)
            {
                if (!child.IsActive) continue;
                child.Input(input);
            }
        }

        /// <summary>
        /// 外部から特定のメニューを有効にする．
        /// </summary>
        public void CallChild(MenuName name)
        {
            if (name == MenuName.Magic) magicMenu.Call();
            if (name == MenuName.Status) statusSystem.Call();
        }

        void Decide()
        {
            IsActive = false;
            var selected = (MenuName)Index;
            if(selected == MenuName.Items)
            {
                itemMenu.Call();
                return;
            }
            if(selected == MenuName.Magic || selected == MenuName.Equip || selected == MenuName.Status)
            {
                statusMenu.IsActive = true;
                return;
            }
            if(selected == MenuName.Option)
            {
                // Option設定用ウィンドウ出現

                IsActive = true; // debug
                return;
            }
            if(selected == MenuName.Save)
            {
                // Save用ウィンドウ出現

                IsActive = true; // debug
                return;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            for (int i = 0; i<menuItems.Length; i++)
            {
                menuItems[i].MoveToBack();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            for (int i = 0; i<menuItems.Length; i++)
            {
                menuItems[i].MoveToDefault();
            }
        }

        public override void Update()
        {
            base.Update();
            // ImageUpdate
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Update();
            }
            statusMenu.Update();
            // Update Selected
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].IsSelected = i == Index;
            }

            // Update Children
            foreach (var child in menuChildren)
            {
                child.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            // 選択肢描画
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(d);
            }
            // 子ウィンドウ描画
            foreach (var child in menuChildren)
            {
                child.Draw(d);
            }
        }
    }
}
