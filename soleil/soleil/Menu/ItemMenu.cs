using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : BasicMenu
    {
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            // 実際は所持アイテムのデータから生成する
            Panels = new ItemPanel[]{
                new ItemPanel("ハイポーション", 2, this),
                new ItemPanel("エーテル", 3, this),
                new ItemPanel("フェニックスの手羽先", 3, this),
                new ItemPanel("活きのいいザリガニ", 2, this),
                new ItemPanel("セミの抜け殻", 1, this),
                new ItemPanel("きれいな石", 99, this),
            };

            for (int i = 0; i < Panels.Length; ++i)
            {
                Panels[i].LocalPos = ItemDrawStartPos + new Vector(0, (Panels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
        }
    }
}
