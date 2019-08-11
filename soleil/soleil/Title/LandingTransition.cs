using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    /// <summary>
    /// TitleSceneの最初のウィンドウが表示されるまでの演出
    /// </summary>
    class LandingTransition
    {
        /// <summary>
        /// 経過フレーム(key)に応じて関数(value)を実行する．
        /// </summary>
        private readonly Dictionary<int, System.Action> sequence;
        private readonly TitleGraphics graphics;
        private readonly TitleMaster master;

        int frame;

        public LandingTransition(TitleGraphics _graphics, TitleMaster _master)
        {
            graphics = _graphics;
            master = _master;

            sequence = new Dictionary<int, System.Action>();
            sequence.Add(60, PopUpBackground);
            sequence.Add(120, PopUpTitle);
            sequence.Add(180, End);
        }

        public void TransitionUpdate()
        {
            frame++;
            if (sequence.ContainsKey(frame)) sequence[frame]();
        }

        private void PopUpBackground() => graphics.CallBackImage();
        private void PopUpTitle() => graphics.CallLogo();
        public void End() => master.Mode = TitleMode.FirstWindow;
    }
}
