using Soleil.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョン探索時に戦闘が発生したときの処理．
    /// Mode == InitBattleに対応．
    /// </summary>
    class InitBattle: DungeonExec
    {
        private readonly SceneManager sceneManager;
        private readonly DungeonMaster master;
        private PersonParty party;
        private BattleData data;

        public InitBattle(DungeonMaster _master, PersonParty p, SceneManager sm)
        {
            master = _master;
            sceneManager = sm;
            party = p;

            Actions = new Dictionary<int, System.Action>()
            {
                {80, Encounter },
                {120, ChangeScene }
            };
        }

        protected override void Exec() { }

        public void SetBattle(BattleData _data)
        {
            data = _data;
        }

        private void Encounter()
        {
            FadeOut();
        }

        private void ChangeScene()
        {
            master.Mode = DungeonMode.AfterBattle;
            Console.WriteLine($"{sceneManager}, {party.GetActiveMembers().Length}, {data.Enemies[0].ToString()}");
            new TestBattleScene(sceneManager, party, data.Enemies);
            Reset();
        }
    }
}
