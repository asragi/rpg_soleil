using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class MapManager
    {
        Map nowMap;
        Map previousMap;
        public MapManager() { }

        public void AddNew(Map map)
        {
            previousMap = nowMap;
            nowMap = map;
        }

        public void Update()
        {
            // 移動前マップでイベント処理が終わっていない場合、続行する。
            previousMap?.EventUpdate();
            nowMap.Update();

        }

        public void Draw(Drawing d)
        {
            nowMap.Draw(d);
        }
    }
}
