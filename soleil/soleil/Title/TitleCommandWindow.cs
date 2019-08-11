using Microsoft.Xna.Framework;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    class TitleCommandWindow
    {
        private const int Space = 40;
        private const int FrameWait = 8;
        private const DepthID Depth = DepthID.MessageBack;
        TitleCommand[] commands;
        private int index;

        public TitleCommandWindow(Vector pos, string[] options)
        {
            int length = options.Length;
            commands = new TitleCommand[length];
            for (int i = 0; i < length; i++)
            {
                commands[i] = new TitleCommand(options[i], new Vector(pos.X, pos.Y + Space * i), Depth)
                {
                    Selected = i == 0,
                    FrameWait = i * FrameWait,
                };
            }
        }

        public int Index { get => index; set => index = (value + commands.Length) % commands.Length; }

        public void UpCursor()
        {
            Index--;
            RefreshSelected();
        }

        public void DownCursor()
        {
            Index++;
            RefreshSelected();
        }

        public void Call() => commands.ForEach2(s => s.Call());
        public void Update() => commands.ForEach2(s => s.Update());
        public void Draw(Drawing d) => commands.ForEach2(s => s.Draw(d));

        private void RefreshSelected()
        {
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i].Selected = i == Index;
            }
        }

        class TitleCommand
        {
            private static readonly Vector TextDiff = new Vector(1, -2);
            private static readonly FontID Font = FontID.CorpM;
            TextImage textImg;
            Image selectedImg, unselectedImg;
            bool disabled, selected;
            ImageBase[] components;

            public TitleCommand(string _text, Vector pos, DepthID depth, bool isStatic = true)
            {
                selectedImg = new Image(TextureID.TitleLabelSelected, pos, depth, isStatic: isStatic);
                unselectedImg = new Image(TextureID.TitleLabelUnselected, pos, depth, isStatic: isStatic);
                textImg = new TextImage(Font, pos + TextDiff, depth, isStatic);
                textImg.Text = _text;
                components = new ImageBase[] { unselectedImg, selectedImg, textImg };
            }

            public bool Disabled {
                get => disabled;
                set
                {
                    disabled = value;
                    textImg.Color = GetProperColor();
                }
            }

            public bool Selected
            {
                get => selected;
                set
                {
                    selected = value;
                    textImg.Color = GetProperColor();
                    if (selected)
                    {
                        selectedImg.Alpha = 1;
                        unselectedImg.Alpha = 0;
                    }
                    else
                    {
                        selectedImg.Alpha = 0;
                        unselectedImg.Alpha = 1;
                    }
                }
            }

            public int FrameWait
            {
                set => components.ForEach2(s => s.FrameWait = value);
            }

            public void Call()
            {
                components.ForEach2(s => s.MoveToDefault());
                textImg.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
                if (selected) selectedImg.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
                else unselectedImg.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            }

            public void Update() => components.ForEach2(s => s.Update());

            public void Draw(Drawing d) => components.ForEach2(s => s.Draw(d));

            private Color GetProperColor()
            {
                if (disabled) return ColorPalette.GlayBlue;
                return selected ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
            }
        }
    }
}
