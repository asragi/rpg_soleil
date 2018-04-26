using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    enum CollideLayer
    {
        Player,
        Wall,
        Character,

        /// <summary>
        /// プレイヤーと重なる接触型イベントフラグ
        /// </summary>
        RoadEvent,
    }
}
