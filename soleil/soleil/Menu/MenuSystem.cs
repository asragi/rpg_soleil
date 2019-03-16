using Soleil.Map;
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
        Skill,
        Equip,
        Status,
        Option,
        Save,
        size,
    }
    class MenuSystem : MenuChild
    {
        /// <summary>
        /// メニューを閉じたかどうかのフラグを伝える
        /// </summary>
        public bool IsQuit { get; private set; }
        readonly Vector MoneyComponentPos = new Vector(680, 507);
        readonly string[] Descriptions = new string[]
        {
            "アイテムを確認・選択して使用します。",
            "魔法を確認・選択して使用します。",
            "スキルを確認・選択して使用します。",
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
        ItemTargetSelect itemTargetSelect;
        // MagicMenu
        MagicMenu magicMenu;
        MagicUserSelect magicUserSelect;
        MagicTargetSelect magicTargetSelect;
        // Status 表示
        StatusTargetSelect statusTargetSelect;
        StatusMenu statusMenu;
        // 詳細ステータス
        StatusSystem statusSystem;
        // 所持金表示
        MoneyComponent moneyComponent;

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
            menuLineUpper = new MenuLine(70, -30, true);
            menuLineLower = new MenuLine(470, 20, false);
            // MenuDescription
            menuDescription = new MenuDescription(new Vector(125, 35));
            // Item Menu
            itemMenu = new ItemMenu(this, menuDescription);
            itemTargetSelect = new ItemTargetSelect(itemMenu);
            // Item Target Select
            // Status Menu
            statusTargetSelect = new StatusTargetSelect(this);
            statusMenu = new StatusMenu(this);
            // Magic Menu
            magicUserSelect = new MagicUserSelect(this);
            magicMenu = new MagicMenu(magicUserSelect, menuDescription);
            magicTargetSelect = new MagicTargetSelect(magicMenu);

            // 詳細ステータス
            statusSystem = new StatusSystem(statusTargetSelect, menuLineUpper, menuLineLower);
            // MenuChildren(foreach用. 描画順に．)
            menuChildren = new MenuChild[] {
                statusMenu, statusTargetSelect,
                itemMenu, itemTargetSelect,
                magicMenu, magicTargetSelect, magicUserSelect,
                statusSystem };

            // 参照を設定しまくる． // 最悪な状態なのでいい感じにしたい
            itemMenu.SetRefs(itemTargetSelect, statusMenu);
            statusTargetSelect.SetRefs(statusMenu);
            magicUserSelect.SetRefs(statusMenu);
            magicTargetSelect.SetRefs(statusMenu);

            // メニューと同時に立ち上がったり閉じたりしてほしいInputに関係ないものたち．
            AddComponents(new IComponent[]
            {
                backImage,
                menuLineLower,
                menuLineUpper,
                menuDescription,
                frontImage,
            });

            // Transition
            transition = Transition.GetInstance();

            // 入力処理
            IsActive = false;

            // Money
            moneyComponent = new MoneyComponent(MoneyComponentPos);
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
            statusMenu.Call(false);
            IsActive = true;
            IsQuit = false;

            moneyComponent.Call();
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
            statusMenu.Quit();
            //transition.SetDepth(DepthID.Debug);
            ImageTransition(TransitionMode.FadeIn);
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Quit();
            }
            moneyComponent.Quit();
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
            if (selected == MenuName.Status)
            {
                statusTargetSelect.Call();
                return;
            }
            if (selected == MenuName.Magic)
            {
                magicUserSelect.Call();
                return;
            }
            if (selected == MenuName.Option)
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
            moneyComponent.Update();
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
            // Money描画
            moneyComponent.Draw(d);
        }
    }
}
