using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMagicCategory : MenuComponent
    {
        const int XDiff = 120;
        const int YDiff = 60;
        MagicCategoryPiece[] pieces;
        public StatusMagicCategory(Vector pos)
            :base()
        {
            pieces = new MagicCategoryPiece[10]; // 術10系統
            for (int i = 0; i < 10; i++)
            {
                var x = XDiff * (i / 2);
                var y = (i % 2 == 0) ? 0 : YDiff;
                pieces[i] = new MagicCategoryPiece(pos + new Vector(x,y), i);
            }

            Components = pieces;
        }
    }
}
