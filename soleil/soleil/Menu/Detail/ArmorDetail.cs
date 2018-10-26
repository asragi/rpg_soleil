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
    /// 防具の詳細性能表示クラス．
    /// </summary>
    class ArmorDetail : DetailComponent
    {
        readonly String ExplainText = "防御力";
        readonly int Space = 220;
        readonly Vector InitPos;

        TextWithVal defExplain;
        public ArmorDetail(Vector _pos)
            :base()
        {
            InitPos = _pos;
            defExplain = new TextWithVal(FontID.Test, _pos, Space, ExplainText);
        }

        public void Call()
        {
            defExplain.Call();
        }

        public void Quit()
        {
            defExplain.Quit();
        }

        private void Refresh(SelectablePanel panel)
        {
            if (!(panel is ItemPanelBase)) return;
            var itemPanel = (ItemPanelBase)panel;
            
            var data = ItemDataBase.Get(itemPanel.ID);
            var type = ItemDataBase.Get(itemPanel.ID).Type;

            if(type == ItemType.Accessory || type == ItemType.Armor)
            {
                defExplain.Enable = true;
                defExplain.EnableValDisplay = true;
                defExplain.Val = ((IArmor)data).DefData.Physical;
                return;
            }
            if(type == ItemType.Weapon)
            {
                defExplain.Enable = true;
                var val = ((IArmor)data).DefData.Physical;
                defExplain.Val = val;
                // 防御力性能を持たないなら非表示
                defExplain.Enable = val != 0;
                return;
            }
            // 装備でない
            defExplain.Enable = false;            
        }

        public void Update(SelectablePanel panel)
        {
            base.Update();
            Refresh(panel);
            defExplain.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            defExplain.Draw(d);
        }
    }
}
