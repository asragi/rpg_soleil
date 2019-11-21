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
                {1, SetMusic },
                {2, FadeIn },
                {40, Next }
            };
        }

        protected override void Exec() { }

        private void SetMusic() => Audio.StopMusic();
        private void Next()
        {
            master.Mode = DungeonMode.FirstWindow;
            Reset();
        }
    }
}
