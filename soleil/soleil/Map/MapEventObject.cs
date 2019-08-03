using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 接触用の矩形をもちイベントを起こすもの
    /// </summary>
    abstract class MapEventObject : MapObject, IEventer
    {
        public readonly string Name;
        public static Vector DefaultBoxSize = new Vector(30, 30);
        protected CollideBox ExistanceBox;
        protected virtual CollideLayer CollideLayer { get {return CollideLayer.Character; } }
        protected EventSequence EventSequence;
        protected PlayerObject Player;

        public MapEventObject(string name, Vector _pos, Vector? _boxSize, ObjectManager om, BoxManager bm)
            :base(om)
        {
            Name = name;
            Pos = _pos;
            Player = om.GetPlayer();
            // boxsizeが指定されていなければ既定の値にする。
            var boxSize = _boxSize ?? DefaultBoxSize;
            ExistanceBox = new CollideBox(this, Vector.Zero, boxSize, CollideLayer, bm);
            EventSequence = new EventSequence(Player);
        }

        public override void Update()
        {
            base.Update();
            EventUpdate();
        }

        virtual public void EventUpdate()
        {
            EventSequence.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            EventSequence.Draw(sb);
        }
    }
}
