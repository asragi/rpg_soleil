using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event;

namespace Soleil.Map.Maps.Somnia
{
    class BlueGirl : MapCharacter
    {
        public BlueGirl(Vector pos, ObjectManager om, BoxManager bm)
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
