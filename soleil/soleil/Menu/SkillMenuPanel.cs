﻿using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillMenuPanel : TextSelectablePanel
    {
        public override string Desctiption => desc;
        private string desc;

        public SkillMenuPanel(ISkill data, SkillMenu parent, bool active = true)
            : base(data.Name, parent, DepthID.MenuMessage, active)
        {
            Val = data.Cost;
            desc = data.Description;
            LocalPos = Vector.Zero;
        }
    }
}
