using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum MapName
    {
        test,
    }
    abstract class Map
    {
        MapEventManager mapEventManager;
        MapInputManager mapInput;
        protected ObjectManager om;
        protected BoxManager bm;
        PlayerObject player;

        public Map(MapName _name)
        {
            om = ObjectManager.GetInstance();
            bm = new BoxManager(new MapData(_name), player);
            player = new PlayerObject(om, bm);
            mapInput = new MapInputManager(player);
            mapEventManager = MapEventManager.GetInstance();
            mapEventManager.SetMapInputManager(mapInput);
        }

        virtual public void Update()
        {
            om.Update();
            bm.Update();
            mapInput.Update();
            mapEventManager.Update();
        }

        virtual public void Draw(Drawing sb)
        {
            bm.Draw(sb);
            om.Draw(sb);
        }
    }
}
