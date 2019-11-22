using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Battle;
using Soleil.Event;

namespace Soleil.Map.Maps.Somnia
{
    class BlueGirl : MapCharacter
    {
        public BlueGirl(Vector pos, ObjectManager om, BoxManager bm, PersonParty party)
            : base("ciger", pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.MobBlueGirl, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            EventSequence.SetEventSet(
                new MessageWindowEvent(this, "アイセンヘルグっていう孤島から来たんだけど"),
                new MessageWindowEvent(this, "ここはすごく雰囲気悪いね"),
                new MessageWindowEvent(this, "......"),
                new MessageWindowEvent(this, "すっごくワクワクするね！！"),
                new WaitEvent(20),
                new MessageWindowEvent(this, "そうだ！ちょっとペットを連れてきてるんだけど，\nよかったら見ていかない？"),
                new SelectWindowEvent(this, "はい", "いいえ"),
                new BoolEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex() == 0,
                        new EventUnit[] {
                            new BattleInitEvent(new BattleData(CharacterType.TestEnemy, CharacterType.TestEnemy), party)
                        },
                        new EventUnit[] {
                            new MessageWindowEvent(this, "そっか")
                        }
                ),
                new ChangeInputFocusEvent(InputFocus.Player)
            );
        }

        public override void OnCollisionEnter(CollideObject col)
        {
            if (col.Layer == CollideLayer.PlayerHit)
            {
                EventSequence.StartEvent();
            }
            base.OnCollisionEnter(col);
        }
    }
}
