using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class MagicMenuPanel : TextSelectablePanel
    {
        public readonly Vector CostPosDiff = new Vector(300, 0);
        public override string Desctiption => ItemName;

        public MagicMenuPanel(String itemName, int num, MagicMenu parent)
            : base(itemName, parent)
        {
            // itemNum
            Val = num;
            LocalPos = Vector.Zero;
        }
    }
}
