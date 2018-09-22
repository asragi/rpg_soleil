using Soleil.Event;

namespace Soleil.Map
{
    class TestObject :MapObject
    {
        CollideBox exi;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            Pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
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
