using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicMenu : BasicMenu
    {
        public MagicMenu(MenuComponent parent)
            : base(parent)
        {
            // 実際はキャラクターのデータから生成する
            Panels = new MagicMenuPanel[]{
                new MagicMenuPanel("サンダーボルト", 8, this),
                new MagicMenuPanel("マジカルヒール", 40, this),
                new MagicMenuPanel("エクスプロード", 16, this),
                new MagicMenuPanel("ルナティックレイ", 66, this),
            };

            for (int i = 0; i < Panels.Length; ++i)
            {
                Panels[i].LocalPos = ItemDrawStartPos + new Vector(0, (Panels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
        }
    }
}
