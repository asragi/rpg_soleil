using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// 動きなど機能を与えたFontSpriteの基底クラス
    /// </summary>
    class FontImage : ImageBase
    {
        FontID font;
        public String Text { get; set; }
        public Color Color { get; set; }
        public bool EnableShadow { get; set; }
        public Color ShadowColor { get; set; }
        public Vector ShadowPos { get; set; }
        public override Vector GetSize => (Vector)Resources.GetFont(font).MeasureString(Text);

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public FontImage(FontID fontID, Vector pos, DepthID depth, bool isStatic = true, float alpha = 1)
            :base(pos, depth, false, isStatic, alpha )
        {
            font = fontID;
            Text = "";
            EnableShadow = false;
            Color = Color.White;
            ShadowColor = Color.Black;
            ShadowPos = Vector.Zero;
        }

        public override void Draw(Drawing d)
        {
            if (EnableShadow) d.DrawStaticText(Pos + ShadowPos, Resources.GetFont(font), Text, ShadowColor * Alpha, DepthID, Vector2.One, Angle, false);
            d.DrawStaticText(Pos, Resources.GetFont(font), Text, Color * Alpha, DepthID, Vector2.One, Angle, false);
        }
    }
}
