using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// 戦闘・アイテム獲得などダンジョンの各フロアで起こる出来事を記述する抽象クラス．
    /// </summary>
    abstract class DungeonFloorEvent: ICloneable
    {
        public virtual bool Archived { get; set; }
        public abstract string DisplayName { get; }
        public abstract object Clone();
    }
}
