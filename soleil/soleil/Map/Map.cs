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
        MapInputManager mapInputManager;
        MapCameraManager mapCameraManager;
        protected ObjectManager om;
        protected BoxManager bm;
        PlayerObject player;

        public Map(MapName _name)
        {
            om = ObjectManager.GetInstance();
            bm = new BoxManager(new MapData(_name), player);
            player = new PlayerObject(om, bm);
            mapInputManager = MapInputManager.GetInstance();
            mapInputManager.SetPlayer(player);
            mapEventManager = MapEventManager.GetInstance();
            mapCameraManager = new MapCameraManager(player);
            mapEventManager.SetMapInputManager(mapInputManager);
        }

        virtual public void Update()
        {
            om.Update();
            bm.Update();
            mapInputManager.Update();
            mapEventManager.Update();
            mapCameraManager.Update();
        }

        virtual public void Draw(Drawing sb)
        {
            bm.Draw(sb);
            om.Draw(sb);
        }
    }
}
