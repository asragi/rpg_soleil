using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニュー画面で登場する小さなウィンドウの基底
    /// </summary>
    abstract class BasicMenu : MenuChild
    {
        protected const int FadeSpeed = 16; // アイテムメニューが出現するスピード
        protected const int RowSize = 8; // 現在のフォントサイズだと8項目がちょうどよい
        protected virtual Vector WindowPos => new Vector(330, 100);
        protected readonly Vector WindowPosDiff = new Vector(0, 20);

        protected readonly Vector ItemDrawStartPos = new Vector(25, 28);
        protected readonly int ItemPanelSpacing = 4;

        Image backImage;
        public Vector Pos { get { return backImage.Pos; } }
        public float Alpha { get => backImage.Alpha; }
        protected int Index;
        protected int InitIndex;
        protected SelectablePanel[] Panels;
        protected SelectablePanel[] AllPanels;
        protected int IndexSize;
        protected MenuDescription MenuDescription;
        public SelectablePanel SelectedPanel => Panels[Index];

        public BasicMenu(MenuComponent parent, MenuDescription desc)
            : base(parent)
        {
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuModalBack), WindowPos + WindowPosDiff, DepthID.MessageBack, false, true, 0);
            Index = 0;
            MenuDescription = desc;
        }

        protected void Init()
        {
            AllPanels = MakeAllPanels();
            Panels = SetPanels();
        }

        protected void Refresh()
        {
            Init();
        }

        public virtual void Call()
        {
            // Transition Images
            backImage.MoveTo(WindowPos, FadeSpeed, MenuSystem.EaseFunc);
            backImage.Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            foreach (var item in AllPanels)
            {
                item?.MoveTo(WindowPos + item.LocalPos, FadeSpeed, MenuSystem.EaseFunc);
                item?.Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            }
            RefreshSelected();
        }

        public virtual void Quit()
        {
            // Transition Images
            backImage.MoveTo(WindowPos + WindowPosDiff, FadeSpeed, MenuSystem.EaseFunc);
            backImage.Fade(FadeSpeed, MenuSystem.EaseFunc, false);
            foreach (var item in Panels)
            {
                item?.MoveTo(WindowPos + WindowPosDiff + item.LocalPos, FadeSpeed, MenuSystem.EaseFunc);
                item?.Fade(FadeSpeed, MenuSystem.EaseFunc, false);
            }
        }

        protected abstract SelectablePanel[] MakeAllPanels();

        protected SelectablePanel[] SetPanels()
        {
            if (AllPanels.Length <= 0) // アイテムを一つも持っていない
            {
                IndexSize = 1;
                var empty = new EmptyPanel(this);
                empty.LocalPos = ItemDrawStartPos;
                return new[] { empty };
            }
            var panelSize = Math.Min(AllPanels.Length, RowSize);
            IndexSize = panelSize;

            var tmp = new SelectablePanel[panelSize];
            for (int i = 0; i < panelSize; ++i)
            {
                tmp[i] = AllPanels[InitIndex + i];
                tmp[i].LocalPos = ItemDrawStartPos + new Vector(0, (tmp[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
            return tmp;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Quit();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Call();
        }

        public override void Update()
        {
            base.Update();
            backImage.Update();
            foreach (var item in Panels)
            {
                item?.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            backImage.Draw(d);
            foreach (var item in Panels)
            {
                item?.Draw(d);
            }
        }

        protected void RefreshSelected()
        {
            for (int i = 0; i < Panels.Length; i++)
            {
                Panels[i]?.SetSelectedAndFade(i == Index);
            }
            MenuDescription.Text = Panels[Index].Desctiption;
        }

        // Input
        public override void OnInputRight() { }
        public override void OnInputLeft() { }

        public override void OnInputUp()
        {
            if (Index == 0)
            {
                if (InitIndex > 0)
                {
                    InitIndex--;
                    Index = 0;
                }
                else
                {
                    InitIndex = Math.Max(0, AllPanels.Length - RowSize);
                    Index = Math.Max(0,Math.Min(AllPanels.Length, Panels.Length) - 1);
                }
                Panels = SetPanels();
                RefreshSelected();
                return;
            }
            Index = (Index - 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }

        public override void OnInputDown()
        {
            if (Index == RowSize - 1)
            {
                if (InitIndex < AllPanels.Length - RowSize)
                {
                    InitIndex++;
                    Index = Math.Min(AllPanels.Length, Panels.Length) - 1;
                }
                else
                {
                    InitIndex = 0;
                    Index = 0;
                }
                Panels = SetPanels();
                RefreshSelected();
                return;
            }
            Index = (Index + 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { ReturnParent(); }
    }
}
