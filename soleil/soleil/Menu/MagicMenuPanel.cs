﻿using Microsoft.Xna.Framework;
using Soleil.Skill;
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
        private static readonly Vector IconSpace = new Vector(5, 9);
        private readonly Color MagicColor;
        public override string Desctiption => desc;
        private string desc;

        private Image icon;
        public readonly SkillID ID;

        public MagicMenuPanel(ISkill data, MagicMenuBase parent)
            : base(data.Name, parent, DepthID.MenuMessage)
        {
            // itemNum
            Val = data.Cost;
            desc = data.Description;
            LocalPos = Vector.Zero;
            MagicData magicData = (MagicData)data;
            MagicColor = ColorPalette.MagicColors[magicData.Category];
            icon = new Image(MagicIcon.IconMap[magicData.Category], LocalPos + parent.Pos, DepthID.MenuMessage);
            icon.Color = SetColor(MagicColor);
            ID = data.ID;
        }

        protected override void OnSelected()
        {
            base.OnSelected();
            icon.Color = SetColor(SelectedColor);
        }

        protected override void OnUnselected()
        {
            base.OnUnselected();
            icon.Color = SetColor(MagicColor);
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
