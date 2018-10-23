using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicMenu : BasicMenu
    {
        public MagicMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            return new MagicMenuPanel[]{
                new MagicMenuPanel("サンダーボルト", 8, this),
                new MagicMenuPanel("マジカルヒール", 40, this),
                new MagicMenuPanel("エクスプロード", 16, this),
                new MagicMenuPanel("ルナティックレイ", 66, this),
            };
        }
    }
}
