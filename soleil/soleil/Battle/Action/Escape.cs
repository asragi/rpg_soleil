using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    using EscapeRate = Func<CharacterStatus, List<CharacterStatus>, float>;

    /// <summary>
    /// 戦闘からの逃亡
    /// </summary>
    class Escape : Action
    {
        /// <summary>
        /// 自分と相手陣営のStatusからEscapeが成功する確率を返す関数
        /// </summary>
        protected EscapeRate ERate;


        public MagicFieldName? MField;
        public Escape(EscapeRate escape,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null, int mp = 0)
            : base(Range.AllEnemy.GetInstance(), mp)
        {
            ERate = escape;
            MField = mField;
        }




        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            Func<Action, List<Occurence>, int, List<Occurence>> func = (act, ocrs, source) =>
            {
                List<CharacterStatus> oppositeStatus = BF.OppositeIndexes(BF.GetSide(source)).Select(p => BF.GetCharacter(p).Status).ToList();
                var rate = ERate(BF.GetCharacter(source).Status, oppositeStatus);

                if (Global.RandomDouble() < rate)
                {
                    //逃亡成功
                    string mes = BF.GetCharacter(source).Name + "は戦闘から逃げ出した";
                    ocrs.Add(new OccurenceBattleEnd(mes));
                }
                else
                {
                    //逃亡失敗
                    string mes = BF.GetCharacter(source).Name + "は逃亡に失敗した";
                    ocrs.Add(new Occurence(mes));
                }

                return ocrs;
            };

            cEffects.Add(new ConditionedEffect(
                (act) => HasSufficientMP,
                (act, ocrs) => func(act, ocrs, ARange.SourceIndex),
                10000));

            return cEffects;
        }
    }
}
