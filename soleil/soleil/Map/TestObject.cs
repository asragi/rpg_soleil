using Soleil.Event;

namespace Soleil
{
    class TestObject :MapObject
    {
        CollideBox exi;
        EventSequence eventSequence;
        //EventManager eventManager;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);

            
            //eventManager = new EventManager();


            var testBool = false;
            eventSequence = new EventSequence(
                new EventSet(
                    new MessageWindowEvent(pos, new Vector(200, 76), 0, "テストメッセージ"),
                    new MessageWindowEvent(pos, new Vector(250, 76), 0, "いい感じになってる？")
                    ),
                EventBranch.BoolEventBranch(testBool,
                    new EventSet(
                        new MessageWindowEvent(pos, new Vector(200, 76), 0, "うまくいってそう")),
                    new EventSet(
                        new MessageWindowEvent(pos, new Vector(220, 76), 0, "本当によくやった")
                        )
                    ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
                    ));
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
