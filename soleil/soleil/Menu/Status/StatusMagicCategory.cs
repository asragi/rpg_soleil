using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMagicCategory : MenuComponent
    {
        readonly Vector ContentPos = new Vector(36, 9);
        const int YDiff = 27;
        MagicCategoryPiece[] pieces;
        Image backImg;

        public static readonly Dictionary<MagicCategory, string> magicNames = new Dictionary<MagicCategory, string>()
        {
            { MagicCategory.Sun, "陽術" },
            { MagicCategory.Shade, "陰術" },
            { MagicCategory.Magic, "魔術" },
            { MagicCategory.Dark ,"邪術" },
            { MagicCategory.Sound, "音術" },
            { MagicCategory.Shinobi, "忍術" },
            { MagicCategory.Wood, "樹術" },
            { MagicCategory.Metal, "鋼術" },
            { MagicCategory.Space, "空術" },
            { MagicCategory.Time, "時術" },
        };

        public StatusMagicCategory(Vector pos)
            : base()
        {
            backImg = new Image(TextureID.MenuCategory, pos, Vector.Zero, DepthID.MenuTop);
            pieces = new MagicCategoryPiece[10]; // 術10系統
            for (int i = 0; i < 10; i++)
            {
                pieces[i] = new MagicCategoryPiece(pos + ContentPos + new Vector(0, YDiff * i), (MagicCategory)i);
                pieces[i].Name = magicNames[(MagicCategory)i];
            }

            AddComponents(new[] { backImg });
            AddComponents(pieces);
        }

        public void RefreshWithPerson(Person p)
        {
            for (int i = 0; i < 10; i++)
            {
                int lv = p.Magic.GetLv(pieces[i].category);
                pieces[i].Lv = lv;
                pieces[i].Color = lv==0 ? ColorPalette.GlayBlue : ColorPalette.DarkBlue;
            }
        }
    }
}
