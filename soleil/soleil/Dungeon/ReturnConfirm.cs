using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョンから退却する際の確認表示．
    /// </summary>
    class ReturnConfirm
    {
        private static readonly Vector WindowPos
            = new Vector(300, 100);
        private static readonly string[] Options
            = new[] { "入り口に戻る", "キャンセル" };
        private SelectableWindow selectWindow;
        // private MessageWindow message;

        private readonly DungeonMaster master;

        public ReturnConfirm(DungeonMaster _master)
        {
            master = _master;
        }

        public void Call()
        {
            selectWindow = new SelectableWindow(WindowPos, true, Options);
            selectWindow.Call();
        }

        public void Quit() => selectWindow.Quit();

        public void OnInputUp() => selectWindow.UpCursor();

        public void OnInputDown() => selectWindow.DownCursor();

        public void OnInputSubmit()
        {
            if (selectWindow.Index == 0)
            {
                master.Mode = DungeonMode.ReturnHome;
                Quit();
            }
            if (selectWindow.Index == 1) Cancel();
        }

        public void OnInputCancel() => Cancel();

        private void Cancel()
        {
            Quit();
            master.Mode = DungeonMode.FirstWindow;
        }
    }
}
