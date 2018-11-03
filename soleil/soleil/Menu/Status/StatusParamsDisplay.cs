using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusParamsDisplay : MenuComponent
    {
        const int DiffY = 30;
        // もっとちゃんとしたところにおきたい
        readonly string[] Words = new[]
        {
            "STR",
            "VIT",
            "MAG",
            "SPD",
            "P-ATK",
            "M-ATK",
            "P-DEF",
            "M-DEF"
        };

        readonly int[] Para = new[] // 仮置き適当パラメータ
        {
            6,
            12,
            44,
            11,
            12,
            22,
            18,
            21,
        };

        TextWithVal[] texts;

        public StatusParamsDisplay(Vector pos)
        {
            texts = new TextWithVal[8];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new TextWithVal(FontID.Test, pos + new Vector(0,DiffY*i), 200, Words[i], Para[i]);
                texts[i].TextColor = ColorPalette.DarkBlue;
                texts[i].ValColor = ColorPalette.DarkBlue;
            }
            Components = texts;
        }
    }
}
