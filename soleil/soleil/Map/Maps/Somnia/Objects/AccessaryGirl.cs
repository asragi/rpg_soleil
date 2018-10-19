﻿using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.Maps.Somnia
{
    class AccessaryGirl : MapCharacter
    {
        readonly Vector WindowPosDiff = new Vector(-230, -100);
        public AccessaryGirl(Vector pos, ObjectManager om, BoxManager bm)
            : base(pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.SomniaMob1, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos + WindowPosDiff, 0, "アクセサリー売るよ"),
                    new ChangeInputFocusEvent(InputFocus.Player)
                )
            );
        }

        public override void OnCollisionEnter(CollideBox col)
        {
            if (col.Layer == CollideLayer.PlayerHit)
            {
                EventSequence.StartEvent();
            }
            base.OnCollisionEnter(col);
        }
    }
}
