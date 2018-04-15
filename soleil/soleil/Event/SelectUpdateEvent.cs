using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class SelectUpdateEvent
        :UpdateEventBase
    {
        WindowManager wm;
        WindowTag tag;
        public SelectUpdateEvent(WindowTag _tag)
            : base()
        {
            tag = _tag;
            wm = WindowManager.GetInstance();
        }

        public override void Execute()
        {
            // GetDecideIndex()が-1以外を返す（＝選択肢が決定された）とき、次のイベントへ。
            if (wm.GetDecideIndex() == -1) return;
            Next();
        }
    }
}
