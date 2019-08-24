using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョンの選択で入口に戻る際の処理
    /// </summary>
    class ReturnToHome
    {
        private readonly Dictionary<int, System.Action> actions;
        private readonly PlayerObjectWrap player;
        private readonly SceneManager sceneManager;
        private readonly PersonParty party;

        private readonly DungeonName dungeonName;
        private int execFrame;

        public ReturnToHome(
            PlayerObjectWrap _player, DungeonName name,
            SceneManager manager, PersonParty _party
            )
        {
            player = _player;
            sceneManager = manager;
            party = _party;
            dungeonName = name;
            execFrame = 0;

            actions = new Dictionary<int, Action>()
            {
                {80, FadeOut },
                {110, ChangeScene }
            };
        }

        public void Exec()
        {
            execFrame++;
            if (actions.ContainsKey(execFrame)) actions[execFrame]();
            player.ExecInput(Direction.L);
        }

        private void FadeOut()
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeOut);
        }

        private void ChangeScene()
        {
            var data = DungeonDatabase.Get(dungeonName);
            var mapName = data.EntranceName;
            var destination = data.EntrancePos;
            new MapScene(sceneManager, party, mapName, destination);
            // sceneManager.KillNowScene();
        }
    }
}
