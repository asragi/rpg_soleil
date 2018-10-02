using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class MapChangeObject : MapObject
    {
        CollideBox collide;
        public MapChangeObject(Vector pos, Vector size, MapName mapName, Vector destination, Direction dir,
            ObjectManager om, BoxManager bm)
            : base(om)
        {
            Pos = pos;
            collide = new CollideBox(this, Vector.Zero, size, CollideLayer.RoadEvent, bm);
            EventSequence.SetEventSet(
                new EventSet(
                    new FadeOutEvent()
                    , new ChangeMapEvent(mapName, destination, dir)
                    , new FadeInEvent()
                )
            );
        }

        public override void OnCollisionEnter(CollideBox col)
        {
            base.OnCollisionEnter(col);
            if (col.Layer != CollideLayer.Player) return;
            EventSequence.StartEvent();
        }
    }
}
