﻿using Soleil.Skill;
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
        public override string Desctiption => desc;
        private string desc;

        public MagicMenuPanel(ISkill data, MagicMenu parent)
            : base(data.Name, parent)
        {
            // itemNum
            Val = data.Cost;
            desc = data.Description;
            LocalPos = Vector.Zero;
        }
    }
}
