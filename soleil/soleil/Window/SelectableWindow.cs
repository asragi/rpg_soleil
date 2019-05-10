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
    class SelectableWindow: VariableWindow
    {
        const int LineSpace = 35;
        string[] options;
        FontImage[] texts;
        int optionNum;
        protected int index;
        bool decided;
        public SelectableWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm, params string[] _options)
            : base(_pos, _size, tag, wm)
        {
            options = _options;
            texts = new FontImage[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                texts[i] = new FontImage(FontID.KkBlack, Pos + new Vector(Spacing, Spacing + LineSpace * i), DiffPos, DepthID.Message, false);
                texts[i].FadeSpeed = FadeSpeed;
                texts[i].Text = options[i];
            }
            optionNum = options.Length - 1;
            index = 0;
            decided = false;
            AddComponents(texts);
        }

        public void UpCursor()
        {
            if (decided) return;
            index--;
            if (index < 0) index = optionNum;
        }

        public void DownCursor()
        {
            if (decided) return;
            index++;
            if(index > optionNum) index = 0;
        }

        public void Decide()
        {
            decided = true;
        }

        /// <summary>
        /// 決定後であれば選んだ選択肢のindexを返す. 未選択時は常に-1を返す.
        /// </summary>
        /// <returns></returns>
        public int ReturnIndex()
        {
            if (decided) return index;
            return -1;
        }

        protected override void DrawContent(Drawing d)
        {
            base.DrawContent(d);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].Draw(d);
            }
            d.Draw(Pos + new Vector(0, 20+Spacing + LineSpace * index), Resources.GetTexture(TextureID.White), DepthID.Frame, 5);
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
    }
}