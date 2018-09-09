using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : MenuChild
    {
        const int RowSize = 8; // 現在のフォントサイズだと8項目がちょうどよい
        readonly Vector WindowPos = new Vector(330, 100);
        readonly Vector WindowStartPos = new Vector(830, 100);

        readonly Vector ItemDrawStartPos = new Vector(25, 28);
        readonly int ItemPanelSpacing = 4;
        Image backImage;
        ItemPanel[] itemPanels;

        int index;
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            // 実際は所持アイテムのデータから生成する
            itemPanels = new ItemPanel[]{
                new ItemPanel("ハイポーション", 2),
                new ItemPanel("エーテル", 3),
                new ItemPanel("フェニックスの手羽先", 3),
                new ItemPanel("活きのいいザリガニ", 2),
                new ItemPanel("セミの抜け殻", 1),
                new ItemPanel("きれいな石", 99),
            };

            for (int i = 0; i < itemPanels.Length; ++i)
            {
                itemPanels[i].Pos = ItemDrawStartPos + new Vector(0, (itemPanels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }

            backImage = new Image(0, Resources.GetTexture(TextureID.MenuModalBack), WindowStartPos, DepthID.MessageBack, false, true, 0);

            index = 0;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            backImage.MoveTo(WindowStartPos, 35, Easing.OutCubic);
            backImage.Fade(35, Easing.OutCubic, false);

            foreach (var item in itemPanels)
            {
                item.MoveTo(WindowStartPos + item.Pos, 35, Easing.OutCubic);
                item.Fade(35, Easing.OutCubic, false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            backImage.MoveTo(WindowPos, 35, Easing.OutCubic);
            backImage.Fade(35, Easing.OutCubic, true);
            foreach (var item in itemPanels)
            {
                item.MoveTo(WindowPos + item.Pos, 35, Easing.OutCubic);
                item.Fade(35, Easing.OutCubic, true);
            }
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

        public override void OnInputRight() { }
        public override void OnInputLeft() { }

        public override void OnInputUp()
        {
            index = (index - 1 + itemPanels.Length) % itemPanels.Length;
            for (int i = 0; i < itemPanels.Length; i++)
            {
                itemPanels[i].SetSelected(i == index);
            }
        }

        public override void OnInputDown()
        {
            index = (index + 1 + itemPanels.Length) % itemPanels.Length;
            for (int i = 0; i < itemPanels.Length; i++)
            {
                itemPanels[i].SetSelected(i == index);
            }
        }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
