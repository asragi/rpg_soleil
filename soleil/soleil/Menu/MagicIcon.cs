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
            { MagicCategory.Sun, Color.Yellow },
            { MagicCategory.Magic, Color.Purple }
        };

        readonly Vector localPos;
        BasicMenu parent;
        public bool IsSelected;
        bool disabled;
        FontImage tmp;

        public MagicIcon(Vector _localPos, bool disable, BasicMenu _parent)
        {
            parent = _parent;
            localPos = _localPos;
            var pos = localPos + _parent.Pos;
            disabled = disable;
            tmp = new FontImage(FontID.Yasashisa, pos, DepthID.Message);
            tmp.Text = disabled ? "・" : "●";
            tmp.Color = Color.Yellow;
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
