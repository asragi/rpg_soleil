using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicMenu : BasicMenu
    {
        SkillHolder holder;
        public MagicMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            // Debug
            holder = new SkillHolder();
            holder.LearnSkill(SkillID.MagicalHeal);
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var magList = new List<MagicMenuPanel>();
            for (int i = 0; i < (int)SkillID.size; i++)
            {
                var id = (SkillID)i;
                if (holder.HasSkill(id))
                {
                    var data = SkillDataBase.Get(id);
                    magList.Add(new MagicMenuPanel(data.Name, i, this));
                }
            }
            return magList.ToArray();
        }
    }
}
