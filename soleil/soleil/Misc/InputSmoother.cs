using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    class InputSmoother
    {
        const int InputWait = 6;
        int waitFrame;
        const int HeadWait = 20;
        int headWaitCount;
        bool inputCheck;

        /// <summary>
        /// 入力押しっぱなしでも毎フレーム移動しないようにする関数
        /// </summary>
        public Direction SmoothInput(Direction dir)
        {
            waitFrame--;
            if (dir != Direction.N)
            {
                headWaitCount--;
                if (headWaitCount > 0 && inputCheck) return Direction.N;
                if (waitFrame > 0) return Direction.N;
                waitFrame = InputWait;
                inputCheck = true;
                return dir;
            }
            inputCheck = false;
            waitFrame = 0;
            headWaitCount = HeadWait;
            return Direction.N;
        }
    }
}
