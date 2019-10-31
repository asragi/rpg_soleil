using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    /// <summary>
    /// NewGameを選択した際に画面が遷移するまでの演出
    /// </summary>
    class NewGameTransition
    {
        private static int TransitionDuration = 100;
        SceneManager sceneManager;
        private int frame = 0;
        public NewGameTransition(SceneManager sm)
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
            if (frame == TransitionDuration) InitNewGame();
        }

        private void InitTransition()
        {
            Transition.GetInstance().SetMode(TransitionMode.FadeOut);
        }

        private void InitNewGame()
        {
            var party = SaveLoad.GetParty(isNew: true);
            new MapScene(sceneManager, party, Map.MapName.Opening, new Vector(-100, -100));
        }
    }
}
