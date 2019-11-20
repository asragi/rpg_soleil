using Soleil.Event;
using Soleil.Map.WorldMap;

namespace Soleil.Map
{
    class WorldmapObject : MapObject, IEventer
    {
        static readonly string[] options = new[]
        {
            "ワールドマップへ",
            "キャンセル"
        };
        protected EventSequence EventSequence;
        Direction returnDir;

        /// <param name="_returnDir">移動をキャンセルした際に少し歩かせて位置を戻す際の移動方向</param>
        public WorldmapObject(
            (Vector, Vector) pos,WorldPointKey dest,
            Direction _returnDir,
            PersonParty pp, ObjectManager om, BoxManager bm
            )
            : base(om)
        {
            Pos = (pos.Item1 + pos.Item2) / 2;
            returnDir = _returnDir;
            new CollideLine(this, pos, CollideLayer.RoadEvent, bm);
            var wm = WindowManager.GetInstance();
            EventSequence = new EventSequence(om.GetPlayer());
            EventSequence.SetEventSet(
                new EventSet(
                        new SelectWindowEvent(Pos, WindowTag.A, options)
                    ),
                new BoolEventBranch(EventSequence, () => wm.GetDecideIndex() == 0,
                    new ToWorldMapEvent(pp, dest),
                    new CharacterMoveEvent(om.GetPlayer(), returnDir, 15, true)
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
                    )
            );
        }

        public override void Update()
        {
            base.Update();
            EventUpdate();
        }

        public void EventUpdate()
        {
            EventSequence.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            EventSequence.Draw(sb);
        }

        public override void OnCollisionEnter(CollideObject col)
        {
            if (col.Layer == CollideLayer.Player)
            {
                EventSequence.StartEvent();
            }
            base.OnCollisionEnter(col);
        }
    }
}
