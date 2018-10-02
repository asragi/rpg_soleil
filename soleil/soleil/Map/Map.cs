using Microsoft.Xna.Framework.Graphics;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    enum MapName
    {
        Somnia1,
        Somnia2,
    }
    abstract class MapBase
    {
        MapInputManager mapInputManager;
        protected MapCameraManager MapCameraManager;
        protected ObjectManager om;
        protected BoxManager bm;
        PlayerObject player;
        protected MapData MapData;
        MenuSystem menuSystem;

        protected MapConstruct[] MapConstructs;
        protected CameraPoint[] CameraPoints;

        public MapBase(MapName _name)
        {
            om = new ObjectManager();
            MapData = new MapData(_name);
            MapData.SetMapFlag();
            bm = new BoxManager(MapData, player);
            player = new PlayerObject(om, bm);
            menuSystem = new MenuSystem();
            mapInputManager = MapInputManager.GetInstance();
            mapInputManager.SetPlayer(player);
            mapInputManager.SetMenuSystem(menuSystem);
            MapCameraManager = new MapCameraManager(player);
        }

        virtual public void Update()
        {
            om.Update();
            bm.Update();
            menuSystem.Update();
            mapInputManager.Update();
            MapCameraManager.Update();
        }

        public void EventUpdate()
        {
            om.EventUpdate();
        }

        public void SetPlayerPos(Vector pos) => om.SetPlayerPos(pos);

        virtual public void Draw(Drawing sb)
        {
            menuSystem.Draw(sb);
            bm.Draw(sb);
            om.Draw(sb);
        }
    }
}
