using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    abstract class Scene
    {
        protected WindowManager wm;
        bool active,dead;
        public Scene(SceneManager sm)
        {
            wm = new WindowManager();
            active = true;
            dead = false;
            sm.Add(this);
        }

        virtual public void Update()
        {
            if (!active) return;
            wm.Update();
        }

        virtual public void Draw(Drawing sb)
        {
            wm.Draw(sb);
        }

        public void Kill()
        {
            dead = true;
        }

        public void SetActive(bool _active)
        {
            active = _active;
        }


        public bool IsDead()
        {
            return dead;
        }

        public bool IsActive()
        {
            return active;
        }

    }
}
