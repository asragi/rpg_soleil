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
        public FontID Font { set => font = value; }
        public String Text { get; set; }
        public override Vector GetSize => (Vector)Resources.GetFont(font).MeasureString(Text);

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
            if (IsStatic) d.DrawStaticText(Pos, Resources.GetFont(font), Text, Color * Alpha, DepthID, Vector2.One, Angle, false);
            else d.DrawText(Pos, Resources.GetFont(font), Text, Color * Alpha, DepthID, 1, Angle, false);
        }
    }
}
