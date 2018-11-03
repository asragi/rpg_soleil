using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusParamsDisplay : MenuComponent
    {
        const int DiffY = 28;
        // もっとちゃんとしたところにおきたい
        readonly string[] Words = new[]
        {
            "STR",
            "VIT",
            "MAG",
            "SPD",
            "ATK",
            "DEF"
        };

        readonly int[] Para = new[] // 仮置き適当パラメータ
        {
            6,
            12,
            44,
            11,
            22,
            18,
        };

        TextWithVal[] texts;

        public StatusParamsDisplay(Vector pos)
        {
            texts = new TextWithVal[6];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new TextWithVal(FontID.Touhaba, pos + new Vector(0,DiffY*i), 130, Words[i], Para[i]);
                texts[i].TextColor = ColorPalette.DarkBlue;
                texts[i].ValColor = ColorPalette.DarkBlue;
            }
            Components = texts;
        }
    }
}
