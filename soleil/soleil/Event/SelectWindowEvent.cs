using Soleil.Map;

namespace Soleil.Event
{
    class SelectWindowEvent
        :WindowEventBase
    {
        private const FontID Font = FontID.CorpM;
        string[] options;
        bool changeFocus;
        SelectableWindow selectable;

        /// <summary>
        /// オブジェクトを指定してウィンドウを出す．
        /// </summary>
        public SelectWindowEvent(ICollideObject obj, params string[] _options)
            :base(obj, SelectableWindow.ProperSize(Font, _options))
        {
            changeFocus = true;
            options = _options;
        }

        public SelectWindowEvent(Vector pos, WindowTag _tag, params string[] _options)
            :this(pos, _tag, true, _options) { }
        public SelectWindowEvent(Vector pos, WindowTag _tag, bool _changeFocus, params string[] _options)
            : base(pos, SelectableWindow.ProperSize(FontID.CorpM, _options), _tag)
        {
            changeFocus = _changeFocus;
            options = _options;
        }

        public override void Start()
        {
            base.Start();
            // SelectWindowを生成する．
            selectable = new SelectableWindow(PosFunc(), Size, Tag, Wm, false, options);
            selectable.Call();
            if (changeFocus) Wm.SetNowSelectWindow(Tag);
            // FocusをWindowに設定
            var mim = MapInputManager.GetInstance();
            mim.SetFocus(InputFocus.Window);
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
