using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Conversation
{
    /// <summary>
    /// 会話シーンイベントで会話と同時に選択肢を表示するイベント
    /// </summary>
    class ConversationSelect: EventBase
    {
        readonly Vector Position = new Vector(300, 300);
        SelectableWindow select;
        string[] options;
        public ConversationSelect(string[] _options)
        {
            options = _options;
        }

        public override void Start()
        {
            select = new SelectableWindow(Position, false, options);
            base.Start();
        }

        public override void Execute()
        {
            base.Execute();
            Next();
        }
    }
}
