using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event;

namespace Soleil.Map.Maps.Somnia
{
    class Cigerman : MapCharacter
    {
        readonly Vector WindowPosDiff = new Vector(30, -100);
        public Cigerman(Vector pos, ObjectManager om, BoxManager bm)
            : base(pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.SomniaMob1, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new []{anim, anim, anim, anim, anim, anim, anim, anim, }; // 最悪
            SetStandAnimation(standAnim);

            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos + WindowPosDiff, 0, "テストメッセージ"),
                    new MessageWindowEvent(Pos + WindowPosDiff, 0, "俺の髪型，なかなかだろ？"),
                    new SelectWindowEvent(Pos + WindowPosDiff, 0, "イケイケじゃん", "ハンバーグみたいだね")
                ),
                new BoolEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex() == 0,
                    new EventSet(
                        new MessageWindowEvent(Pos + WindowPosDiff, 0, "訊くまでもなかったか")),
                    new EventSet(
                        new MessageWindowEvent(Pos + WindowPosDiff, 0, "俺の髪型が\nサザエさんみてーだと？？"))
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
    }
}
