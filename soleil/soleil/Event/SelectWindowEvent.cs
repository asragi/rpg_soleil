using Soleil.Map;

namespace Soleil.Event
{
    class SelectWindowEvent
        :WindowEventBase
    {
        string[] options;
        bool changeFocus;
        SelectableWindow selectable;
        MapInputManager inputManager;

        public SelectWindowEvent(Vector pos, WindowTag _tag, MapInputManager mim, params string[] _options)
            :this(pos, _tag, true, mim, _options) { }
        public SelectWindowEvent(Vector pos, WindowTag _tag, bool _changeFocus, MapInputManager mim, params string[] _options)
            : base(pos, SelectableWindow.ProperSize(FontID.CorpM, _options), _tag)
        {
            changeFocus = _changeFocus;
            options = _options;
            inputManager = mim;
        }

        public override void Start()
        {
            base.Start();
            // SelectWindowを生成する．
            selectable = new SelectableWindow(Pos, Size, Tag, Wm, false, options);
            selectable.Call();
            if (changeFocus) Wm.SetNowSelectWindow(Tag);
            // FocusをWindowに設定
            inputManager.SetFocus(InputFocus.Window);
        }

        public override void Execute()
        {
            base.Execute();
            // GetDecideIndex()が-1以外を返す（＝選択肢が決定された）とき、次のイベントへ。
            if (Wm.GetDecideIndex() == -1) return;
            selectable.Quit();
            Next();
        }
    }
}
