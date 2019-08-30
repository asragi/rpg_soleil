using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeon内部を探索．
    /// </summary>
    class DungeonSearch: DungeonExec
    {
        private readonly DungeonMaster master;
        private readonly DungeonState dState;

        private readonly DungeonExecutor executor;

        public DungeonSearch(
            DungeonMaster _master,
            DungeonState state
            )
        {
            master = _master;
            dState = state;
            executor = new DungeonExecutor(dState.Name);

            Actions = new Dictionary<int, Action>()
            {
                {5, InitSearch },
                {120, NotifyResult }
            };
        }

        protected override void Exec() { }

        private void InitSearch()
        {

        }

        private void NotifyResult()
        {
            master.Mode = executor.OnSearch(dState.FloorNum);
            Console.WriteLine($"Debug Mode is {master.Mode}");
            Reset();
        }
    }
}
