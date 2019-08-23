using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonに侵入した初めに行われる演出・ウェイト．
    /// </summary>
    class InitialWait
    {
        private readonly Dictionary<int, System.Action> actions;
        // refs
        private readonly DungeonMaster master;

        // fields
        private int execFrame;

        public InitialWait(DungeonMaster _master)
        {
            master = _master;

            actions = new Dictionary<int, Action>()
            {
                {60, ModeToFirstWindow }
            };
        }

        public void Reset()
        {
            execFrame = 0;
        }

        public void Exec()
        {
            execFrame++;
            if (actions.ContainsKey(execFrame)) actions[execFrame]();
        }

        private void ModeToFirstWindow() => master.Mode = DungeonMode.FirstWindow;
    }
}
