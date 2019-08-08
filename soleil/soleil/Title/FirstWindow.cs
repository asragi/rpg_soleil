using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    /// <summary>
    /// Titleで初めに現れるウィンドウ．
    /// </summary>
    class FirstWindow
    {
        private static readonly Vector WindowPos = new Vector(128, 186);
        private static readonly string[] Commands = new[]
        {
            "Load Game", "New Game", "Options", "Exit"
        };

        SelectableWindow selectWindow;

        public FirstWindow()
        {
            selectWindow = new SelectableWindow(WindowPos, true, Commands);
            selectWindow.Call();
        }

        public void OnInputUp()
        {
            selectWindow.UpCursor();
        }

        public void OnInputDown()
        {
            selectWindow.DownCursor();
        }
    }
}
