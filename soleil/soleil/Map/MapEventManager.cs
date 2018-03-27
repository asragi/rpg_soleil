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
        private MapInputManager mapInputManager;

        // singleton
        private static MapEventManager mapEventManager = new MapEventManager();
        private MapEventManager() {
            wm = WindowManager.GetInstance();
        }
        public static MapEventManager GetInstance() => mapEventManager;
        public void SetMapInputManager(MapInputManager m) => mapInputManager = m;

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

        /// <summary>
        /// 選択肢を表示する.
        /// </summary>
        public void CreateSelectWindow(Vector pos,Vector size,int tag, params string[] option)
        {
            new SelectableWindow(pos, size, tag, wm, option);
            wm.SetNowSelectWindow(tag);
            SetFocusSelectWindow(tag);
        }

        /// <summary>
        /// 選んだ選択肢のindexを返す.
        /// </summary>
        /// <returns>未選択時は-1を返す.</returns>
        public int ReturnSelectIndex(int tag)
        {
            return wm.GetDecideIndex();
        }

        /// <summary>
        /// 選択肢のフォーカスをウィンドウに変更する.
        /// </summary>
        private void SetFocusSelectWindow(int tag)
        {
            mapInputManager.SetFocus(MapFocus.Window);
        }

        /// <summary>
        /// Inputのフォーカスをプレイヤーに変更する.
        /// </summary>
        public void SetFocusPlayer()
        {
            mapInputManager.SetFocus(MapFocus.Player);
        }


        public void Update()
        {

        }
    }
}
