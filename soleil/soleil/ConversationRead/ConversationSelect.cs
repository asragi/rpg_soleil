using Soleil.Event;
using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    /// <summary>
    /// 会話シーンイベントで会話と同時に選択肢を表示するイベント
    /// </summary>
    class ConversationSelect: EventBase
    {
        readonly WindowManager wm = WindowManager.GetInstance();
        readonly Vector Position = new Vector(700, 100);
        SelectableWindow select;
        string[] options;
        public ConversationSelect(string[] _options)
        {
            options = _options;
        }

        public override void Start()
        {
            base.Start();
            select = new SelectableWindow(Position, false, options);
            select.Call();
            wm.SetNowSelectWindow(select.Tag);
            // FocusをWindowに設定
            var mim = MapInputManager.GetInstance();
            mim.SetFocus(InputFocus.Window);
        }

        public override void Execute()
        {
            base.Execute();
            // GetDecideIndex()が-1以外を返す（＝選択肢が決定された）とき、次のイベントへ。
            if (wm.GetDecideIndex() == -1) return;
            select.Quit();
            Next();
        }
    }
}
