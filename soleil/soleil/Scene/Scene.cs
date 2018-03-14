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

        bool active,dead;
        public Scene(SceneManager sm)
        {
            active = true;
            dead = false;
            sm.Add(this);
        }

        virtual public void Update()
        {
            if (!active) return;
        } 

        virtual public void Draw(Drawing sb)
        {

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
