using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    class AfterBattle : DungeonExec
    {
        DungeonMaster master;
        public AfterBattle(DungeonMaster _master)
        {
            master = _master;

            Actions = new Dictionary<int, Action>()
            {
                {1, FadeIn },
                {40, Next }
            };
        }

        protected override void Exec() { }

        private void Next()
        {
            Audio.StopMusic();
            master.Mode = DungeonMode.FirstWindow;
            Reset();
        }
    }
}
