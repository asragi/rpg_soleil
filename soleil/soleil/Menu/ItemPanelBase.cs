using Microsoft.Xna.Framework;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムに関する情報を取り扱うMenuPanel
    /// </summary>
    abstract class ItemPanelBase :TextSelectablePanel
    {
        public readonly ItemID ID;
        UIImage icon;
        readonly Vector IconSpace = new Vector(8, 10);

        public ItemPanelBase(ItemID id, string name, BasicMenu parent, bool active = true)
            : base(name, parent, active)
        {
            ID = id;
            icon = DecideIcon(ID, parent);
            icon.Color = SetColor(ItemColor);
        }

        private UIImage DecideIcon(ItemID id, BasicMenu parent)
        {
            var itemType = ItemDataBase.Get(id).Type;
            TextureID tex;
            if (itemType == ItemType.Weapon)
            {
                tex = TextureID.IconWand;
            }
            else if (itemType == ItemType.Armor)
            {
                tex = TextureID.IconArmor;
            }
            else if (itemType == ItemType.Accessory)
            {
                tex = TextureID.IconAccessary;
            }
            else if (itemType == ItemType.Consumable)
            {
                tex = TextureID.IconPot;
            }
            else if (itemType == ItemType.Unconsumable)
            {
                tex = TextureID.IconJewel;
            }
            else tex = TextureID.IconAccessary;
            return new UIImage(tex, LocalPos + parent.Pos, Vector.Zero, DepthID.Message);
        }

        protected override void OnSelected()
        {
            base.OnSelected();
            icon.Color = SetColor(SelectedColor);
        }

        protected override void OnUnselected()
        {
            base.OnUnselected();
            icon.Color = SetColor(ItemColor);
        }

        public override void Update()
        {
            base.Update();
            icon.Update();
            icon.Pos = BasicMenu.Pos + IconSpace + LocalPos;
            icon.Alpha = BasicMenu.Alpha;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            icon.Draw(d);
        }

        private Color SetColor(Color col)
        {
            if (!Active) return DisableColor;
            return col;
        }
    }
}
