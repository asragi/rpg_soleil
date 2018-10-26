using Soleil.Menu.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// 選ばれているものの詳細を表示するウィンドウ
    /// </summary>
    class DetailWindow
    {
        readonly Vector DrawStartPos = new Vector(30, 30);
        readonly Vector InitPos;
        DetailComponent[] details;
        PossessNum possessNum;

        public DetailWindow(Vector pos)
        {
            InitPos = pos;
            possessNum = new PossessNum(DrawStartPos + InitPos);
        }

        public void Call()
        {
            possessNum.Call();
        }

        public void Quit()
        {
            possessNum.Quit();
        }

        public void Update(SelectablePanel selectedPanel)
        {
            possessNum.Update(selectedPanel);
        }

        public void Draw(Drawing d)
        {
            possessNum.Draw(d);
        }
    }
}
