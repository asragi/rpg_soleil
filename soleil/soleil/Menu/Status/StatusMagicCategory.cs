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
        const int XDiff = 100;
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
        }

        public override void Call()
        {
            base.Call();
            foreach (var item in pieces)
            {
                item.Call();
            }
        }

        public override void Quit()
        {
            base.Quit();
            foreach (var item in pieces)
            {
                item.Quit();
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (var item in pieces)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            foreach (var item in pieces)
            {
                item.Draw(d);
            }
        }
    }
}
