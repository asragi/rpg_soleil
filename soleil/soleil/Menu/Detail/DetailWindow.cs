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
        readonly Vector DetailPos = new Vector(30, 100);
        readonly Vector InitPos;
        public readonly static FontID Font = FontID.Yasashisa;
        DetailComponent[] details;
        ArmorDetail armorDetail;
        PossessNum possessNum;

        public DetailWindow(Vector pos)
        {
            InitPos = pos;
            armorDetail = new ArmorDetail(DetailPos + InitPos);
            details = new DetailComponent[]
            {
                armorDetail
            };
            possessNum = new PossessNum(DrawStartPos + InitPos);
        }

        public void Call()
        {
            armorDetail.Call();
            possessNum.Call();
        }

        public void Quit()
        {
            armorDetail.Quit();
            possessNum.Quit();
        }

        public void Update(SelectablePanel selectedPanel)
        {
            armorDetail.Update(selectedPanel);
            possessNum.Update(selectedPanel);
        }

        public void Draw(Drawing d)
        {
            armorDetail.Draw(d);
            possessNum.Draw(d);
        }
    }
}
