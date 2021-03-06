﻿using System;
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
            : base("ciger", pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.SomniaMob1, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            EventSequence.SetEventSet(
                new MessageWindowEvent(om.GetPlayer(), "テストメッセージ"),
                new MessageWindowEvent(this, "俺の髪型，なかなかだろ？"),
                new SelectWindowEvent(om.GetPlayer(), "イケイケじゃん", "ハンバーグみたいだね"),
                new BoolEventBranch(EventSequence, () => WindowManager.GetInstance().GetDecideIndex() == 0,
                        new MessageWindowEvent(this, "訊くまでもなかったか"),
                        new MessageWindowEvent(this, "俺の髪型が\nサザエさんみてーだと？？")
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
