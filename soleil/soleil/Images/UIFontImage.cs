using Microsoft.Xna.Framework;
using Soleil.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class UIFontImage : UIImageBase
    {
        FontID font;
        public String Text { get; set; }

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public UIFontImage(FontID fontID, Vector pos, Vector? posDiff, DepthID depth, bool isStatic = true, float alpha = 0)
            : base(pos, posDiff, depth, false, isStatic, alpha)
        {
            font = fontID;
            Text = "";
        }

        public override void Draw(Drawing d)
        {
            d.DrawStaticText(Pos, Resources.GetFont(font), Text, Color.White * Alpha, DepthID, Vector2.One, Angle, false);
        }
    }
}
