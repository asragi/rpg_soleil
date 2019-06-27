using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMagicCategory : MenuComponent
    {
        readonly Vector ContentPos = new Vector(36, 9);
        const int YDiff = 27;
        MagicCategoryPiece[] pieces;
        Image backImg;

        readonly string[] names = new[] // debug tmp
        {
            "陽術",
            "陰術",
            "魔術",
            "－－",
            "音術",
            "忍術",
            "樹術",
            "鋼術",
            "－－",
            "－－",
        };

        public StatusMagicCategory(Vector pos)
            :base()
        {
            backImg = new Image(TextureID.MenuCategory, pos, Vector.Zero, DepthID.MenuTop);
            pieces = new MagicCategoryPiece[10]; // 術10系統
            for (int i = 0; i < 10; i++)
            {
                pieces[i] = new MagicCategoryPiece(pos + ContentPos + new Vector(0, YDiff * i), i);
                pieces[i].Name = names[i];
            }

            AddComponents(new[] { backImg });
            AddComponents(pieces);
        }
    }
}
