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
        public abstract string Desctiption { get; }
        /// <summary>
        /// 親要素に対する相対的な座標．
        /// </summary>
        public virtual Vector LocalPos { get; set; }
        // ウィンドウ
        protected BasicMenu BasicMenu;
        public virtual Vector ItemNumPosDiff { get => new Vector(360, 0); }
        
        protected bool IsSelected;

        public SelectablePanel(BasicMenu parent)
        {
            BasicMenu = parent;
        }

        public virtual void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {}

        public virtual void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {}

        public void SetSelectedAndFade(bool select)
        {
            if (select) // 選択されているとき．
            {
                OnSelected();
            }
            else if (!select && IsSelected) // 選択が解除された瞬間
            {
                OnUnselected();
            }
            IsSelected = select;
        }

        protected virtual void OnSelected()
        {}
        
        protected virtual void OnUnselected()
        {}

        public abstract void Update();
        public abstract void Draw(Drawing d);
    }

}
