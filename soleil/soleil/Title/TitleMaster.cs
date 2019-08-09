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

        public TitleMaster()
        {
            firstWindow = new FirstWindow();
            input = new TitleInput(this, firstWindow);
        }

        public TitleMode Mode { get; set; } = TitleMode.FirstWindow;

        public void Update()
        {
            input.Update();
        }

        public void Draw(Drawing d)
        {

        }
    }
}
