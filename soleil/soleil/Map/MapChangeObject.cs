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
        protected EventSequence EventSequence;
        public MapChangeObject((Vector, Vector) pos, MapName mapName, Vector destination, Direction dir,
            ObjectManager om, BoxManager bm, PersonParty party, Camera cam)
            : base(om)
        {
            new CollideLine(this, pos, CollideLayer.RoadEvent, bm);
            Pos = (pos.Item1 + pos.Item2) / 2;
            EventSequence = new EventSequence(om.GetPlayer());
            EventSequence.SetEventSet(
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.None),
                    new CharacterMoveEvent(om.GetPlayer(), dir, 15, false),
                    new FadeOutEvent(),
                    new ChangeMapEvent(mapName, destination, dir, party, cam),
                    new FadeInEvent(),
                    new ChangeInputFocusEvent(InputFocus.Player)
                )
            );
        }

        public override void OnCollisionEnter(CollideObject col)
        {
            base.OnCollisionEnter(col);
            if (col.Layer != CollideLayer.Player) return;
            EventSequence.StartEvent();
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
