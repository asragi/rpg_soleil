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

        public void Destroy(int tag)
        {
            windows.FindAll(s => s.Tag == tag).ForEach(t => t.Destroy());
        }

        public void Draw(Drawing d)
        {
            windows.ForEach(s => s.Draw(d));
        }

    }
}
