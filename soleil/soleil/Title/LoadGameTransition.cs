using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    /// <summary>
    /// Loadするセーブファイルを選択後にゲームが開始されるまでの遷移演出．
    /// </summary>
    class LoadGameTransition
    {
        private static int TransitionDuration = 100;
        SceneManager sceneManager;
        private int frame = 0;
        public LoadGameTransition(SceneManager sm)
        {
            sceneManager = sm;
        }

        /// <summary>
        /// Transition処理の最中にのみ呼び出されるUpdate
        /// </summary>
        public void TransitionUpdate()
        {
            frame++;
            if (frame == TransitionDuration - Transition.TransitionTime) InitTransition();
            if (frame == TransitionDuration) InitLoadedGame();
        }

        private void InitTransition()
        {
            Transition.GetInstance().SetMode(TransitionMode.FadeOut);
        }

        private void InitLoadedGame()
        {
            var party = SaveLoad.GetParty(isNew: false);
            new MapScene(sceneManager, party, Map.MapName.MagistolRoom, new Vector(400, 400));
        }
    }
}
