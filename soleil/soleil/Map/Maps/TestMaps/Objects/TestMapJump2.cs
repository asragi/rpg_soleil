using Soleil.Event;

namespace Soleil.Map
{
    class TestMapJump2 :MapObject
    {
        CollideBox exi;
        public TestMapJump2(ObjectManager om, BoxManager bm)
            : base(om)
        {
            Pos = new Vector(0, 740);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 275), CollideLayer.Character, bm);

            EventSequence.SetEventSet(
                new EventSet(
                    new FadeOutEvent()
                    ,new ChangeMapEvent(MapName.Somnia2, new Vector(1400, 700), 0)
                    ,new FadeInEvent()
                    )
                );
        }

        public override void OnCollisionEnter(CollideBox col)
        {
            EventSequence.StartEvent();
            base.OnCollisionEnter(col);
        }

        public override void Update()
        {
            base.Update();
        }


        public override void OnCollisionExit()
        {
            base.OnCollisionExit();
        }
    }
}
