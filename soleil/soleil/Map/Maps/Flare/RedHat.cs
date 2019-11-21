using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event;

namespace Soleil.Map.Maps.Flare
{
    class RedHat : MapCharacter
    {
        public RedHat(Vector pos, ObjectManager om, BoxManager bm)
            : base("ciger", pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.MobRedHat, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            EventSequence.SetEventSet(
                new MessageWindowEvent(this, "お店を空けて観光に来たはいいが......"),
                new MessageWindowEvent(this, "誰もいないな！\nもっと賑やかなもんだと思ってたぜ"),
                new MessageWindowEvent(this, "暑くてみんな引きこもっちゃってるのかな？"),
                new MessageWindowEvent(this, "はぁ～～～～～～～～～\nオレのバカンスはどこに......"),
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
