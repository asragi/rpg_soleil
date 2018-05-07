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

            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos, new Vector(200, 76), 0, "テストメッセージ"),
                    new MessageWindowEvent(Pos, new Vector(250, 76), 0, "いい感じになってる？"),
                    new SelectWindowEvent(Pos, new Vector(100, 110), 0, "はい", "いいえ")
                ),
                // GetInstance()はのちのちいい感じにする（どこにwindowManager設置したら書きやすいか悩んでる）
                new BoolEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex() == 1,
                    new EventSet(
                        new MessageWindowEvent(Pos, new Vector(200, 76), 0, "うまくいってそう")),
                    new EventSet(
                        new MessageWindowEvent(Pos, new Vector(220, 76), 0, "本当によくやった"))
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
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
