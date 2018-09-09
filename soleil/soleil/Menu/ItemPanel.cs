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
    class ItemPanel
    {
        public readonly Vector PanelSize = new Vector(300, 36);
        /// <summary>
        /// パネルの文字描画を左上からどれだけの位置にするか
        /// </summary>
        readonly Vector Spacing = new Vector(8, 8);
        readonly FontID ItemFont = FontID.Test;

        Vector pos;
        public Vector Pos {
            get { return pos; }
            set
            {
                pos = value;
                itemNameImage.Pos = pos + Spacing;
            }
        }

        FontImage itemNameImage;
        readonly public String ItemName;
        public ItemPanel(String itemName)
        {
            ItemName = itemName;
            pos = Vector.Zero;

            // Set Font Image
            itemNameImage = new FontImage(ItemFont, Vector.Zero, DepthID.Message, true, 0);
            itemNameImage.Color = ColorPalette.DarkBlue;
            itemNameImage.EnableShadow = false;
            itemNameImage.ShadowPos = new Vector(3, 3);
            itemNameImage.ShadowColor = ColorPalette.GlayBlue;
            itemNameImage.Text = itemName;
        }

        public void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            itemNameImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            itemNameImage.MoveTo(target, duration, _easeFunc);
        }

        public void Update()
        {
            itemNameImage.Update();
        }

        public void Draw(Drawing d)
        {
            itemNameImage.Draw(d);
        }
    }
}
