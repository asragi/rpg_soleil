using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel : SelectablePanel
    {
        public readonly Vector ItemNumPosDiff = new Vector(300, 0);
        public override Vector LocalPos
        {
            get { return _LocalPos; }
            set
            {
                base.LocalPos = value;
                itemNumImage.Pos = _LocalPos + Spacing + BasicMenu.Pos + ItemNumPosDiff;
            }
        }

        // 当該アイテムの所持数
        int itemNum;
        FontImage itemNumImage;

        public ItemPanel(String itemName, int num, ItemMenu parent)
            :base(itemName, parent)
        {
            // itemNum
            itemNum = num;
            itemNumImage = new FontImage(ItemFont, LocalPos+parent.Pos + ItemNumPosDiff, DepthID.Message, true, 0);
            itemNumImage.Color = ColorPalette.DarkBlue;
            itemNumImage.Text = (itemNum >= 0) ? itemNum.ToString() : ""; // Itemnumがおかしな値なら描画しない

            LocalPos = Vector.Zero;
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            base.Fade(duration, _easeFunc, isFadeIn);
            itemNumImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            base.MoveTo(target, duration, _easeFunc);
            itemNumImage.MoveTo(target + ItemNumPosDiff, duration, _easeFunc);
        }

        public override void Update()
        {
            base.Update();
            itemNumImage.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            itemNumImage.Draw(d);
        }
    }
}
