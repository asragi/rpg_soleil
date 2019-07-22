using Microsoft.Xna.Framework;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 配色の統一感を与えるためのクラス. 新しく色を使う場合はここに追加してから用いる.
    /// </summary>
    static class ColorPalette
    {
        public static Color AliceBlue = Color.AliceBlue;
        public static Color GlayBlue = new Color(156, 179, 199);
        public static Color DarkBlue = new Color(36, 69, 98);

        public readonly static Dictionary<MagicCategory, Color> MagicColors = new Dictionary<MagicCategory, Color>() {
            { MagicCategory.Sun, Color.PaleGoldenrod },
            { MagicCategory.Shade, Color.SteelBlue },
            { MagicCategory.Magic, Color.Plum },
            { MagicCategory.Dark, Color.Crimson },
            { MagicCategory.Wood, Color.OliveDrab },
            { MagicCategory.Metal, Color.Orange },
            { MagicCategory.Sound, Color.PaleVioletRed },
            { MagicCategory.Shinobi, Color.DarkCyan },
            { MagicCategory.Space, Color.Gray },
            { MagicCategory.Time, Color.MidnightBlue },
        };
    }
}
