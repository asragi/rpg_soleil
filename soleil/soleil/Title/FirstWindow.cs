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
        private static readonly Vector WindowPos = new Vector(162, 321);
        private static readonly string[] Commands = new[]
        {
            "Load Game", "New Game", "Options", "Exit"
        };

        // Ref
        TitleMaster master;
        // Window
        TitleCommandWindow selectWindow;
        // Field
        bool saveExists;

        public FirstWindow(TitleMaster _master)
        {
            master = _master;
            selectWindow = new TitleCommandWindow(WindowPos, Commands);
            saveExists = SaveLoad.FileExist();
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

        public void CallWindow()
        {
            selectWindow.Call();
        }

        public void Update() => selectWindow.Update();
        public void Draw(Drawing d) => selectWindow.Draw(d);

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

        private void QuitWindow()
        {
            // selectWindow.Quit();
        }
    }
}
