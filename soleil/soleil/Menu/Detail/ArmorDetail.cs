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
        readonly int Space = 100;
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
            if(panel is ItemPanelBase p)
            {
                var data = ItemDataBase.Get(p.ID);
                var type = ItemDataBase.Get(p.ID).Type;
                if (type != ItemType.Armor && type != ItemType.Accessory) return;
                defExplain.Val = ((IArmor)data).DefData.Physical;
            }
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
