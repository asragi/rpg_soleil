using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class EmptyPanel : TextSelectablePanel
    {
        public override string Desctiption => "";
        public EmptyPanel(BasicMenu parent)
            : base("", parent, DepthID.MenuMessage) { EnableVal = false; }
    }
}
