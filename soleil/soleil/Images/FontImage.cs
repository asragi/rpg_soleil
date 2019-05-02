using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Soleil.Images;
using Soleil.Menu;
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
        public FontID Font { get; set; }
        public string Text { get; set; }
        public override Vector GetSize => (Vector)Resources.GetFont(Font).MeasureString(Text);
        public Color OutlineColor { get; set; } = Color.White;

        /// <summary>
        /// ImageManagerから作る.
        /// </summary>
        public FontImage(FontID fontID, Vector pos, Vector? posDiff, DepthID depth, bool isStatic = true, float alpha = 0)
            : base(pos, posDiff, depth, false, isStatic, alpha)
        {
            Font = fontID;
            Text = "";
        }

        public FontImage(FontID fontID, Vector pos, DepthID depth, bool isStatic = true, float alpha = 0)
            : this(fontID, pos, null, depth, isStatic, alpha) { }
   

        public override void Draw(Drawing d)
        {
            if (IsStatic) d.DrawStaticText(Pos, Resources.GetFont(Font), Text, Color * Alpha, DepthID, Vector2.One, Angle, false);
            else d.DrawText(Pos, Resources.GetFont(Font), Text, Color * Alpha, DepthID, 1, Angle, false);
        }

        /// <summary>
        /// 枠線を外側に表示するためのクラス内クラス
        /// </summary>
        private class Outline: IComponent
        {
            public Color Color { get; set; }
            readonly FontImage parent;
            readonly Vector[] diffs;
            readonly FontImage[] outlineTexts;
            static Vector[] normalizedDiffs = new[] { new Vector(1, 0), new Vector(0, -1), new Vector(-1, 0), new Vector(0, 1)};

            public Outline(FontImage _parent, int diff, Vector positionDiff, DepthID depth)
            {
                parent = _parent;
                diffs = SetDiffPosition(normalizedDiffs, parent.Pos, diff);
                outlineTexts = SetFontImages(parent.Font, diffs, positionDiff, depth);

                Vector[] SetDiffPosition(Vector[] normalVecs, Vector parentPos, int diffSize)
                {
                    var result = new Vector[normalVecs.Length];
                    for (int i = 0; i < diffs.Length; i++)
                    {
                        result[i] = parentPos + normalVecs[i] * diffSize;
                    }
                    return result;
                }

                FontImage[] SetFontImages(FontID font, Vector[] outlineVecs, Vector posDiff,
                    DepthID _depth, bool isStatic = true, float alpha = 0)
                {
                    var size = outlineVecs.Length;
                    var result = new FontImage[size];
                    for (int i = 0; i < size; i++)
                    {
                        result[i] = new FontImage(font, outlineVecs[i], posDiff, _depth, isStatic, alpha);
                    }
                    return result;
                }
            }

            public void Update()
            {
                throw new NotImplementedException();
            }

            public void Call()
            {
                throw new NotImplementedException();
            }

            public void Quit()
            {
                throw new NotImplementedException();
            }

            public void Draw(Drawing d)
            {
                throw new NotImplementedException();
            }
        }
    }
}
