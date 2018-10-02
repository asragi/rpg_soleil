using System;
using System.Collections.Generic;
using System.Linq;

namespace Soleil
{
    public class BuffRate
    {
        public float STRrate;
        public float VITrate;
        public float MAGrate;
        public float SPDrate;
        public BuffRate(float STRrate = 1.0f, float VITrate = 1.0f, float MAGrate = 1.0f, float SPDrate = 1.0f)
        {
            this.STRrate = STRrate;
            this.VITrate = VITrate;
            this.MAGrate = MAGrate;
            this.SPDrate = SPDrate;
        }
        public BuffRate(BuffRate buff)
        {
            this.STRrate = buff.STRrate;
            this.VITrate = buff.VITrate;
            this.MAGrate = buff.MAGrate;
            this.SPDrate = buff.SPDrate;
        }
        public BuffRate AddRate(float STRrate = 0.0f, float VITrate = 0.0f, float MAGrate = 0.0f, float SPDrate = 0.0f)
        {
            var tmp = new BuffRate();
            tmp.STRrate = this.STRrate + STRrate;
            tmp.VITrate = this.VITrate + VITrate;
            tmp.MAGrate = this.MAGrate + MAGrate;
            tmp.SPDrate = this.SPDrate + SPDrate;
            return tmp;
        }
        public BuffRate MultRate(float STRrate = 1.0f, float VITrate = 1.0f, float MAGrate = 1.0f, float SPDrate = 1.0f)
        {
            var tmp = new BuffRate();
            tmp.STRrate = this.STRrate * STRrate;
            tmp.VITrate = this.VITrate * VITrate;
            tmp.MAGrate = this.MAGrate * MAGrate;
            tmp.SPDrate = this.SPDrate * SPDrate;
            return tmp;
        }
        public int Comp()
        {
            int up = 0, down = 0;
            if (STRrate > 1.0f) up++;
            if (STRrate < 1.0f) down++;

            if (VITrate > 1.0f) up++;
            if (VITrate < 1.0f) down++;

            if (MAGrate > 1.0f) up++;
            if (MAGrate < 1.0f) down++;

            if (SPDrate > 1.0f) up++;
            if (SPDrate < 1.0f) down++;

            if (up > 0 && down == 0) return 1;
            else if (down > 0 && up == 0) return -1;
            else return 0;
        }
    }
    public class CharacterStatus
    {
        public AbilityScore abilityScore;
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
            get
            {
                return Fraction(abilityScore.STR * Rates.STRrate);
            }
        }
        
        public int VIT
        {
            get
            {
                return Fraction(abilityScore.VIT * Rates.VITrate);
            }
        }

        public int MAG
        {
            get
            {
                return Fraction(abilityScore.MAG * Rates.MAGrate);
            }
        }
        
        public int SPD
        {
            get
            {
                return Fraction(abilityScore.SPD * Rates.SPDrate);
            }
        }

        public int WP = 10000;

        int turn = 0;
        public int NextWaitPoint()
        {
            return WP + 10000 * (turn++);
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
        }

        public CharacterStatus(AbilityScore aScore, int _WP)
        {
            abilityScore = aScore;
            HP = abilityScore.HPMAX;
            MP = abilityScore.MPMAX;
            WP = _WP;
            Rates = new BuffRate();
        }
    }

}