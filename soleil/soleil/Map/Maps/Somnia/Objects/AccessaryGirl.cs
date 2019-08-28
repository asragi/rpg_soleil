using Soleil.Event;
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

        // Event bool
        BoolSet boolSet;
        BoolSet preservedBools;
        enum BoolName { First, Sold, size }

        public AccessaryGirl(Vector pos, PersonParty party, ObjectManager om, BoxManager bm)
            : base("accessary", pos, null, om, bm)
        {
            AnimationData anim = new AnimationData(AnimationID.SomniaAcceU, new Vector(-5, -45), true, 8);
            AnimationData[] standAnim = new[] { anim, anim, anim, anim, anim, anim, anim, anim, anim }; // 最悪
            SetStandAnimation(standAnim);

            // Event
            boolSet = new BoolSet((int)BoolName.size);
            preservedBools = GlobalBoolSet.GetBoolSet(BoolObject.Accessary, (int)BoolName.size);

            EventSequence.SetEventSet(
                new BoolEventBranch(EventSequence, () => preservedBools[(int)BoolName.First],
                        new MessageWindowEvent(this, "また会ったね"),
                        new MessageWindowEvent(this, "はじめまして") 
                    ),
                new EventSet(
                    new MessageWindowEvent(this, "アクセサリー売るよ"),
                    new ShopEvent(ShopName.Accessary, party, boolSet, (int)BoolName.Sold)
                ),
                new BoolEventBranch(EventSequence, () => boolSet[(int)BoolName.Sold],
                    new MessageWindowEvent(this, "毎度あり！")
                    ,
                    new BoolEventBranch(EventSequence, () => preservedBools[(int)BoolName.First],
                        new EventSet( new MessageWindowEvent(this, "残念")),
                        new EventSet( new MessageWindowEvent(this, "ちっ\n冷やかしは帰りな"))
                    )
                ),
                new EventSet(
                    new ChangeInputFocusEvent(InputFocus.Player),
                    new BoolSetEvent(preservedBools, (int)BoolName.First, true)
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
