﻿using Soleil.Map;

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

        public override void Start()
        {
            base.Start();
            // SelectWindowを生成する．
            var SelectWindow = new SelectableWindow(Pos, Size, Tag, Wm, options);
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
            Wm.Destroy(Tag);
            Next();
        }
    }
}
