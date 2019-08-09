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

        public TitleMaster(SceneManager sm)
        {
            firstWindow = new FirstWindow(this);
            input = new TitleInput(this, firstWindow);
            newGame = new NewGameTransition(sm);
        }

        public TitleMode Mode { get; set; } = TitleMode.FirstWindow;

        public void Update()
        {
            input.Update();

            if (Mode == TitleMode.NewGame) newGame.TransitionUpdate();
            if (Mode == TitleMode.Load) return;
            if (Mode == TitleMode.Exit) return;
        }

        public void Draw(Drawing d)
        {

        }
    }
}
