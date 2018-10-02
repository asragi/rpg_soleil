using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニュー画面で登場する小さなウィンドウの基底
    /// </summary>
    abstract class BasicMenu : MenuChild
    {
        protected const int FadeSpeed = 30; // アイテムメニューが出現するスピード
        protected const int RowSize = 8; // 現在のフォントサイズだと8項目がちょうどよい
        protected readonly Vector WindowPos = new Vector(330, 100);
        protected readonly Vector WindowStartPos = new Vector(830, 100);

        protected readonly Vector ItemDrawStartPos = new Vector(25, 28);
        protected readonly int ItemPanelSpacing = 4;

        Image backImage;
        public Vector Pos { get { return backImage.Pos; } }
        protected int Index;
        protected SelectablePanel[] Panels;
        protected int IndexSize;

        public BasicMenu(MenuComponent parent)
            : base(parent)
        {
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuModalBack), WindowStartPos, DepthID.MessageBack, false, true, 0);
            Index = 0;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            backImage.MoveTo(WindowStartPos, FadeSpeed, MenuSystem.EaseFunc);
            backImage.Fade(FadeSpeed, MenuSystem.EaseFunc, false);

            foreach (var item in Panels)
            {
                item.MoveTo(WindowStartPos + item.LocalPos, FadeSpeed, MenuSystem.EaseFunc);
                item.Fade(FadeSpeed, MenuSystem.EaseFunc, false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            backImage.MoveTo(WindowPos, FadeSpeed, MenuSystem.EaseFunc);
            backImage.Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            foreach (var item in Panels)
            {
                item.MoveTo(WindowPos + item.LocalPos, FadeSpeed, MenuSystem.EaseFunc);
                item.Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            }
            RefreshSelected();
        }

        public override void Update()
        {
            base.Update();
            backImage.Update();
            foreach (var item in Panels)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            backImage.Draw(d);
            foreach (var item in Panels)
            {
                item.Draw(d);
            }
        }

        protected void RefreshSelected()
        {
            for (int i = 0; i < Panels.Length; i++)
            {
                Panels[i]?.SetSelectedAndFade(i == Index);
            }
        }

        // Input
        public override void OnInputRight() { }
        public override void OnInputLeft() { }

        public override void OnInputUp()
        {
            Index = (Index - 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }

        public override void OnInputDown()
        {
            Index = (Index + 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
