using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusParamsDisplay : MenuComponent
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

        readonly int[] Para = new[] // 仮置き適当パラメータ
        {
            6,
            12,
            44,
            11,
            22,
            36,
            18,
            27,
        };

        TextWithVal[] texts;

        public StatusParamsDisplay(Vector pos)
        {
            texts = new TextWithVal[8];
            for (int i = 0; i < texts.Length; i++)
            {
                var xDiff = (i >= 4) ? DiffX : 0;
                texts[i] = new TextWithVal(FontID.CorpMini, pos + new Vector(xDiff, DiffY* (i % 4)), 116, Words[i], Para[i]);
                texts[i].TextColor = ColorPalette.DarkBlue;
                texts[i].ValColor = ColorPalette.DarkBlue;
                texts[i].ValFont = FontID.Yasashisa;
            }
            AddComponents(texts);
        }
    }
}
