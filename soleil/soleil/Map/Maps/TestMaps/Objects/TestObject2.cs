using Soleil.Event;

namespace Soleil.Map
{
    class TestObject2 :MapObject
    {
        CollideBox exi;
        public TestObject2(ObjectManager om, BoxManager bm)
            : base(om)
        {
            Pos = new Vector(400, 200);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);

            
            EventSequence = new EventSequence();
            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos, new Vector(200, 76), 0, "テストメッセージ"),
                    new MessageWindowEvent(Pos, new Vector(250, 76), 0, "いい感じになってる？"),
                    new SelectWindowEvent(Pos, new Vector(134, 144), 0, "はい", "いいえ", "わからん")
                ),
                // GetInstance()はのちのちいい感じにする（どこにwindowManager設置したら書きやすいか悩んでる）
                new NumEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex(),
                    new EventSet(
                        new MessageWindowEvent(Pos, new Vector(200, 76), 0, "はいじゃないが")),
                    new EventSet(
                        new MessageWindowEvent(Pos, new Vector(312, 76), 0, "いい感じになってるでしょ！！")),
                    new EventSet(
                        new MessageWindowEvent(Pos, new Vector(220, 76), 0, "わからんか～～"))
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
