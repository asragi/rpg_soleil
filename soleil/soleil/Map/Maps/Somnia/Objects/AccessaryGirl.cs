﻿using Soleil.Event;
using Soleil.Event.Shop;
using Soleil.Item;
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
        readonly Dictionary<ItemID, int> values = new Dictionary<ItemID, int> {
            {ItemID.Stone, 200 },
            {ItemID.Zarigani, 1200 },
            {ItemID.SilverWand, 73000 },
            {ItemID.BeadsWork, 3000 }
        };

        // Event bool
        BoolSet boolSet;
        enum BoolName { Sold, size }

        public AccessaryGirl(Vector pos, ObjectManager om, BoxManager bm)
            : base(pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.SomniaAcceU, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            // Event
            boolSet = new BoolSet((int)BoolName.size);

            EventSequence.SetEventSet(
                new EventSet(
                    new MessageWindowEvent(Pos + WindowPosDiff, 0, "アクセサリー売るよ"),
                    new ShopEvent(values),
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
