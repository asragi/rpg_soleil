using Soleil.Event;
using Soleil.Map.WorldMap;

namespace Soleil.Map
{
    class WorldmapObject : MapEventObject
    {
        static readonly string[] options = new[]
        {
            "ワールドマップへ",
            "キャンセル"
        };

        public WorldmapObject(
            string name,
            Vector pos, Vector size, WorldPointKey dest,
            PersonParty pp, ObjectManager om, BoxManager bm
            )
            : base(name, pos, size, om, bm)
        {
            var wm = WindowManager.GetInstance();
            EventSequence.SetEventSet(
                new EventSet(
                        new SelectWindowEvent(Pos, WindowTag.A, options)
                    ),
                new BoolEventBranch(EventSequence, () => wm.GetDecideIndex() == 0,
                    new EventUnit[]{
                        new ToWorldMapEvent(pp, dest)
                    },
                    new EventUnit[] { }
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
                    )
            );
        }

        public override void OnCollisionEnter(CollideObject col)
        {
            if (col.Layer == CollideLayer.PlayerHit)
            {
                EventSequence.StartEvent();
            }
            base.OnCollisionEnter(col);
        }
    }
}
