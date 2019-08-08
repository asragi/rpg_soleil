using Microsoft.Xna.Framework.Graphics;
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
        MagistolRoom,
        MagistolCol1,
        MagistolCol3,
        MagistolShop,
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
        protected CharacterPictureHolder PictureHolder;
        protected ConversationSystem ConversationSystem;

        protected EventSequence[] EventSequences;
        private bool started;

        protected MapConstruct[] MapConstructs;
        protected CameraPoint[] CameraPoints;
        protected PersonParty Party;

        public MapBase(MapName _name, PersonParty _party, Camera cam)
        {
            var wm = WindowManager.GetInstance();
            om = new ObjectManager();
            MapData = new MapData(_name);
            MapData.SetMapFlag();
            bm = new MapBoxManager(MapData);
            player = new PlayerObject(om, bm);
            Party = _party;
            menuSystem = new MenuSystem(_party);
            mapInputManager = MapInputManager.GetInstance();
            mapInputManager.SetPlayer(player);
            mapInputManager.SetMenuSystem(menuSystem);
            MapCameraManager = new MapCameraManager(player, cam);
            PictureHolder = new CharacterPictureHolder();
            ConversationSystem = new ConversationSystem(wm);
        }

        protected virtual void Start()
        {
            started = true;
        }

        virtual public void Update()
        {
            if (!started) Start();
            om.Update();
            EventSequenceUpdate();
            bm.Update();
            menuSystem.Update();
            mapInputManager.Update();
            MapCameraManager.Update();
            PictureHolder.Update();
            ConversationSystem.Update();
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
            PictureHolder.Draw(sb);
            ConversationSystem.Draw(sb);
        }
    }
}
