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
        private string text;
        public virtual string Text { get => text; set { text = value; if(outline != null) outline.Text = text; } }
        public override Vector ImageSize => (Vector)Resources.GetFont(Font).MeasureString(Text);
        public Color OutlineColor { get; set; } = ColorPalette.DarkBlue;

        // Outline
        private Outline outline;

        // RightAlign
        private bool rightAlign;

        public override int FrameWait { set { base.FrameWait = value; outline?.FrameWait(value); } }

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
   
        public void ActivateOutline(int diff, bool activate = true)
        {
            if (!activate && outline == null) return;
            outline = outline ?? new Outline(this, diff, PosDiff, DepthID, IsStatic);
            outline.Color = OutlineColor;
            outline.IsVisible = activate;
            outline.Text = Text;
        }

        public void RightAlign(bool activate)
        {
            rightAlign = activate;
            RefreshTextPos();
        }

        private void RefreshTextPos()
        {
            Vector basePos;
            if (rightAlign)
            {
                basePos = InitPos - ImageSize;
            }
            else
            {
                basePos = InitPos;
            }
            basePos += PosDiff;
            Pos = basePos;
        }

        public override void Update()
        {
            base.Update();
            outline?.Update();
        }

        public override void Draw(Drawing d)
        {
            outline?.Draw(d);
            if (IsStatic) d.DrawStaticText(Pos, Resources.GetFont(Font), Text, Color * Alpha, DepthID, Vector2.One, Angle, false);
            else d.DrawText(Pos, Resources.GetFont(Font), Text, Color * Alpha, DepthID, 1, Angle, false);
        }

        public override void Call()
        {
            base.Call();
            outline?.Call();
        }

        public override void Quit()
        {
            base.Quit();
            outline?.Quit();
        }


        /// <summary>
        /// 枠線を外側に表示するためのクラス内クラス
        /// </summary>
        private class Outline: IComponent
        {
            public Color Color { set => outlineTexts.ForEach2(s => s.Color = value); }
            public bool IsVisible;
            public string Text { set => outlineTexts.ForEach2(s => s.Text = value); }
            readonly FontImage parent;
            readonly Vector[] diffs;
            readonly FontImage[] outlineTexts;
            static Vector[] normalizedDiffs = new[] { new Vector(1, 0), new Vector(0, -1), new Vector(-1, 0), new Vector(0, 1)};

            public void FrameWait(int frame) => outlineTexts.ForEach2(s => s.FrameWait = frame);

            public Outline(FontImage _parent, int diff, Vector positionDiff, DepthID depth, bool isStatic)
            {
                parent = _parent;
                diffs = SetDiffPosition(normalizedDiffs, parent.Pos - positionDiff, diff);
                outlineTexts = SetFontImages(parent.Font, diffs, positionDiff, depth, isStatic);

                Vector[] SetDiffPosition(Vector[] normalVecs, Vector parentPos, int diffSize)
                {
                    var size = normalizedDiffs.Length;
                    var result = new Vector[size];
                    for (int i = 0; i < size; i++)
                    {
                        result[i] = parentPos + normalVecs[i] * diffSize;
                    }
                    return result;
                }

                FontImage[] SetFontImages(FontID font, Vector[] outlineVecs, Vector posDiff,
                    DepthID _depth, bool _isStatic, float alpha = 0)
                {
                    var size = outlineVecs.Length;
                    var result = new FontImage[size];
                    for (int i = 0; i < size; i++)
                    {
                        result[i] = new FontImage(font, outlineVecs[i], posDiff, _depth, _isStatic, alpha);
                    }
                    return result;
                }
            }

            public void Update() => outlineTexts.ForEach2(s => s.Update());
            public void Call() => outlineTexts.ForEach2(s => s.Call());
            public void Quit() => outlineTexts.ForEach2(s => s.Quit());
            public void Draw(Drawing d)
            {
                if (IsVisible) outlineTexts.ForEach2(s => s.Draw(d));
            }
        }
    }
}
