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
    class MoveNext
    {
        private readonly Dictionary<int, System.Action> actions;

        DungeonMaster master;
        PlayerObjectWrap player;

        int execFrame;
        public MoveNext(DungeonMaster _master, PlayerObjectWrap _player)
        {
            master = _master;
            player = _player;

            actions = new Dictionary<int, Action>()
            {
                {70, FadeOut },
                {100, MoveNextFloor }
            };
        }

        public void Exec()
        {
            execFrame++;
            if (actions.ContainsKey(execFrame)) actions[execFrame]();
            player.ExecInput(Direction.R);
        }

        public void Reset()
        {
            execFrame = 0;
        }

        private void FadeOut()
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeOut);
        }

        private void MoveNextFloor()
        {
            master.ToNextFloor();
        }
    }
}
