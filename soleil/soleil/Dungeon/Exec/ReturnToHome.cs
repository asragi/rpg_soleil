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
    class ReturnToHome: DungeonExec
    {
        private readonly PlayerObjectWrap player;
        private readonly SceneManager sceneManager;
        private readonly PersonParty party;

        private readonly DungeonName dungeonName;

        public ReturnToHome(
            PlayerObjectWrap _player, DungeonName name,
            SceneManager manager, PersonParty _party
            )
        {
            player = _player;
            sceneManager = manager;
            party = _party;
            dungeonName = name;

            Actions = new Dictionary<int, Action>()
            {
                {80, FadeOut },
                {110, ChangeScene }
            };
        }

        protected override void Exec()
        {
            player.ExecInput(Direction.L);
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
