﻿using Microsoft.Xna.Framework.Graphics;
using Soleil.Event;
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
        Flare1,
        Flare2,
        Somnia1,
        Somnia2,
        Somnia4,
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

        protected EventSequence[] EventSequences;

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
            EventSequenceUpdate();
            bm.Update();
            menuSystem.Update();
            mapInputManager.Update();
            MapCameraManager.Update();
        }

        /// <summary>
        /// Map遷移後も前Mapで発動しているEventがあれば
        /// </summary>
        public void EventUpdate()
        {
            om.EventUpdate();
            EventSequenceUpdate();
        }

        private void EventSequenceUpdate()
        {
            if (EventSequences == null) return;
            for (int i = 0; i < EventSequences.Length; i++) EventSequences[i].Update();
        }

        public void SetPlayerPos(Vector pos) => om.SetPlayerPos(pos);
        public void SetPlayerDir(Direction dir) => player.Direction = dir;

        virtual public void Draw(Drawing sb)
        {
            menuSystem.Draw(sb);
            bm.Draw(sb);
            om.Draw(sb);
        }
    }
}
