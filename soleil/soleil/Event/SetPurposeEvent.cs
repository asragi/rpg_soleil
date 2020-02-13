using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// 次の行動のヒントの表示を変更するイベント
    /// </summary>
    class SetPurposeEvent: EventBase
    {
        readonly PurposeName purposeName;
        public SetPurposeEvent(PurposeName pn)
        {
            purposeName = pn;
        }

        public override void Execute()
        {
            base.Execute();

            var purposeHolder = PurposeHolder.Instance;
            purposeHolder.SetPurpose(purposeName);
            Next();
        }
    }
}
