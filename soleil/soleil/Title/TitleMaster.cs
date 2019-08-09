using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    enum TitleMode
    {
        None,
        FirstWindow,
        SelectSave,
        Load,
        NewGame,
        OptionSelect,
        Exit,
    }

    /// <summary>
    /// TitleSceneで用いるインスタンス各種を生成，更新するクラス．
    /// </summary>
    class TitleMaster
    {
        TitleInput input;
        FirstWindow firstWindow;
        NewGameTransition newGame;
        LoadGameTransition loadGame;
        ExitTransition exitTransition;

        public TitleMaster(SceneManager sm)
        {
            firstWindow = new FirstWindow(this);
            input = new TitleInput(this, firstWindow);
            newGame = new NewGameTransition(sm);
            loadGame = new LoadGameTransition(sm);
            exitTransition = new ExitTransition();
        }

        public TitleMode Mode { get; set; } = TitleMode.FirstWindow;

        public void Update()
        {
            input.Update();

            if (Mode == TitleMode.NewGame) newGame.TransitionUpdate();
            if (Mode == TitleMode.Load) loadGame.TransitionUpdate();
            if (Mode == TitleMode.Exit) exitTransition.TransitionUpdate();
        }

        public void Draw(Drawing d)
        {

        }
    }
}
