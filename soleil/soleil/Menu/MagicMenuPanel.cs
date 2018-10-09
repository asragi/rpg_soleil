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
    class MagicMenuPanel : SelectablePanel
    {
        public readonly Vector CostPosDiff = new Vector(300, 0);
        public override Vector LocalPos
        {
            get { return _LocalPos; }
            set
            {
                base.LocalPos = value;
                costImage.Pos = _LocalPos + Spacing + BasicMenu.Pos + CostPosDiff;
            }
        }
        public override string Desctiption => ItemName;
        // 消費コスト表示
        int costNum;
        FontImage costImage;

        public MagicMenuPanel(String itemName, int num, MagicMenu parent)
            : base(itemName, parent)
        {
            // itemNum
            costNum = num;
            costImage = new FontImage(ItemFont, LocalPos + parent.Pos + CostPosDiff, DepthID.Message, true, 0);
            costImage.Color = ColorPalette.DarkBlue;
            costImage.Text = costNum.ToString();

            LocalPos = Vector.Zero;
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            base.Fade(duration, _easeFunc, isFadeIn);
            costImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            base.MoveTo(target, duration, _easeFunc);
            costImage.MoveTo(target + CostPosDiff, duration, _easeFunc);
        }

        public override void Update()
        {
            base.Update();
            costImage.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            costImage.Draw(d);
        }
    }
}
