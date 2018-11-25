using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class HPMPDisplay : MenuComponent
    {
        StatusMP mp;
        StatusHP hp;
        public HPMPDisplay(Vector pos, int val, Vector mpos, int val2, int val3) // 引数は適当（データが来てから）
        {
            mp = new StatusMP(mpos, val2, val3);
            hp = new StatusHP(pos, val);
            AddComponents(new IComponent[] { mp, hp });
        }
    }
}

