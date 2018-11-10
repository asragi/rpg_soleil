using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMagicCategory : MenuComponent
    {
        const int XDiff = 80;
        const int YDiff = 80;
        MagicCategoryPiece[] pieces;

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
            pieces = new MagicCategoryPiece[10]; // 術10系統
            for (int i = 0; i < 10; i++)
            {
                var x = XDiff * (i / 2);
                var y = (i % 2 == 0) ? 0 : YDiff;
                pieces[i] = new MagicCategoryPiece(pos + new Vector(x,y), i);
                pieces[i].Name = names[i];
            }

            Components = pieces;
        }
    }
}
