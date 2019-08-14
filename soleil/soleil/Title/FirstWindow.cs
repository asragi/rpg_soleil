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

        // Ref
        TitleMaster master;
        // Window
        SelectableWindow selectWindow;
        // Field
        bool saveExists;

        public FirstWindow(TitleMaster _master)
        {
            master = _master;
            saveExists = SaveLoad.FileExist();
            CallWindow();
        }

        public void OnInputUp()
        {
            selectWindow.UpCursor();
        }

        public void OnInputDown()
        {
            selectWindow.DownCursor();
        }

        public void OnInputSubmit()
        {
            switch (selectWindow.Index)
            {
                case 0:
                    SelectLoad();
                    return;
                case 1:
                    SelectNew();
                    return;
                case 2:
                    SelectOption();
                    return;
                case 3:
                    SelectExit();
                    return;
                default:
                    throw new Exception();
            }
        }

        private void SelectLoad()
        {
            if (!saveExists) return;
            master.Mode = TitleMode.SelectSave;
            QuitWindow();
        }

        private void SelectNew()
        {
            master.Mode = TitleMode.NewGame;
            QuitWindow();
        }

        private void SelectOption()
        {

        }

        private void SelectExit()
        {
            master.Mode = TitleMode.Exit;
            QuitWindow();
        }

        private void CallWindow()
        {
            selectWindow = new SelectableWindow(WindowPos, true, Commands);
            selectWindow.Call();
        }

        private void QuitWindow()
        {
            selectWindow.Quit();
        }
    }
}
