using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    /// <summary>
    /// 会話シーンに登場するキャラクターグラフィックのクラス．
    /// </summary>
    class ConversationPerson
    {
        readonly int[] xPositionArray = new[] { 100,250,400,550,700 };
        const int Y = 100;

        public string Name { get; private set; }
        public int Position { get; private set; }
        public string Face { get; private set; }
        public ConversationPerson(string name, int position)
        {
            Name = name;
            Position = position;
        }

        public void SetFace(string face)
        {
            if (Face != face)
            {
                // 表情変化時のトランジション処理
            }

            Face = face;
        }

        public void Update()
        {

        }

        public void Draw(Drawing d)
        {

        }
    }
}
