using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.ConversationRead
{
    class ConversationPerson
    {
        public string Name { get; private set; }
        public int Position { get; private set; }

        public ConversationPerson(string name, int position)
        {
            (Name, Position) = (name, position);
        }
    }
}
