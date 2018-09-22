namespace Soleil.Event
{
    class SelectWindowEvent
        :WindowEventBase
    {
        string[] options;
        bool changeFocus;

        public SelectWindowEvent(Vector pos, WindowTag _tag, params string[] _options)
            :this(pos, _tag, true, _options) { }
        public SelectWindowEvent(Vector pos, WindowTag _tag, bool _changeFocus, params string[] _options)
            : base(pos, SelectableWindow.ProperSize(FontID.Test, _options), _tag)
        {
            changeFocus = _changeFocus;
            options = _options;
        }

        public override void Execute()
        {
            var SelectWindow = new SelectableWindow(Pos, Size, Tag, Wm, options);
            if (changeFocus) Wm.SetNowSelectWindow(Tag);
            Next();
        }
    }
}
