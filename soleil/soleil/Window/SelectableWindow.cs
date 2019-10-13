using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// カーソルを用いた選択を行う機能を持つウィンドウ
    /// </summary>
    class SelectableWindow : VariableWindow
    {
        const int LineSpace = 35;
        const int CursorWidthPlus = 4;
        private static readonly Vector CursorPosModify = new Vector(0, 2);
        string[] options;
        TextImage[] texts;
        int optionNum;
        public int Index { get; protected set; }
        bool decided;
        Image cursorImg;
        public SelectableWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm, bool isStatic, params string[] _options)
            : base(_pos, _size, tag, wm, isStatic)
        {
            options = _options;
            texts = new TextImage[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                texts[i] = new TextImage(MessageWindow.DefaultFont, Pos + new Vector(Spacing, Spacing + LineSpace * i), DiffPos, DepthID.Message, isStatic);
                texts[i].FadeSpeed = FadeSpeed;
                texts[i].Text = options[i];
                texts[i].Color = i == Index ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
            }
            Vector cursorPos = new Vector(Size.X / 2, VariableRectangle.FrameSize + LineSpace * (0.5f + Index)) + CursorPosModify;
            cursorImg = new Image(TextureID.White, Pos + cursorPos, DiffPos, DepthID.MessageBack, true, isStatic);
            cursorImg.Size = new Vector(Size.X - Spacing * 2 + CursorWidthPlus, LineSpace);
            cursorImg.Color = ColorPalette.DarkBlue;
            optionNum = options.Length - 1;
            Index = 0;
            decided = false;
            AddComponents(texts);
            AddComponents(cursorImg);
        }

        public SelectableWindow(Vector _pos, bool isStatic, params string[] options)
            : this(_pos, ProperSize(MessageWindow.DefaultFont, options), WindowTag.A, WindowManager.GetInstance(), isStatic, options)
        { }

        public void Reset()
        {
            decided = false;
        }

        public void UpCursor()
        {
            if (decided) return;
            Index--;
            if (Index < 0) Index = optionNum;
            RefreshCursorPos();
        }

        public void DownCursor()
        {
            if (decided) return;
            Index++;
            if (Index > optionNum) Index = 0;
            RefreshCursorPos();
        }

        public void Decide()
        {
            decided = true;
        }

        /// <summary>
        /// 決定後であれば選んだ選択肢のIndexを返す. 未選択時は常に-1を返す.
        /// </summary>
        /// <returns></returns>
        public int ReturnIndex()
        {
            if (decided) return Index;
            return -1;
        }

        /// <summary>
        /// 選択肢の内容から適切なSelectableWindowのサイズを計算して返す．
        /// </summary>
        public static Vector ProperSize(FontID id, string[] options)
        {
            float length = 0;
            var font = Resources.GetFont(id);

            // 選択肢中一番長いものの長さを取得．
            for (int i = 0; i < options.Length; i++)
            {
                var messageLength = font.MeasureString(options[i]).X;
                length = (length < messageLength) ? messageLength : length;
            }

            return new Vector(2 * Spacing + length, 2 * Spacing + LineSpace * options.Length);
        }

        private void RefreshCursorPos()
        {
            cursorImg.Pos = Pos + new Vector(Size.X / 2, VariableRectangle.FrameSize + LineSpace * (0.5f + Index)) + CursorPosModify;
            cursorImg.InitPos = cursorImg.Pos;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].Color = i == Index ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
            }
        }
    }
}