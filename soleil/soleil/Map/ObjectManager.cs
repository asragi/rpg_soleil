using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class ObjectManager
    {
        List<MapObject> objects;

        public ObjectManager()
        {
            objects = new List<MapObject>();
        }

        public void Add(MapObject obj)
        {
            objects.Add(obj);
        }

        public void Update()
        {
            foreach (var item in objects)
            {
                item.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var item in objects)
            {
                item.Draw(sb);
            }
        }
    }
}
