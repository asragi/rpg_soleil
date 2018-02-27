using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    abstract class MapObject
    {
        protected Vector pos;
        protected bool dead;
        protected int frame;
        public MapObject(ObjectManager om)
        {
            dead = false;
            om.Add(this);
        }

        virtual public void Update()
        {
            frame++;
        }

        virtual public void Draw(Drawing sb)
        {
            
        }

        public Vector GetPosition()
        {
            return pos;
        }

        public bool IsDead()
        {
            return dead;
        }
    }
}
