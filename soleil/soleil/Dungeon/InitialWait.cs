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
        private readonly PlayerObjectWrap player;

        // fields
        private int execFrame;
        private bool playerMove;

        public InitialWait(DungeonMaster _master, PlayerObjectWrap _player)
        {
            master = _master;
            player = _player;

            actions = new Dictionary<int, Action>()
            {
                {80, StopPlayer },
                {120, ModeToFirstWindow }
            };

            Reset();
        }

        public void Reset()
        {
            execFrame = 0;
            playerMove = true;
        }

        public void Exec()
        {
            execFrame++;
            if (actions.ContainsKey(execFrame)) actions[execFrame]();
            if (playerMove) player.ExecInput(Direction.R);
        }

        private void StopPlayer()
        {
            playerMove = false;
            player.SetDirection(Direction.RD);
        }

        private void ModeToFirstWindow() => master.Mode = DungeonMode.FirstWindow;
    }
}
