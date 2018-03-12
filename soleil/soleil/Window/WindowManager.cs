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
        List<Window> windows;
        public WindowManager()
        {
            windows = new List<Window>();
        }

        public void Add(Window w)
        {
            windows.Add(w);
        }

        public void Update()
        {
            windows.ForEach(s => s.Update());
        }

        public void Draw(Drawing d)
        {
            windows.ForEach(s => s.Draw(d));
        }

    }
}
