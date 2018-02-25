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
        List<Scene> scenes;
        public SceneManager(Scene s)
        {
            scenes = new List<Scene>();
            Add(s);
        }

        public void Add(Scene scene)
        {
            scenes.Add(scene);
        }

        public void Update()
        {
            scenes.RemoveAll(s => s.isDead());
            scenes.FindAll(s => s.isActive());
        }

        public void Draw(SpriteBatch sb)
        {
            scenes.Last().Draw(sb); // いい感じにする
        }
    }
}
