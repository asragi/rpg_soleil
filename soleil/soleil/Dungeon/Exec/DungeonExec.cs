using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// 時間経過に応じてイベントを起こすクラス．
    /// 処理の内容はExec()に書く．
    /// </summary>
    abstract class DungeonExec
    {
        protected Dictionary<int, System.Action> Actions;
        private int execFrame;

        public void ExecUpdate()
        {
            execFrame++;
            if (Actions.ContainsKey(execFrame)) Actions[execFrame]();
            Exec();
        }

        public virtual void Reset()
        {
            execFrame = 0;
        }

        protected abstract void Exec();

        protected void FadeIn()
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
        }

        protected void FadeOut()
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeOut);
        }
    }
}
