using Soleil.Map.WorldMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// ワールドマップに移動するイベントのクラス
    /// </summary>
    class ToWorldMapEvent: EventBase
    {
        SceneManager sceneManager;
        PersonParty party;
        WorldPointKey destinationKey;
        Transition transition;

        public ToWorldMapEvent(PersonParty pp, WorldPointKey dest)
            : base()
        {
            sceneManager = SceneManager.GetInstance();
            party = pp;
            destinationKey = dest;
            transition = Transition.GetInstance();
        }

        public override void Start()
        {
            base.Start();
            transition.SetMode(TransitionMode.FadeOut);
        }

        public override void Execute()
        {
            base.Execute();
            // Transition終了でシーン切り替え
            if (transition.GetTransitionMode() == TransitionMode.None)
                new WorldMapScene(sceneManager, party, destinationKey);
        }
    }
}
