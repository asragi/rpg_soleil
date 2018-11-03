using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Soleil.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// 動きなど機能を与えたFontSpriteの基底クラス
    /// </summary>
    class FontImage : UIImageBase
    {
        FontID font;
        public String Text { get; set; }
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public FontImage(FontID fontID, Vector pos, Vector? posDiff, DepthID depth, bool isStatic = true, float alpha = 0)
            : base(pos, posDiff, depth, false, isStatic, alpha)
        {
            font = fontID;
            Text = "";
        }

        public FontImage(FontID fontID, Vector pos, DepthID depth, bool isStatic = true, float alpha = 0)
            : this(fontID, pos, null, depth, isStatic, alpha) { }
   

        public override void Draw(Drawing d)
        {
            d.DrawStaticText(Pos, Resources.GetFont(font), Text, Color * Alpha, DepthID, Vector2.One, Angle, false);
        }
    }
}
