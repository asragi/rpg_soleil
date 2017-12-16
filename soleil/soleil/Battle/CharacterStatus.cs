using System;
using System.Collections.Generic;
using System.Linq;

namespace Soleil
{
    public class CharacterStatus
    {
        public AbilityScore abilityScore;

        public int HP;
        public int MP;


        public int STR
        {
            get
            {
                return Fraction(abilityScore.STR * STRrate);
            }
        }
        
        public int VIT
        {
            get
            {
                return Fraction(abilityScore.VIT * VITrate);
            }
        }

        public int MAG
        {
            get
            {
                return Fraction(abilityScore.MAG * MAGrate);
            }
        }
        
        public int SPD
        {
            get
            {
                return Fraction(abilityScore.SPD * SPDrate);
            }
        }

        float STRrate = 1.0f;
        float VITrate = 1.0f;
        float MAGrate = 1.0f;
        float SPDrate = 1.0f;

        public int WP = 0;

        public int NextTurn(int t)
        {
            return (WP + 10000 * t + SPD - 1) / SPD;
        }

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
        }
    }

}