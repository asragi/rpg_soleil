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
            Vector pos, Vector size, WorldPointKey dest,
            PersonParty pp, SceneManager sm, ObjectManager om, BoxManager bm
            )
            :base (pos, size, om, bm)
        {
            var wm = WindowManager.GetInstance();
            EventSequence.SetEventSet(
                new EventSet(
                        new SelectWindowEvent(Pos, WindowTag.A, options)
                    ),
                new BoolEventBranch(EventSequence, () => wm.GetDecideIndex() == 0,
                    new EventUnit[]{
                        new ToWorldMapEvent(sm, pp, dest)
                    },
                    new EventUnit[]{
                        new ChangeInputFocusEvent(InputFocus.Player)
                    }
                )
            );
        }
    }
}
