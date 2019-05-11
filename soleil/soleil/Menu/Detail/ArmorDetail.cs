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
        readonly string AtkExpText = "攻撃力";
        readonly string ExplainText = "防御力";
        readonly Vector DefExpPos = new Vector(0, 60);
        readonly int Space = 220;
        readonly Vector InitPos;

        TextWithVal atkExplain;
        TextWithVal defExplain;
        public ArmorDetail(Vector _pos)
            :base()
        {
            InitPos = _pos;
            var font = DetailWindow.Font;
            atkExplain = new TextWithVal(font, _pos, Space, AtkExpText);
            defExplain = new TextWithVal(font, _pos+DefExpPos, Space, ExplainText);
        }

        public void Call()
        {
            atkExplain.Call();
            defExplain.Call();
        }

        public void Quit()
        {
            atkExplain.Quit();
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
                // 攻撃性能を非表示に
                atkExplain.Enable = false; // 攻撃力が上がる防具......？

                // 防御力表示
                defExplain.Enable = true;
                defExplain.EnableValDisplay = true;
                defExplain.Val = ((IArmor)data).DefData.Physical;
                return;
            }
            if(type == ItemType.Weapon)
            {
                // 攻撃力表示設定
                atkExplain.Enable = true;
                var aVal = ((WeaponData)data).AttackData.Magical; // 魔法の世界なので "攻撃力" => mATK
                atkExplain.Val = aVal;

                // 防御力表示
                defExplain.Enable = true;
                var val = ((IArmor)data).DefData.Physical;
                defExplain.Val = val;
                // 防御力性能を持たないなら非表示
                defExplain.Enable = val != 0;
                return;
            }
            // 装備でない
            atkExplain.Enable = false;
            defExplain.Enable = false;            
        }

        public void Update(SelectablePanel panel)
        {
            base.Update();
            Refresh(panel);
            atkExplain.Update();
            defExplain.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            atkExplain.Draw(d);
            defExplain.Draw(d);
        }
    }
}
