using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.ConversationRead
{
    /// <summary>
    /// 会話イベントシーン関連の管理クラス．1マップ1つ設置する．
    /// </summary>
    class ConversationSystem
    {
        ConversationPerson[] people;

        public void SetPeople(ConversationPerson[] p) => people = p;
    }
}
