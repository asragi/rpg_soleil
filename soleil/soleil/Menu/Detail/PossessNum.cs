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
    class PossessNum
    {
        readonly String ExplainText = "所持数";
        readonly Vector NumDiff = new Vector(220, 0);
        ItemList items = PlayerBaggage.GetInstance().Items;
        TextWithVal withVal;
        public PossessNum(Vector _pos)
        {
            withVal = new TextWithVal(FontID.WhiteOutlineGrad, _pos, (int)NumDiff.X);
        }

        void Refresh(SelectablePanel panel)
        {
            if(panel is MagicMenuPanel)
            {
                withVal.Text = "";
                return;
            }
            if(panel is ItemPanelBase i)
            {
                withVal.Text = ExplainText;
                withVal.Val = items.GetItemNum(i.ID);
            }
        }

        public void Call()
        {
            withVal.Call();
        }

        public void Quit()
        {
            withVal.Quit();
        }

        public void Update(SelectablePanel panel)
        {
            Refresh(panel);
            withVal.Update();
        }

        public void Draw(Drawing d)
        {
            withVal.Draw(d);
        }
    }
}
