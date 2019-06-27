using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    /// <summary>
    /// 属性に対する耐性表示コンポーネント．Status表示や装備時に．
    /// </summary>
    class AttributeDisplay : MenuComponent
    {
        const int DiffY = 28;

        // 属性っぽいアイコン用意するかも
        readonly string[] Words = new[]
        {
            "熱","冷","電"
        };

        // 仮置き
        readonly int[] p = new[] { -38, 25, 55 };

        TextImage[] texts;
        TextImage[] indicates;

        public AttributeDisplay(Vector pos)
        {
            texts = new TextImage[3];
            indicates = new TextImage[texts.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new TextImage(FontID.CorpM, pos + new Vector(0, DiffY * i), DepthID.MenuMiddle);
                texts[i].Text = Words[i];
                indicates[i] = new TextImage(FontID.CorpM, pos + new Vector(50, DiffY * i), DepthID.MenuMiddle);
                indicates[i].Text = SetIndicate(p[i]);
            }
            AddComponents(texts.Concat(indicates).ToArray());

            string SetIndicate(int val)
            {
                var mark = (val > 0) ? '+' : '-';
                var abs = Math.Abs(val);

                for (int i = 5; i > 0; i--)
                {
                    if (abs > (i-1) * 10) return new string(mark, i);
                }
                return "";
            }
        }
    }
}
