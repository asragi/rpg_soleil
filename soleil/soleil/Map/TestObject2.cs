﻿using Soleil.Event;

namespace Soleil
{
    class TestObject2 :MapObject
    {
        CollideBox exi;
        EventSequence eventSequence;
        //EventManager eventManager;
        public TestObject2(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(400, 200);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);

            
            //eventManager = new EventManager();

            eventSequence = new EventSequence();
            eventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(pos, new Vector(200, 76), 0, "テストメッセージ"),
                    new MessageWindowEvent(pos, new Vector(250, 76), 0, "いい感じになってる？"),
                    new SelectWindowEvent(pos, new Vector(134, 144), 0, "はい", "いいえ", "わからん")
                ),
                // GetInstance()はのちのちいい感じにする（どこにwindowManager設置したら書きやすいか悩んでる）
                new NumEventBranch(eventSequence, () => WindowManager.GetInstance().GetDecideIndex(),
                    new EventSet(
                        new MessageWindowEvent(pos, new Vector(200, 76), 0, "はいじゃないが")),
                    new EventSet(
                        new MessageWindowEvent(pos, new Vector(312, 76), 0, "いい感じになってるでしょ！！")),
                    new EventSet(
                        new MessageWindowEvent(pos, new Vector(220, 76), 0, "わからんか～～"))
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player)
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
