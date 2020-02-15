using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    /// <summary>
    /// TitleSceneでの入力を制御する．
    /// </summary>
    class TitleInput
    {
        private readonly TitleMaster master;
        private readonly FirstWindow firstWindow;

        private readonly InputSmoother inputSmoother;

        public TitleInput(TitleMaster _master, FirstWindow first)
        {
            master = _master;
            firstWindow = first;

            inputSmoother = new InputSmoother();
        }

        public void Update()
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = inputSmoother.SmoothInput(inputDir);

            switch (master.Mode)
            {
                case TitleMode.FirstWindow:
                    FirstWindowUpdate(smoothInput);
                    return;
                case TitleMode.SelectSave:
                    return;
                default:
                    return;
            }
        }

        private void FirstWindowUpdate(Direction dir)
        {
            if (dir == Direction.U) firstWindow.OnInputUp();
            if (dir == Direction.D) firstWindow.OnInputDown();
            if (KeyInput.GetKeyPush(Key.A)) firstWindow.OnInputSubmit();
        }
    }
}
