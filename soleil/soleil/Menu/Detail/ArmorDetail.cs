using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Detail
{
    /// <summary>
    /// 防具の詳細性能表示クラス．
    /// </summary>
    class ArmorDetail : DetailComponent
    {
        readonly String ExplainText = "防御力";
        readonly Vector InitPos;

        FontImage explainText;
        FontImage valueText;
        public ArmorDetail(Vector _pos)
            :base()
        {
            InitPos = _pos;
            explainText = new FontImage(FontID.Test, InitPos, DepthID.Message, true, 0);
            valueText = new FontImage(FontID.Test, InitPos, DepthID.Message, true, 0);
        }

        public void Call()
        {

        }

        public void Quit()
        {

        }

        public void Update()
        {

        }

        public void Draw(Drawing d)
        {

        }
    }
}
