using Soleil.Event;

namespace Soleil
{
    class TestMapJump :MapObject
    {
        CollideBox exi;
        EventSequence eventSequence;
        public TestMapJump(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(1500, 738);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 275), CollideLayer.Character, bm);

            eventSequence = new EventSequence();
            eventSequence.SetEventSet(
                new EventSet(
                    new FadeOutEvent()
                    ,new ChangeMapEvent(om.GetPlayer(), MapName.Somnia1, new Vector(400, 400), 0)
                    )
                );
        }

        public override void OnCollisionEnter()
        {
            eventSequence.StartEvent();
            base.OnCollisionEnter();
        }

        public override void Update()
        {
            eventSequence.Update();
            base.Update();
        }


        public override void OnCollisionExit()
        {
            base.OnCollisionExit();
        }
    }
}
