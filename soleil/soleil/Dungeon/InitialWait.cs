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
    class InitialWait: DungeonExec
    {
        // refs
        private readonly DungeonMaster master;
        private readonly PlayerObjectWrap player;

        // fields
        private bool playerMove;

        public InitialWait(DungeonMaster _master, PlayerObjectWrap _player)
        {
            master = _master;
            player = _player;

            Actions = new Dictionary<int, Action>()
            {
                {80, StopPlayer },
                {100, ModeToFirstWindow }
            };

            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            playerMove = true;
        }

        protected override void Exec()
        {
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
