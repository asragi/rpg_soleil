using System;
using System.Collections.Generic;
using System.Linq;

namespace Soleil
{
    public enum BuffRateName
    {
        STRRate = 0,
        VITRate,
        MAGRate,
        SPDRate,
        pATKRate,
        mATKRate,
        pDEFRate,
        mDEFRate,
    }
    public class BuffRate
    {
        public List<float> Rates;
        public BuffRate()
        {
            Rates = Enumerable.Range(0, 8).Select(e => 1.0f).ToList();
        }
        public BuffRate(BuffRate buff)
        {
            Rates = new List<float>(buff.Rates);
        }
        public float this[BuffRateName brName]
        {
            get => Rates[(int)brName];
            set => Rates[(int)brName] = value;
        }

        public BuffRate AddRate(Dictionary<BuffRateName, float> rates)
        {
            var tmp = new BuffRate(this);
            rates.ForEach2(e => { var (k, v) = (e.Key, e.Value); tmp[k] += v; });
            return tmp;
        }
        public BuffRate MultRate(Dictionary<BuffRateName, float> rates)
        {
            var tmp = new BuffRate(this);
            rates.ForEach2(e => { var (k, v) = (e.Key, e.Value); tmp[k] *= v; });
            return tmp;
        }
        public BuffRate IncreaseRate(HashSet<BuffRateName> rates)
        {
            var tmp = new BuffRate(this);
            rates.ForEach2(e =>
            {
                //強化、普通、弱化の3つに対応
                if (tmp[e] < 0.75f)
                    tmp[e] = 1.0f;
                else if (tmp[e] < 1.25f)
                    tmp[e] = 1.5f;
                else
                    tmp[e] = 1.5f;
            });
            return tmp;
        }
        public BuffRate DecreaseRate(HashSet<BuffRateName> rates)
        {
            var tmp = new BuffRate(this);
            rates.ForEach2(e =>
            {
                //強化、普通、弱化の3つに対応
                if (tmp[e] < 0.75f)
                    tmp[e] = 0.5f;
                else if (tmp[e] < 1.25f)
                    tmp[e] = 0.5f;
                else
                    tmp[e] = 1.0f;
            });
            return tmp;
        }

        public int Comp()
        {
            int up = 0, down = 0;
            Rates.ForEach(e =>
            {
                if (e > 1.0f)
                    up++;
                else if (e < 1.0f)
                    down++;
            });

            if (up > 0 && down == 0) return 1;
            else if (down > 0 && up == 0) return -1;
            else return 0;
        }
    }

    /// <summary>
    /// 戦闘中におけるcharaの状態
    /// </summary>
    class CharacterStatus
    {
        public AbilityScore AScore;
        public BuffRate Rates;

        int hp, mp;
        public int HP
        {
            get => hp;
            set => hp = Math.Max(value, 0);
        }
        public int MP
        {
            get => mp;
            set => mp = Math.Max(value, 0);
        }

        public bool Dead { get => HP <= 0; }

        public int STR
        {
            get => Fraction(AScore.STR * Rates[BuffRateName.STRRate]);
        }

        public int VIT
        {
            get => Fraction(AScore.VIT * Rates[BuffRateName.VITRate]);
        }

        public int MAG
        {
            get => Fraction(AScore.MAG * Rates[BuffRateName.MAGRate]);
        }

        public int SPD
        {
            get => Fraction(AScore.SPD * Rates[BuffRateName.SPDRate]);
        }

        float pATK;
        public float PATK
        {
            get => Fraction(pATK * Rates[BuffRateName.pATKRate]);
            private set => pATK = value;
        }

        float mATK;
        public float MATK
        {
            get => Fraction(mATK * Rates[BuffRateName.mATKRate]);
            private set => mATK = value;
        }

        float pDEF;
        public float PDEF
        {
            get => Fraction(pDEF * Rates[BuffRateName.pDEFRate]);
            private set => pDEF = value;
        }

        float mDEF;
        public float MDEF
        {
            get => Fraction(mDEF * Rates[BuffRateName.mDEFRate]);
            private set => mDEF = value;
        }

        public Battle.EquipSet Equips
        {
            get; set;
        }

        public int InitialWP = 10000;
        public int WP = 0;
        public int TurnWP = 10000;

        int turn = 0;
        public int NextWaitPoint()
        {
            return InitialWP + TurnWP * (turn++);
        }

        /// <summary>
        /// float->int への丸め
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static int Fraction(float x)
        {
            return (int)x;
        }


        public CharacterStatus()
        {
            HP = 0;
            MP = 0;

            Equips = new Battle.EquipSet();
            SetParams();
        }

        public CharacterStatus(AbilityScore aScore, int _WP, Battle.EquipSet equips = null)
        {
            AScore = aScore;
            HP = AScore.HPMAX;
            MP = AScore.MPMAX;
            InitialWP = _WP;
            Rates = new BuffRate();

            Equips = equips ?? new Battle.EquipSet();
            SetParams();
        }

        void SetParams()
        {
            PATK = 1f;
            MATK = 1f;
            PDEF = 1f;
            MDEF = 1f;

            //式これで良い？
            PATK += Equips.GetAttack(Skill.AttackType.Physical);
            PDEF += Equips.GetDef(AttackAttribution.Beat, Skill.AttackType.Physical);
            MATK += Equips.GetAttack(Skill.AttackType.Magical);
            MDEF += Equips.GetDef(AttackAttribution.Beat, Skill.AttackType.Magical);
        }


        public void GetSkills()
        {

        }
    }

}