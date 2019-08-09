using Microsoft.Xna.Framework;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// 魔法リスト表示時に下方に表示する魔法アイコン
    /// </summary>
    class MagicIcon
    {
        public readonly static Dictionary<MagicCategory, TextureID> IconMap = new Dictionary<MagicCategory, TextureID>() {
            { MagicCategory.Sun, TextureID.MagicSun },
            { MagicCategory.Shade, TextureID.MagicShade },
            { MagicCategory.Magic, TextureID.MagicMagic },
            { MagicCategory.Dark, TextureID.MagicDark },
            { MagicCategory.Wood, TextureID.MagicWood },
            { MagicCategory.Metal, TextureID.MagicMetal },
            { MagicCategory.Sound, TextureID.MagicSound },
            { MagicCategory.Shinobi, TextureID.MagicNinja },
            { MagicCategory.Space, TextureID.MagicSpace },
            { MagicCategory.Time, TextureID.MagicTime },
        };

        private readonly static Color DefaultColor = ColorPalette.GlayBlue;
        private readonly static Vector AdjustPos = new Vector(-4, -5);
        readonly Vector localPos;
        readonly Color iconColor;
        BasicMenu parent;
        bool isSelected;
        private bool disabled;
        Image iconImg;
        TextImage disableIcon;

        public MagicIcon(Vector _localPos, MagicCategory c, BasicMenu _parent)
        {
            parent = _parent;
            localPos = _localPos;
            var pos = localPos + _parent.Pos;
            iconImg = new Image(IconMap[c], pos, DepthID.Message);
            iconColor = ColorPalette.MagicColors[c];
            disableIcon = new TextImage(FontID.CorpM, pos + AdjustPos, DepthID.Message);
            disableIcon.Text = "・";
        }

        public bool IsSelected
        {
            set
            {
                isSelected = value;
                RefreshIcon();
            }
        }

        public bool IsDisabled
        {
            get => disabled;
            set
            {
                disabled = value;
                RefreshIcon();
            }
        }

        public void Update()
        {
            iconImg.Pos = localPos + parent.Pos;
            iconImg.Alpha = parent.Alpha;
            iconImg.Update();

            disableIcon.Pos = localPos + AdjustPos + parent.Pos;
            disableIcon.Alpha = parent.Alpha;
            disableIcon.Update();
        }

        public void Draw(Drawing d)
        {
            if (disabled) disableIcon.Draw(d);
            else iconImg.Draw(d);
        }

        private void RefreshIcon()
        {
            if (disabled) return;
            else iconImg.Color = isSelected ? iconColor : DefaultColor;
        }
    }
}
