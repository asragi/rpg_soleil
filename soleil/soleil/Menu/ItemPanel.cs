using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel
    {
        public readonly Vector PanelSize = new Vector(300, 36);
        public readonly Vector ItemNumPosDiff = new Vector(300, 0);
        /// <summary>
        /// パネルの文字描画を左上からどれだけの位置にするか
        /// </summary>
        readonly Vector Spacing = new Vector(8, 4);
        readonly FontID ItemFont = FontID.Test;
        readonly Func<double, double, double, double, double> func = Easing.OutCubic;

        // Parentに対する相対的な基準座標
        private Vector localPos;
        public Vector LocalPos
        {
            get { return localPos; }
            set
            {
                localPos = value;
                itemNumImage.Pos = localPos + Spacing + itemMenu.Pos + ItemNumPosDiff;
                itemNameImage.Pos = localPos + Spacing + itemMenu.Pos;
                selectedBack.Pos = localPos + itemMenu.Pos;
            }
        }
        ItemMenu itemMenu;

        // アイテム名の描画
        FontImage itemNameImage;
        readonly public String ItemName;

        // 当該アイテムの所持数
        int itemNum;
        FontImage itemNumImage;

        // 選択状態の背景（これCursorとしてくらすにしたほうがよいきがする）
        Image selectedBack;
        Image unselectedBack;
        bool isSelected;

        public ItemPanel(String itemName, int num, ItemMenu parent)
        {
            ItemName = itemName;
            itemMenu = parent;
            // itemNum
            itemNum = num;
            itemNumImage = new FontImage(ItemFont, LocalPos+parent.Pos + ItemNumPosDiff, DepthID.Message, true, 0);
            itemNumImage.Color = ColorPalette.DarkBlue;
            itemNumImage.Text = itemNum.ToString();
            // Set Font Image
            itemNameImage = new FontImage(ItemFont, LocalPos+parent.Pos, DepthID.Message, true, 0);
            itemNameImage.Color = ColorPalette.DarkBlue;
            itemNameImage.EnableShadow = false;
            itemNameImage.ShadowPos = new Vector(3, 3);
            itemNameImage.ShadowColor = ColorPalette.GlayBlue;
            itemNameImage.Text = itemName;

            // 選択状態を示すやつ
            selectedBack = new Image(0, Resources.GetTexture(TextureID.MenuSelected), LocalPos + parent.Pos, DepthID.Message, false, true, 0);
            unselectedBack = new Image(0, Resources.GetTexture(TextureID.MenuUnselected), Vector.Zero, DepthID.Message, false, true, 0);

            LocalPos = Vector.Zero;
        }

        public void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            itemNameImage.Fade(duration, _easeFunc, isFadeIn);
            itemNumImage.Fade(duration, _easeFunc, isFadeIn);
            if(isSelected) selectedBack.Fade(duration, _easeFunc, isFadeIn);
            unselectedBack.Fade(duration, _easeFunc, isFadeIn);
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            itemNameImage.MoveTo(target + Spacing, duration, _easeFunc);
            itemNumImage.MoveTo(target + ItemNumPosDiff, duration, _easeFunc);
            selectedBack.MoveTo(target, duration, _easeFunc);
            unselectedBack.MoveTo(target, duration, _easeFunc);
        }

        public void SetSelectedAndFade(bool select)
        {
            if (select)
            {
                selectedBack.Fade(20, func, true);
                unselectedBack.Fade(20, func, false);
                itemNameImage.Color = ColorPalette.AliceBlue;
            }
            else if(!select && isSelected) // 選択が解除された瞬間
            {
                selectedBack.Fade(20, func, false);
                unselectedBack.Fade(20, func, true);
                itemNameImage.Color = ColorPalette.DarkBlue;
            }
            isSelected = select;
        }

        public void Update()
        {
            itemNameImage.Update();
            itemNumImage.Update();
            selectedBack.Update();
            unselectedBack.Update();
        }

        public void Draw(Drawing d)
        {
            selectedBack.Draw(d);
            //unselectedBack.Draw(d);
            itemNameImage.Draw(d);
            itemNumImage.Draw(d);
        }
    }
}
