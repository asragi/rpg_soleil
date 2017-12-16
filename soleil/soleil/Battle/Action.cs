using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum ActionName
    {
        NormalAttack,
        ExampleMagic,

        Size,
    }
    class Action
    {
        Func<CharacterStatus, CharacterStatus, float> attack;
        public bool doAttack = false;
        public Action()
        { }

        public Action(Func<CharacterStatus, CharacterStatus, float> _attack)
        {
            doAttack = true;
            attack = _attack;
        }
    }
}
