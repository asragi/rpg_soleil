using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョンの基本となる3種の行動を選択するウィンドウ．
    /// </summary>
    class FirstSelectWindow
    {
        private static readonly Vector WindowPos = new Vector(100, 100);
        private static readonly string[] Options = new[] { "進む", "探索する", "入り口に戻る" };
        SelectableWindow window;
        public FirstSelectWindow()
        {
        }

        public void Call()
        {
            window = new SelectableWindow(WindowPos, true, Options);
            window.Call();
        }

        public void Quit() => window.Quit();

        public void OnInputUp()
        {
            window.UpCursor();
        }

        public void OnInputDown()
        {
            window.DownCursor();
        }

        public void OnInputSubmit()
        {
            switch (window.Index)
            {
                case 0:
                    GoNextFloor();
                    return;
                case 1:
                    Search();
                    return;
                case 2:
                    ReturnHome();
                    return;
            }
        }

        private void GoNextFloor() { }
        private void Search() { }
        private void ReturnHome() { }
    }
}
