using Microsoft.Xna.Framework;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicIcon
    {
        static Dictionary<MagicCategory, Color> tmpColors = new Dictionary<MagicCategory, Color>() {
            { MagicCategory.Sun, Color.Gold },
            { MagicCategory.Shade, Color.RoyalBlue },
            { MagicCategory.Magic, Color.DarkOrchid },
            { MagicCategory.Dark, Color.Crimson },
            { MagicCategory.Wood, Color.OliveDrab },
            { MagicCategory.Metal, Color.Orange },
            { MagicCategory.Sound, Color.PaleVioletRed },
            { MagicCategory.Shinobi, Color.DarkCyan },
            { MagicCategory.Space, Color.Gray },
            { MagicCategory.Time, Color.MidnightBlue },
        };

        readonly Vector localPos;
        BasicMenu parent;
        public bool IsSelected;
        bool disabled;
        FontImage tmp;

        public MagicIcon(Vector _localPos, bool disable, MagicCategory c, BasicMenu _parent)
        {
            parent = _parent;
            localPos = _localPos;
            var pos = localPos + _parent.Pos;
            disabled = disable;
            tmp = new FontImage(FontID.Yasashisa, pos, DepthID.Message);
            tmp.Text = disabled ? "・" : "●";
            tmp.Color = tmpColors[c];
        }

        public void Update()
        {
            tmp.Pos = localPos + parent.Pos;
            tmp.Alpha = parent.Alpha;
            tmp.Update();
        }

        public void Draw(Drawing d)
        {
            tmp.Draw(d);
        }
    }
}
