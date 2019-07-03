using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 衝突判定を行うオブジェクトに対するインターフェイス．
    /// </summary>
    interface ICollideObject
    {
        void OnCollisionEnter(CollideBox cb);
        void OnCollisionStay(CollideBox cb);
        void OnCollisionExit(CollideBox cb);
        Vector GetPosition();
    }
}
