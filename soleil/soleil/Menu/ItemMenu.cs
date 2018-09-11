using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : MenuChild
    {
        const int FadeSpeed = 35;
        const int RowSize = 8; // 現在のフォントサイズだと8項目がちょうどよい
        readonly Vector WindowPos = new Vector(330, 100);
        readonly Vector WindowStartPos = new Vector(830, 100);

        readonly Vector ItemDrawStartPos = new Vector(25, 28);
        readonly int ItemPanelSpacing = 4;
        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;

        Image backImage;
        ItemPanel[] itemPanels;
        public Vector Pos { get { return backImage.Pos; } }
        int index;
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuModalBack), WindowStartPos, DepthID.MessageBack, false, true, 0);

            // 実際は所持アイテムのデータから生成する
            itemPanels = new ItemPanel[]{
                new ItemPanel("ハイポーション", 2, this),
                new ItemPanel("エーテル", 3, this),
                new ItemPanel("フェニックスの手羽先", 3, this),
                new ItemPanel("活きのいいザリガニ", 2, this),
                new ItemPanel("セミの抜け殻", 1, this),
                new ItemPanel("きれいな石", 99, this),
            };

            for (int i = 0; i < itemPanels.Length; ++i)
            {
                itemPanels[i].LocalPos = ItemDrawStartPos + new Vector(0, (itemPanels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }

            index = 0;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            backImage.MoveTo(WindowStartPos, FadeSpeed, EaseFunc);
            backImage.Fade(FadeSpeed, EaseFunc, false);

            foreach (var item in itemPanels)
            {
                item.MoveTo(WindowStartPos + item.LocalPos, FadeSpeed, EaseFunc);
                item.Fade(FadeSpeed, EaseFunc, false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            backImage.MoveTo(WindowPos, FadeSpeed, EaseFunc);
            backImage.Fade(FadeSpeed, EaseFunc, true);
            foreach (var item in itemPanels)
            {
                item.MoveTo(WindowPos + item.LocalPos, FadeSpeed, EaseFunc);
                item.Fade(FadeSpeed, EaseFunc, true);
            }
            RefreshSelected();
        }

        public override void Update()
        {
            base.Update();
            backImage.Update();
            foreach (var item in itemPanels)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            backImage.Draw(d);
            foreach (var item in itemPanels)
            {
                item.Draw(d);
            }
        }

        private void RefreshSelected()
        {
            for (int i = 0; i < itemPanels.Length; i++)
            {
                itemPanels[i].SetSelectedAndFade(i == index);
            }
        }

        // Input
        public override void OnInputRight() { }
        public override void OnInputLeft() { }

        public override void OnInputUp()
        {
            index = (index - 1 + itemPanels.Length) % itemPanels.Length;
            RefreshSelected();
        }

        public override void OnInputDown()
        {
            index = (index + 1 + itemPanels.Length) % itemPanels.Length;
            RefreshSelected();
        }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
