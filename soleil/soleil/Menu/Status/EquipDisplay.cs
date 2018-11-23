using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class EquipDisplay : MenuComponent
    {
        const int TopDiffY = 24;
        const int DiffY = 40;
        FontImage[] texts;
        FontImage equipTitle;
        const string EquipTitle = "Equips";
        const string Empty = "------";
        readonly string[] tmp = new[] {"シルバーワンド", "指定服", "ビーズのアクセサリー", "太陽の髪留め", Empty };

        public EquipDisplay(Vector pos)
        {
            texts = new FontImage[5];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new FontImage(FontID.KkBlack, pos + new Vector(0, TopDiffY + DiffY * i), DepthID.MenuMiddle);
                texts[i].Text = tmp[i];
            }
            equipTitle = new FontImage(FontID.KkGoldMini, pos, DepthID.MenuTop);
            equipTitle.Text = EquipTitle;
            AddComponents(texts.Concat(new[] { equipTitle }).ToArray());
        }
    }
}
