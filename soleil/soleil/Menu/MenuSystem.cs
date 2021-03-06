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
        Skill,
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
            // "ステータスを確認します。",
            "音量などの設定を行います。",
            "ゲームデータのセーブを行います。"
        };

        PersonParty party;
        Image backImage, frontImage;

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
        // SkillMenu
        SkillUserSelect skillUserSelect;
        SkillMenu skillMenu;
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
            TextureID.MenuSkill1,
            TextureID.MenuSkill2,
            TextureID.MenuEquip1,
            TextureID.MenuEquip2,
            TextureID.MenuOption1,
            TextureID.MenuOption2,
            TextureID.MenuSave1,
            TextureID.MenuSave2
        };

        public MenuSystem(PersonParty _party)
            : base(null)
        {
            party = _party;
            Index = 0;
            // Image初期化
            backImage = new Image(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuBack);
            frontImage = new Image(TextureID.MenuFront, Vector.Zero, Vector.Zero, DepthID.MenuTop);

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
            statusTargetSelect = new StatusTargetSelect(this, menuDescription, Descriptions[(int)MenuName.Status]);
            statusMenu = new StatusMenu(_party, this);
            // Magic Menu
            magicUserSelect = new MagicUserSelect(this, menuDescription, Descriptions[(int)MenuName.Magic]);
            magicMenu = new MagicMenu(magicUserSelect, menuDescription, party);
            magicTargetSelect = new MagicTargetSelect(magicMenu);
            // Skill Menu
            skillUserSelect = new SkillUserSelect(this, menuDescription, Descriptions[(int)MenuName.Skill]);
            skillMenu = new SkillMenu(skillUserSelect, menuDescription);
            // 詳細ステータス
            statusSystem = new StatusSystem(statusTargetSelect, menuDescription, _party);
            // MenuChildren(foreach用. 描画順に．)
            menuChildren = new MenuChild[] {
                statusMenu, statusTargetSelect,
                itemMenu, itemTargetSelect,
                skillMenu, skillUserSelect,
                magicMenu, magicTargetSelect, magicUserSelect,
                statusSystem };

            // 参照を設定しまくる． // 最悪な状態なのでいい感じにしたい
            itemMenu.SetRefs(itemTargetSelect, statusMenu);
            statusTargetSelect.SetRefs(statusMenu);
            magicUserSelect.SetRefs(statusMenu);
            magicTargetSelect.SetRefs(statusMenu);
            skillUserSelect.SetRefs(statusMenu);
            magicMenu.SetRefs(magicTargetSelect, statusMenu);

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
            moneyComponent = new MoneyComponent(MoneyComponentPos, new Vector(30, 0));
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public override void Call()
        {
            base.Call();
            Audio.PlaySound(SoundID.MenuOpen);
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
            Audio.PlaySound(SoundID.Back);
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
            if (dir != Direction.N) Audio.PlaySound(SoundID.MenuCursor);
            // IsActiveなら自身の項目を動かす
            if (IsActive)
            {
                if (dir == Direction.U) Index--;
                if (dir == Direction.D) Index++;
                Index = (Index + menuItems.Length) % menuItems.Length;
                menuDescription.Text = Descriptions[Index];
                if (KeyInput.GetKeyPush(Key.A)) Decide();
                else if (KeyInput.GetKeyPush(Key.B) || KeyInput.GetKeyPush(Key.C)) Quit();
                return;
            }
            // Activeな子ウィンドウに入力を送る
            foreach (var child in menuChildren)
            {
                if (!child.IsActive) continue;
                child.Input(input);
                if (KeyInput.GetKeyPush(Key.B)) Audio.PlaySound(SoundID.Back);
            }
        }

        /// <summary>
        /// 外部から特定のメニューを有効にする．
        /// </summary>
        public void CallChild(MenuName name, Person p)
        {
            if (name == MenuName.Magic) magicMenu.CallWithPerson(p);
            if (name == MenuName.Status) statusSystem.CallWithPerson(p);
            if (name == MenuName.Skill) skillMenu.CallWithPerson(p);
        }

        void Decide()
        {
            IsActive = false;
            var selected = (MenuName)Index;
            if (selected == MenuName.Items)
            {
                itemMenu.Call();
                Audio.PlaySound(SoundID.DecideSoft);
                return;
            }
            if (selected == MenuName.Status)
            {
                statusTargetSelect.Call();
                Audio.PlaySound(SoundID.DecideSoft);
                return;
            }
            if (selected == MenuName.Magic)
            {
                magicUserSelect.Call();
                Audio.PlaySound(SoundID.DecideSoft);
                return;
            }
            if (selected == MenuName.Skill)
            {
                skillUserSelect.Call();
                Audio.PlaySound(SoundID.DecideSoft);
                return;
            }
            if (selected == MenuName.Option)
            {
                // Option設定用ウィンドウ出現
                IsActive = true; // debug
                return;
            }
            if (selected == MenuName.Save)
            {
                // Save用ウィンドウ出現
                SaveLoad.Save();
                Console.WriteLine("SAVE");
                IsActive = true; // debug
                return;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].MoveToBack();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            for (int i = 0; i < menuItems.Length; i++)
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
