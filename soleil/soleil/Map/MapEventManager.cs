using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// Map上でのイベントを処理するクラス. MapObject等から呼び出す.
    /// </summary>
    class MapEventManager
    {
        private WindowManager wm;

        // singleton
        private static MapEventManager mapEventManager = new MapEventManager();
        private MapEventManager() {
            wm = WindowManager.GetInstance();
        }
        public static MapEventManager GetInstance() => mapEventManager;

        /// <summary>
        /// メッセージウィンドウを作る.
        /// </summary>
        /// <param name="tag">ウィンドウ識別用変数</param>
        public void CreateMessageWindow(Vector pos, Vector size,int tag)
        {
            new MessageWindow(pos, size, tag, wm);
        }

        /// <summary>
        /// ウィンドウを消去する.指定したtagのウィンドウをすべて消去する.
        /// </summary>
        /// <param name="tag">ウィンドウ識別用変数</param>
        public void DestroyWindow(int tag)
        {
            wm.Destroy(tag);
        }

        public void Update()
        {

        }
    }
}
