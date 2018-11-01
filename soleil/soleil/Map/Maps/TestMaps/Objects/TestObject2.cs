using Soleil.Event;

namespace Soleil.Map
{
    class TestObject2 :MapEventObject
    {
        public TestObject2(ObjectManager om, BoxManager bm)
            : base(new Vector(400, 200), null, om, bm)
        {
            Pos = new Vector(400, 200);

            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos, 0, "テストメッセージ"),
                    new MessageWindowEvent(Pos, 0, "いい感じになってる？"),
                    new SelectWindowEvent(Pos,　0, "はい", "いいえ", "わからん")
                ),
                // GetInstance()はのちのちいい感じにする（どこにwindowManager設置したら書きやすいか悩んでる）
                new NumEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex(),
                    new EventSet(
                        new MessageWindowEvent(Pos, 0, "はいじゃないが")),
                    new EventSet(
                        new MessageWindowEvent(Pos, 0, "いい感じになってるでしょ！！")),
                    new EventSet(
                        new MessageWindowEvent(Pos, 0, "わからんか～～"))
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
                )
            );
        }

        public override void OnCollisionEnter(CollideBox col)
        {
            EventSequence.StartEvent();
            base.OnCollisionEnter(col);
        }
    }
}
