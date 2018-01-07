using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// Actionによって生じた情報を持つ
    /// </summary>
    class Occurence
    {
        //Effect
        public string Message;
        //Target
        //Damage
        public Occurence(string message)
        {
            Message = message;
        }
    }
}
