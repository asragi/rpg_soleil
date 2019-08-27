using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョンで次のフロアに移動するまでの演出
    /// </summary>
    class MoveNext: DungeonExec
    {
        DungeonMaster master;
        PlayerObjectWrap player;

        public MoveNext(DungeonMaster _master, PlayerObjectWrap _player)
        {
            master = _master;
            player = _player;

            Actions = new Dictionary<int, Action>()
            {
                {70, FadeOut },
                {100, MoveNextFloor }
            };
        }

        protected override void Exec()
        {
            player.ExecInput(Direction.R);
        }

        private void MoveNextFloor()
        {
            master.ToNextFloor();
        }
    }
}
