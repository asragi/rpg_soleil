using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonのデータを読み込み実際に処理を行うクラス．
    /// </summary>
    class DungeonExecutor
    {
        private readonly DungeonName name;
        public DungeonExecutor(DungeonName _name)
        {
            name = _name;
        }

        /// <summary>
        /// フロア侵入時の処理．
        /// </summary>
        private void OnEnterFloor(int floorNum)
        {

        }

        /// <summary>
        /// フロア探索時の処理．
        /// </summary>
        private void OnSearch(int floorNum)
        {
            var data = DungeonDatabase.Get(name);
            if (data.HasEvent(floorNum))
            {
                return;
            }
            return;
        }
    }
}
