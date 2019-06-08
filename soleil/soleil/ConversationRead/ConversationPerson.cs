using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    class ConversationPerson
    {
        public string Name { get; private set; }
        // public int Position { get; private set; }
        public string Face { get; private set; }
        public ConversationPerson(string name /*, int position*/)
        {
            Name = name;
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
