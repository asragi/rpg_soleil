using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// 街に入る際のトランジション処理
    /// </summary>
    class WorldMapTransition
    {
        bool initTransition = false;
        WorldMapScene scene;
        WorldPointKey keyMoveTo;
        Transition transition;

        public WorldMapTransition(WorldMapScene _scene)
        {
            transition = Transition.GetInstance();
            scene = _scene;
        }

        public void Update(WorldMapMode mode)
        {
            if (mode != WorldMapMode.Transition) return;
            if (transition.GetTransitionMode() != TransitionMode.None) return;

            scene.ChangeSceneToMap(keyMoveTo);
        }

        public void Init(WorldPointKey target)
        {
            keyMoveTo = target;
            transition.SetMode(TransitionMode.FadeOut);
        }
    }
}
