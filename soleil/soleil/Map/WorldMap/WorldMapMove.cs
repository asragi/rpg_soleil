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
        readonly static Func<double, double, double, double, double> EaseFunc = Easing.Linear;
        readonly WorldMap worldMap;
        readonly WorldMapCamera camera;

        int frame;
        Vector from, to;
        WorldPointKey destination;

        public WorldMapMove(WorldMap map, WorldMapCamera cam)
        {
            worldMap = map;
            camera = cam;
            Vector nowPosition = map.GetPlayerPoint().Pos;
            from = nowPosition;
            to = nowPosition;
        }

        public WorldMapMode Update(WorldMapMode mode, WorldMapWindowLayer windowLayer)
        {
            frame++;
            worldMap.SetPlayerPos(GetEasingPosition(frame, MoveDuration, from, to));
            if (mode != WorldMapMode.Move) return mode;
            if (frame >= MoveDuration)
            {
                camera.SetDestination(to);
                worldMap.SetPlayerPosition(destination);
                windowLayer.InitWindow();
                return WorldMapMode.InitWindow;
            }
            return WorldMapMode.Move;
        }

        private static Vector GetEasingPosition(int _frame, int max, Vector _from, Vector _to)
        {
            if (_frame >= max) return _to;
            var x = EaseFunc(_frame, max, _to.X, _from.X);
            var y = EaseFunc(_frame, max, _to.Y, _from.Y);
            return new Vector(x, y);
        }

        public void MoveFromTo(WorldPoint pointFrom, WorldPoint pointTo)
        {
            from = pointFrom.Pos;
            to = pointTo.Pos;
            destination = pointTo.ID;
            frame = 0;
            camera.SetDestination((from + to) / 2);
        }
    }
}
