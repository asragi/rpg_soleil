using Soleil.Event;

namespace Soleil
{
    class TestMapJump :MapObject
    {
        CollideBox exi;
        public TestMapJump(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(1500, 738);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 275), CollideLayer.Character, bm);

            EventSequence.SetEventSet(
                new EventSet(
                    new FadeOutEvent()
                    ,new ChangeMapEvent(MapName.Somnia1, new Vector(70, 700), 0)
                    ,new FadeInEvent()
                    )
                );
        }

        public override void OnCollisionEnter()
        {
            EventSequence.StartEvent();
            base.OnCollisionEnter();
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
