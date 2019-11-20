using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class SceneManager
    {
        private static SceneManager sceneManager = new SceneManager();
        Transition transition;
        public Scene NowScene => scenes.Last();
        private Scene beforeScene;
        List<Scene> scenes;
        bool changing;
        private SceneManager()
        {
            scenes = new List<Scene>();
            transition = Transition.GetInstance();
        }

        public static SceneManager GetInstance() => sceneManager;

        public void Add(Scene scene)
        {
            if (scenes.Count > 0)
            {
                beforeScene = NowScene;
                changing = true;
            }
            scenes.Add(scene);
        }

        public void KillNowScene() => NowScene.Kill();

        public void Update()
        {
            scenes.RemoveAll(s => s.IsDead());
            NowScene.Update();
            transition.Update();
        }

        public void Draw(Drawing sb)
        {
            if (changing)
            {
                beforeScene.Draw(sb);
                beforeScene = null;
                changing = false;
            }
            else
            {
                NowScene.Draw(sb); // いい感じにする
            }
            transition.Draw(sb);
        }
    }
}
