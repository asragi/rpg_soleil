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
        }

        public override void Call()
        {
            base.Call();
            mp.Call();
            hp.Call();
        }

        public override void Quit()
        {
            base.Quit();
            mp.Quit();
            hp.Quit();
        }

        public override void Update()
        {
            base.Update();
            mp.Update();
            hp.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            mp.Draw(d);
            hp.Draw(d);
        }
    }
}

