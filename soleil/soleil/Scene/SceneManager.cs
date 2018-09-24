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
        List<Scene> scenes;
        private SceneManager()
        {
            scenes = new List<Scene>();
            transition = Transition.GetInstance();
        }

        public static SceneManager GetInstance() => sceneManager;

        public void Add(Scene scene)
        {
            scenes.Add(scene);
        }

        public void Update()
        {
            scenes.RemoveAll(s => s.IsDead());
            foreach (var s in scenes)
            {
                s.Update();
            }
            transition.Update();
        }

        public void Draw(Drawing sb)
        {
            scenes.Last().Draw(sb); // いい感じにする
            transition.Draw(sb);
        }
    }
}
