using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class MapChangeObject : MapEventObject
    {
        public MapChangeObject(Vector pos, Vector size, MapName mapName, Vector destination, Direction dir,
            ObjectManager om, BoxManager bm, MapInputManager mim, PersonParty party)
            : base(pos, size, om, bm)
        {
            Pos = pos;
            ExistanceBox.Layer = CollideLayer.RoadEvent;
            EventSequence.SetEventSet(
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.None, mim),
                    new CharacterMoveEvent(om.GetPlayer(), dir, 15, false),
                    new FadeOutEvent(),
                    new ChangeMapEvent(mapName, destination, dir, party),
                    new FadeInEvent(),
                    new ChangeInputFocusEvent(InputFocus.Player, mim)
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
