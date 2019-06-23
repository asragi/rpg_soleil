using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    /// <summary>
    /// キャラクターのデータを受け取って内容の更新を行うクラス
    /// </summary>
    interface IPersonUpdate
    {
        void RefreshWithPerson(Person p);
    }
}
