using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    class DungeonInput
    {
        // refs
        private readonly DungeonMaster master;
        private readonly FirstSelectWindow firstSelect;

        // instance
        private InputSmoother smoother;

        public DungeonInput(DungeonMaster _master, FirstSelectWindow fs)
        {
            master = _master;
            firstSelect = fs;
            smoother = new InputSmoother();
        }

        public void Update()
        {
            Direction input = KeyInput.GetStickInclineDirection(1);
            Direction dir = smoother.SmoothInput(input);
            switch (master.Mode)
            {
                case DungeonMode.FirstWindow:
                    SelectFirstWindow(dir);
                    return;
            }
        }

        private void SelectFirstWindow(Direction dir)
        {
            if (dir == Direction.U) firstSelect.OnInputUp();
            if (dir == Direction.D) firstSelect.OnInputDown();
            if (KeyInput.GetKeyPush(Key.A)) firstSelect.OnInputSubmit();
        }
    }
}
