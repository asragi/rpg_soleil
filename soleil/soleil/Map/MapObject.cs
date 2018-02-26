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
        public MapObject(ObjectManager om)
        {
            dead = false;
            om.Add(this);
        }

        virtual public void Update()
        {

        }

        virtual public void Draw(Drawing sb)
        {
            
        }

        public bool IsDead()
        {
            return dead;
        }
    }
}
