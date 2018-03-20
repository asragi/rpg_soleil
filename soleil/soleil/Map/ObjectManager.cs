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
        private static ObjectManager objectManager = new ObjectManager();
        List<MapObject> objects;

        private ObjectManager()
        {
            objects = new List<MapObject>();
        }

        public static ObjectManager GetInstance() => objectManager;

        public void Add(MapObject obj)
        {
            objects.Add(obj);
        }

        public void Update()
        {
            objects.RemoveAll(s => s.IsDead());
            foreach (var item in objects)
            {
                item.Update();
            }
        }

        public void Draw(Drawing sb)
        {
            foreach (var item in objects)
            {
                item.Draw(sb);
            }
        }
    }
}
