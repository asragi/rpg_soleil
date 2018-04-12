using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// (Battle|Map)SceneにおいてWindowを管理するクラス
    /// </summary>
    class WindowManager
    {
        private static WindowManager windowManager = new WindowManager(); 
        List<Window> windows;
        SelectableWindow nowSelectWindow;
        private WindowManager()
        {
            windows = new List<Window>();
        }

        public static WindowManager GetInstance() => windowManager;

        public void Add(Window w)
        {
            windows.Add(w);
        }

        public void Update()
        {
            windows.RemoveAll(s => s.Dead);
            windows.ForEach(s => s.Update());
        }

        /// <summary>
        /// SelectableWindowのCursorを動かす
        /// </summary>
        /// <param name="key">入力されたKey</param>
        public void MoveCursor(Key key)
        {
            var selectWindows = windows.FindAll(s => s.Active && s is SelectableWindow);
            foreach (var item in selectWindows)
            {
                var s = item as SelectableWindow;
                if (key == Key.Up) s.UpCursor();
                else if (key == Key.Down) s.DownCursor();
            }
        }

        /// <summary>
        /// フォーカスする選択肢ウィンドウを変更する.
        /// </summary>
        public void SetNowSelectWindow(WindowTag tag)
        {
            var selectWindows = windows.FindAll(s => s.Active && s is SelectableWindow)
                .ConvertAll<SelectableWindow>(t=>(SelectableWindow)t);
            nowSelectWindow = selectWindows.FindLast(s => s.Tag == tag);
        }

        public void Decide()
        {
            if (nowSelectWindow == null) return;
            nowSelectWindow.Decide();
        }
        public int GetDecideIndex()
        {
            if (nowSelectWindow == null) return -1;
            return nowSelectWindow.ReturnIndex();
        }

        public void Destroy(WindowTag tag)
        {
            windows.FindAll(s => s.Tag == tag).ForEach(t => t.Destroy());
        }

        public void Draw(Drawing d)
        {
            windows.ForEach(s => s.Draw(d));
        }

    }
}
