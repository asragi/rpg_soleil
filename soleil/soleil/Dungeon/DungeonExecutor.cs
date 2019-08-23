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
        private static readonly NothingEvent nothing = new NothingEvent();
        private readonly DungeonName name;
        private DungeonFloorEvent nowEvent;
        public DungeonExecutor(DungeonName _name)
        {
            name = _name;
        }

        public void Update()
        {
            if (nowEvent == null) return;
            if (nowEvent.IsEnd)
            {
                nowEvent = null;
                return;
            }
            nowEvent.Act();
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
                nowEvent = data.GetEvent(floorNum);
                return;
            }
            nowEvent = nothing;
            return;
        }
    }
}
