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
        private readonly DungeonMaster master;

        public InitBattle(DungeonMaster _master)
        {
            master = _master;

            Actions = new Dictionary<int, Action>()
            {
                {80, Encounter },
                {120, ChangeScene }
            };
        }

        protected override void Exec() { }

        private void Encounter()
        {
            FadeOut();
        }

        private void ChangeScene()
        {
            master.Mode = DungeonMode.AfterBattle;
            //new TestBattleScene();
            Reset();
        }
    }
}
