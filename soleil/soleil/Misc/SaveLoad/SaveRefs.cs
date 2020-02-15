using Soleil.Item;
using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    /// <summary>
    /// Save時に参照するインスタンスへの参照を一括で管理する．
    /// </summary>
    class SaveRefs
    {
        // -- Refs
        // Party
        public PersonParty Party { get; set; }
        // Map
        public MapBase NowMap { get; set; }

        // -- Propaties

        // Map
        public ObjectManager ObjectManager => NowMap.ObjectManager;
    }
}
