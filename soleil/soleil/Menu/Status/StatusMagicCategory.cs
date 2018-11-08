using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMagicCategory : MenuComponent
    {
        // 左上の基準点からの位置関係
        readonly Vector Spacing = new Vector(15, 15);
        const int XDiff = 120;
        const int YDiff = 60;
        MagicCategoryPiece[] pieces;
        public StatusMagicCategory(Vector pos)
            :base()
        {
            pieces = new MagicCategoryPiece[10]; // 術10系統
            for (int i = 0; i < 10; i++)
            {
                var x = (i % 2 == 0) ? 0 : XDiff;
                var y = YDiff * (i / 2);
                pieces[i] = new MagicCategoryPiece(pos + Spacing + new Vector(x,y), i);
            }

            Components = pieces;
        }
    }
}
