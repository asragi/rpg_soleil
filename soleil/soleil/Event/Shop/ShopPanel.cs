using Soleil.Item;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    class ShopPanel : ItemPanelBase
    {
        public readonly Vector ValuePosDiff = new Vector(300, 0);
        private string desc;
        public override string Desctiption => desc;
        public ItemID ID { get; private set; }
        public int Price { get; private set; }

        public override Vector LocalPos
        {
            get => base.LocalPos;
            set
            {
                base.LocalPos = value;
                valueImage.Pos = _LocalPos + Spacing + BasicMenu.Pos + ValuePosDiff;
            }
        }

        // 価格表示
        string valueText;
        FontImage valueImage;

        public ShopPanel(ItemID id, int value, ShopItemList parent)
            :base(id, ItemDataBase.Get(id).Name, parent)
        {
            desc = ItemDataBase.Get(id).Description;
            ID = id;
            Price = value;

            // itemNum
            valueText = value.ToString();
            valueImage = new FontImage(ItemFont, LocalPos + parent.Pos + ValuePosDiff, DepthID.Message, true, 0);
            valueImage.Color = ColorPalette.DarkBlue;

            valueImage.Text = valueText;
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            base.Fade(duration, _easeFunc, isFadeIn);
            valueImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            base.MoveTo(target, duration, _easeFunc);
            valueImage.MoveTo(target + ValuePosDiff, duration, _easeFunc);
        }

        public void Call()
        {

        }

        public void Quit()
        {

        }

        public override void Update()
        {
            base.Update();
            valueImage.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            valueImage.Draw(d);
        }
    }
}
