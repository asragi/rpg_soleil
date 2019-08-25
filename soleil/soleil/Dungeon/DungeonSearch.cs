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

        public DungeonSearch(DungeonMaster _master)
        {
            master = _master;

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
            master.Mode = DungeonMode.FirstWindow;
            Reset();
        }
    }
}
