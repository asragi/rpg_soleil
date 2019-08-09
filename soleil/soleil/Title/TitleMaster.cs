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

            switch (Mode)
            {
                case TitleMode.None:
                    break;
                case TitleMode.FirstWindow:
                    break;
                case TitleMode.SelectSave:
                    break;
                case TitleMode.Load:
                    break;
                case TitleMode.NewGame:
                    newGame.TransitionUpdate();
                    break;
                case TitleMode.OptionSelect:
                    break;
                case TitleMode.Exit:
                    break;
                default:
                    break;
            }
        }

        public void Draw(Drawing d)
        {

        }
    }
}
