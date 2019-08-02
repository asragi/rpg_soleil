using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusParamsDisplay : MenuComponent, IPersonUpdate
    {
        const int DiffX = 132;
        const int DiffY = 27;
        // もっとちゃんとしたところにおきたい
        readonly string[] Words = new[]
        {
            "STR",
            "VIT",
            "MAG",
            "SPD",
            "ATK",
            "MAG",
            "DEF",
            "RES"
        };

        TextWithVal[] texts;

        public StatusParamsDisplay(Vector pos)
        {
            texts = new TextWithVal[8];
            for (int i = 0; i < texts.Length; i++)
            {
                var xDiff = (i >= 4) ? DiffX : 0;
                texts[i] = new TextWithVal(FontID.CorpMini, pos + new Vector(xDiff, DiffY* (i % 4)), 116, Words[i], 0);
                texts[i].TextColor = ColorPalette.DarkBlue;
                texts[i].ValColor = ColorPalette.DarkBlue;
                texts[i].ValFont = FontID.CorpM;
            }
            AddComponents(texts);
        }

        public void RefreshWithPerson(Person p)
        {
            var score = p.Score;
            int phyAttack = p.Equip.GetAttack(AttackType.Physical);
            int magAttack = p.Equip.GetAttack(AttackType.Magical);
            int phyEquip = p.Equip.GetDef(AttackAttribution.Cut, AttackType.Physical);
            int magEquip = p.Equip.GetDef(AttackAttribution.Cut, AttackType.Magical);
            texts[0].Val = score.STR;
            texts[1].Val = score.VIT;
            texts[2].Val = score.MAG;
            texts[3].Val = score.SPD;
            texts[4].Val = phyAttack;
            texts[5].Val = magAttack;
            texts[6].Val = phyEquip;
            texts[7].Val = magEquip;
        }
    }
}
