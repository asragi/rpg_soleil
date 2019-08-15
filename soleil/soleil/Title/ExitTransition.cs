using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    class ExitTransition
    {
        private static int TransitionDuration = 60;
        private int frame = 0;
        public ExitTransition()
        {

        }

        /// <summary>
        /// Transition処理の最中にのみ呼び出されるUpdate
        /// </summary>
        public void TransitionUpdate()
        {
            frame++;
            if (frame == TransitionDuration - Transition.TransitionTime) InitTransition();
            if (frame == TransitionDuration) ExitGame();
        }

        private void InitTransition()
        {
            Transition.GetInstance().SetMode(TransitionMode.FadeOut);
        }

        private void ExitGame()
        {
            Game1.End = true;
        }
    }
}
