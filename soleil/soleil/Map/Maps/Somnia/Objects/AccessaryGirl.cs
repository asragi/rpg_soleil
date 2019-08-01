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
            preservedBools = GlobalBoolSet.GetBoolSet(BoolObject.Accessary,(int)BoolName.size);

            EventSequence.SetEventSet(
                new BoolEventBranch(EventSequence, () => preservedBools[(int)BoolName.First],
                    new EventUnit[] {
                    new EventSet(
                        new MessageWindowEvent(Pos + WindowPosDiff, 0, "また会ったね")) },
                    new EventUnit[] {
                    new EventSet(
                        new MessageWindowEvent(Pos + WindowPosDiff, 0, "はじめまして")) }),
                new EventSet(
                    new MessageWindowEvent(Pos + WindowPosDiff, 0, "アクセサリー売るよ"),
                    new ShopEvent(ShopName.Accessary, party, boolSet, (int)BoolName.Sold)
                ),
                new BoolEventBranch(EventSequence, () => boolSet[(int)BoolName.Sold],
                    new EventUnit[] {
                        new EventSet(
                            new MessageWindowEvent(Pos + WindowPosDiff, 0, "毎度あり！"))
                    },
                    new EventUnit[] {
                        new BoolEventBranch(EventSequence, () => preservedBools[(int)BoolName.First],
                        new EventUnit[]
                        {
                            new EventSet( new MessageWindowEvent(Pos + WindowPosDiff, 0, "残念")),
                        },
                        new EventUnit[]{
                            new EventSet( new MessageWindowEvent(Pos + WindowPosDiff, 0, "ちっ\n冷やかしは帰りな")),
                        })
                    }),
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
