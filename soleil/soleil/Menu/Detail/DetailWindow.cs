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
    class DetailWindow: MenuComponent
    {
        readonly Vector InitPos;
        public readonly static FontID Font = FontID.CorpM;
        ArmorDetail armorDetail;

        public DetailWindow(Vector pos)
        {
            armorDetail = new ArmorDetail(pos);
            AddComponents(new IComponent[] { armorDetail });
        }

        public void Refresh(SelectablePanel selectedPanel)
        {
            armorDetail.Refresh(selectedPanel);
        }
    }
}
