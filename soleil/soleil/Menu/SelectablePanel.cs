using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// 縦に並ぶ選択式メニューウィンドウの選択項目の抽象クラス
    /// </summary>
    abstract class SelectablePanel
    {
        public readonly Vector PanelSize = new Vector(300, 36);
        /// <summary>
        /// パネルの文字描画を左上からどれだけの位置にするか
        /// </summary>
        protected readonly Vector Spacing = new Vector(8, 4);

        protected readonly FontID ItemFont = FontID.KkBlack;

        public abstract string Desctiption { get; }
        /// <summary>
        /// 親要素に対する相対的な座標．
        /// </summary>
        public virtual Vector LocalPos { get; set; }
        // ウィンドウ
        protected BasicMenu BasicMenu;
        // 項目名の描画
        TextWithVal itemNameImage;
        readonly public String ItemName;
        protected int Val { set => itemNameImage.Val = value; }
        protected bool EnableVal { set => itemNameImage.EnableValDisplay = value; }
        public readonly Vector ItemNumPosDiff = new Vector(360, 0);

        // 選択状態の背景（これCursorとしてくらすにしたほうがよいきがする）
        Image selectedBack;
        bool isSelected;

        public SelectablePanel(String itemName, BasicMenu parent)
        {
            BasicMenu = parent;
            ItemName = itemName;
            // Set Font Image
            itemNameImage = new TextWithVal(ItemFont, LocalPos + parent.Pos, (int)ItemNumPosDiff.X);
            itemNameImage.Text = itemName;

            // 選択状態を示すやつ
            selectedBack = new Image(0, Resources.GetTexture(TextureID.MenuSelected), LocalPos + parent.Pos, DepthID.Message, false, true, 0);
        }

        public virtual void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            if (isSelected) selectedBack.Fade(duration, _easeFunc, isFadeIn);
        }

        public virtual void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            selectedBack.MoveTo(target, duration, _easeFunc);
        }

        public void SetSelectedAndFade(bool select)
        {
            if (select) // 選択されているとき．
            {
                OnSelected();
            }
            else if (!select && isSelected) // 選択が解除された瞬間
            {
                OnUnselected();
            }
            isSelected = select;
        }

        protected virtual void OnSelected()
        {
            selectedBack.Fade(20, MenuSystem.EaseFunc, true);
            itemNameImage.Font = FontID.WhiteOutlineGrad;
        }

        /// <summary>
        /// 選択が解除された瞬間に行われる処理．
        /// </summary>
        protected virtual void OnUnselected()
        {
            selectedBack.Fade(5, MenuSystem.EaseFunc, false);
            itemNameImage.Font = FontID.KkBlack;
        }

        public virtual void Update()
        {

            itemNameImage.Update();
            itemNameImage.Pos = BasicMenu.Pos + Spacing + LocalPos;
            itemNameImage.Alpha = BasicMenu.Alpha;
            selectedBack.Update();
            selectedBack.Pos = LocalPos + BasicMenu.Pos;
        }

        public virtual void Draw(Drawing d)
        {
            selectedBack.Draw(d);
            itemNameImage.Draw(d);
        }
    }
}
