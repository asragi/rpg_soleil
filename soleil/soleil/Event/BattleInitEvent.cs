using Soleil.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class BattleInitEvent: EventBase
    {
        readonly BattleData data;
        readonly SceneManager sceneManager;
        readonly PersonParty party;
        readonly Dictionary<int, System.Action> actions;
        int frame;
        MusicID beforeMusic;
        public BattleInitEvent(BattleData _d, PersonParty p)
        {
            data = _d;
            party = p;
            sceneManager = SceneManager.GetInstance();
            actions = new Dictionary<int, System.Action>();

            actions.Add(1, FadeOut);
            actions.Add(40, InitBattle);
            actions.Add(41, FadeIn);
            actions.Add(100, End);
        }

        public override void Execute()
        {
            base.Execute();
            frame++;
            if (actions.ContainsKey(frame)) actions[frame]();
        }

        private void InitBattle()
        {
            new TestBattleScene(sceneManager, party, data.Enemies);
        }

        private void End()
        {
            frame = 0;
            Next();
        }

        private void FadeOut()
        {
            beforeMusic = Audio.NowPlaying;
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeOut);
        }

        private void FadeIn()
        {
            Audio.PlayMusic(beforeMusic);
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
        }
    }
}

