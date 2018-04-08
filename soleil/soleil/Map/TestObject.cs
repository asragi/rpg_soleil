using Soleil.Event;

namespace Soleil
{
    class TestObject :MapObject
    {
        CollideBox exi;
        EventManager eventManager;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
            eventManager = new EventManager();

            new MessageWindowEvent(pos, new Vector(200, 76), 0, "テストメッセージ", eventManager);
            new MessageWindowEvent(pos, new Vector(250, 76), 0, "いい感じになってそう", eventManager);
        }

        public override void OnCollisionEnter()
        {
            eventManager.StartEvent();
            base.OnCollisionEnter();
        }

        public override void Update()
        {
            eventManager.Update();
            base.Update();
        }


        public override void OnCollisionExit()
        {
            base.OnCollisionExit();
        }
    }
}
