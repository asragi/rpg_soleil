﻿using Microsoft.Xna.Framework;
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
    abstract class ItemPanelBase : TextSelectablePanel
    {
        public readonly ItemID ID;
        Image icon;
        public static readonly Vector IconSpace = new Vector(8, 10);

        public ItemPanelBase(ItemID id, string name, BasicMenu parent, bool active = true)
            : base(name, parent, active)
        {
            ID = id;
            icon = DecideIcon(ID, parent);
            icon.Color = SetColor(ItemColor);
        }

        private Image DecideIcon(ItemID id, BasicMenu parent)
        {
            var itemType = ItemDataBase.Get(id).Type;
            TextureID tex = DecideTexID(itemType);

            return new Image(tex, LocalPos + parent.Pos, Vector.Zero, DepthID.Message);

            TextureID DecideTexID(ItemType item)
            {
                switch (itemType)
                {
                    case ItemType.Consumable:
                        return TextureID.IconPot;
                    case ItemType.Unconsumable:
                        return TextureID.IconJewel;
                    case ItemType.Weapon:
                        return TextureID.IconWand;
                    case ItemType.Armor:
                        return TextureID.IconArmor;
                    case ItemType.Accessory:
                        return TextureID.IconAccessary;
                    default:
                        throw new ArgumentOutOfRangeException("ItemIDが不正です．");
                }
            }
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
