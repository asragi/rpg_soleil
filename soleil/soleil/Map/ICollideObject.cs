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
        void OnCollisionEnter(CollideObject cb);
        void OnCollisionStay(CollideObject cb);
        void OnCollisionExit(CollideObject cb);
        Vector GetPosition();
    }
}
