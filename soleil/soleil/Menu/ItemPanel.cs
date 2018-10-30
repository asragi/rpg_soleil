using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel : ItemPanelBase
    {
        public readonly Vector ItemNumPosDiff = new Vector(300, 0);

        public override Vector LocalPos
        {
            get { return _LocalPos; }
            set
            {
                base.LocalPos = value;
                itemNumImage.Pos = _LocalPos + Spacing + BasicMenu.Pos + ItemNumPosDiff;
            }
        }

        // 当該アイテムの所持数
        string itemNumText;
        FontImage itemNumImage;
        private string desc;
        public override string Desctiption => desc;

        public ItemPanel(ItemID id, ItemList itemData, ItemMenu parent)
            :base(id, ItemDataBase.Get(id).Name, parent)
        {
            // Desctiption
            desc = ItemDataBase.Get(id).Description;
            // itemNum
            itemNumText = id != ItemID.Empty ? itemData.GetItemNum(id).ToString() : "";
            itemNumImage = new FontImage(ItemFont, LocalPos+parent.Pos + ItemNumPosDiff, DepthID.Message, true, 0);
            itemNumImage.Color = ColorPalette.DarkBlue;

            itemNumImage.Text = itemNumText;

            LocalPos = Vector.Zero;
        }

        public override void Fade(int duration, EFunc _easeFunc, bool isFadeIn)
        {
            base.Fade(duration, _easeFunc, isFadeIn);
            itemNumImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public override void MoveTo(Vector target, int duration, EFunc _easeFunc)
        {
            base.MoveTo(target, duration, _easeFunc);
            itemNumImage.MoveTo(target + ItemNumPosDiff, duration, _easeFunc);
        }

        public override void Update()
        {
            base.Update();
            itemNumImage.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            itemNumImage.Draw(d);
        }
    }
}
