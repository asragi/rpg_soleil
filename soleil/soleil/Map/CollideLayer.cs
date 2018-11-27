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
        PlayerBox,
        PlayerHit, // プレイヤーが決定キーを押したときに出る判定
        Wall,
        Character,

        /// <summary>
        /// プレイヤーと重なる接触型イベントフラグ
        /// </summary>
        RoadEvent,
    }
}
