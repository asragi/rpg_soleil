﻿using Soleil.Event;
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
    abstract class MapEventObject : MapObject
    {
        public static Vector DefaultBoxSize = new Vector(30, 30);
        protected CollideBox ExistanceBox;
        protected virtual CollideLayer CollideLayer { get {return CollideLayer.Character; } }
        protected EventSequence EventSequence;

        public MapEventObject(Vector _pos, Vector? _boxSize, ObjectManager om, BoxManager bm)
            :base(om)
        {
            Pos = _pos;
            // boxsizeが指定されていなければ既定の値にする。
            var boxSize = _boxSize ?? DefaultBoxSize;
            ExistanceBox = new CollideBox(this, Vector.Zero, boxSize, CollideLayer, bm);
            EventSequence = new EventSequence();
        }

        virtual public void EventUpdate()
        {
            EventSequence.Update();
        }

        virtual public void Draw(Drawing sb)
        {
            EventSequence.Draw(sb);
        }
    }
}
