using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class EquipDisplay : MenuComponent
    {
        const int DiffY = 34;
        FontImage[] texts;
        readonly string[] tmp = new[] {"シルバーワンド", "指定服", "ビーズのアクセサリー", "太陽の髪留め"};

        public EquipDisplay(Vector pos)
        {
            texts = new FontImage[4];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new FontImage(FontID.Yasashisa, pos + new Vector(0, DiffY * i), DepthID.MenuMiddle);
                texts[i].Color = ColorPalette.DarkBlue;
                texts[i].Text = tmp[i];
            }
            AddComponents(texts);
        }
    }
}
