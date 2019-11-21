using Soleil.Event;
using Soleil.Event.Shop;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.Maps.Magistol
{
    class BlackSuit : MapCharacter
    {
        public BlackSuit(Vector pos, PersonParty party, ObjectManager om, BoxManager bm)
            : base("accessary", pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.MobBlackSuit, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            // Event
            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(this, "そうは見えないかもしれないが\nオレはここの卒業生なんだ"),
                    new MessageWindowEvent(this, "20年前から変わってなくて安心するよ"),
                    new MessageWindowEvent(this, "この絵のモデル，\nオレの同期なんだぜ\nすげーよな"),
                    new WaitEvent(20),
                    new MessageWindowEvent(this, "ところで校長先生は留守なのかな？"),
                    new ChangeInputFocusEvent(InputFocus.Player)
                    )
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
