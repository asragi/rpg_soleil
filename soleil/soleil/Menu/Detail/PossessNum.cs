using Soleil.Event.Shop;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Detail
{
    /// <summary>
    /// 所持数の表示
    /// </summary>
    class PossessNum: MenuComponent
    {
        readonly String ExplainText = "所持数";
        public static readonly Vector NumDiff = new Vector(120, 0);
        ItemList items = PlayerBaggage.GetInstance().Items;
        TextWithVal withVal;
        public PossessNum(Vector _pos)
        {
            withVal = new TextWithVal(DetailWindow.Font, _pos, (int)NumDiff.X);
            AddComponents(new[] { withVal });
        }

        public void Refresh(SelectablePanel panel)
        {
            if (panel is MagicMenuPanel)
            {
                withVal.Text = "";
                return;
            }
            if (panel is ItemPanelBase i)
            {
                withVal.Text = ExplainText;
                withVal.Val = items.GetItemNum(i.ID);
            }
        }
    }
}
