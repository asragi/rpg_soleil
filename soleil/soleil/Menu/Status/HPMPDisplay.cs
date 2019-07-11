using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class HPMPDisplay : MenuComponent, IPersonUpdate
    {
        StatusHPMP mp;
        StatusHPMP hp;
        public HPMPDisplay(Vector pos, Vector mpos) // 引数は適当（データが来てから）
        {
            mp = new StatusHPMP(mpos);
            hp = new StatusHPMP(pos);
            AddComponents(new IComponent[] { mp, hp });
        }

        public void RefreshWithPerson(Person p)
        {
            hp.RefreshWithPerson(p, isHP: true);
            mp.RefreshWithPerson(p, isHP: false);
        }
    }
}

