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

        TextWithVal[] texts;

        public StatusParamsDisplay(Vector pos)
        {
            texts = new TextWithVal[8];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new TextWithVal(FontID.Test, pos + new Vector(0,DiffY*i), 200, Words[i]);
                texts[i].TextColor = ColorPalette.DarkBlue;
                texts[i].ValColor = ColorPalette.DarkBlue;
            }
        }

        public override void Call()
        {
            base.Call();
            foreach (var item in texts)
            {
                item.Call();
            }
        }

        public override void Quit()
        {
            base.Quit();
            foreach (var item in texts)
            {
                item.Quit();
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (var item in texts)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            foreach (var item in texts)
            {
                item.Draw(d);
            }
        }
    }
}
