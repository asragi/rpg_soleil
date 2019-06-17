using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillMenu: BasicMenu
    {
        SkillHolder skillHolder;
        public SkillMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            skillHolder = new Skill.SkillHolder();

        }
    }
}
