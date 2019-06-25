using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// プレイヤーが施設と施設の間を移動している間の処理を行うクラス．
    /// </summary>
    class WorldMapMove
    {
        const int MoveDuration = 70;
        readonly Func<double, double, double, double, double> EaseFunc = Easing.Linear;
        int frame;
        Vector from, to;

        public WorldMapInputMode Update(WorldMapInputMode mode)
        {
            frame++;
            if (mode != WorldMapInputMode.Move) return mode;
            if (frame >= MoveDuration) return WorldMapInputMode.InitWindow;
            return WorldMapInputMode.Move;
        }

        public Vector GetEasingPosition()
        {
            if (frame >= MoveDuration) return to;
            var x = EaseFunc(frame, MoveDuration, from.X, to.X);
            var y = EaseFunc(frame, MoveDuration, from.Y, to.Y);
            return new Vector(x, y);
        }

        public void MoveFromTo(WorldPoint pointFrom, WorldPoint pointTo)
        {
            from = pointFrom.Pos;
            to = pointTo.Pos;
            frame = 0;
        }
    }
}
