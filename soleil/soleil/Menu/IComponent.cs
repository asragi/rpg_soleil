using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    interface IComponent
    {
        void Update();
        void Call();
        void Quit();
        void Draw(Drawing d);
    }
}
