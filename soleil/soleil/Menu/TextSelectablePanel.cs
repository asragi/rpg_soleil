using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class TextSelectablePanel : SelectablePanel
    {
        public new readonly Vector PanelSize = new Vector(300, 36);
        /// <summary>
        /// パネルの文字描画を左上からどれだけの位置にするか
        /// </summary>
        protected readonly Vector Spacing = new Vector(28, 4);

        protected readonly FontID ItemFont = FontID.Yasashisa;
        protected readonly Color ItemColor = ColorPalette.DarkBlue;
        protected readonly FontID SelectedFont = FontID.Yasashisa;
        protected readonly Color SelectedColor = ColorPalette.AliceBlue;
        protected readonly Color DisableColor = ColorPalette.GlayBlue;

        public bool Active { get; set; }

        // 項目名の描画
        TextWithVal itemNameImage;
        readonly public String ItemName;
        protected int Val { set => itemNameImage.Val = value; }
        protected virtual FontID ValFont { set { itemNameImage.ValFont = value; } }
        protected bool EnableVal { set => itemNameImage.EnableValDisplay = value; }
        public override Vector ItemNumPosDiff { get => new Vector(340, 0); }

        // 選択状態の背景（これCursorとしてくらすにしたほうがよいきがする）
        Image selectedBack;

        public TextSelectablePanel(String itemName, BasicMenu parent, bool active = true) : base(parent)
        {
            ItemName = itemName;
            // Set Font Image
            itemNameImage = new TextWithVal(ItemFont, LocalPos + parent.Pos, (int)ItemNumPosDiff.X);
            itemNameImage.Text = itemName;
            Active = active;
            SetTextColor(ItemColor);
            itemNameImage.ValColor = ItemColor;

            // 選択状態を示すやつ
            selectedBack = new Image(0, Resources.GetTexture(TextureID.MenuSelected), LocalPos + parent.Pos, DepthID.Message, false, true, 0);
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            if (IsSelected) selectedBack.Fade(duration, _easeFunc, isFadeIn);
        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            selectedBack.MoveTo(target, duration, _easeFunc);
        }

        protected override void OnSelected()
        {
            selectedBack.Fade(20, MenuSystem.EaseFunc, true);
            itemNameImage.Font = SelectedFont;
            SetTextColor(SelectedColor);
        }

        /// <summary>
        /// 選択が解除された瞬間に行われる処理．
        /// </summary>
        protected override void OnUnselected()
        {
            selectedBack.Fade(5, MenuSystem.EaseFunc, false);
            itemNameImage.Font = ItemFont;
            SetTextColor(ItemColor);
        }

        public override void Update()
        {
            itemNameImage.Update();
            itemNameImage.Pos = BasicMenu.Pos + Spacing + LocalPos;
            itemNameImage.Alpha = BasicMenu.Alpha;
            selectedBack.Update();
            selectedBack.Pos = LocalPos + BasicMenu.Pos;
        }

        public override void Draw(Drawing d)
        {
            selectedBack.Draw(d);
            itemNameImage.Draw(d);
        }

        private void SetTextColor(Color col)
        {
            if (!Active)
            {
                itemNameImage.TextColor = DisableColor;
                return;
            }
            itemNameImage.TextColor = col;
        }
    }
}
