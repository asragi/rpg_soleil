using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum MapName
    {
        test,
    }
    abstract class Map
    {
        protected MapData data;
        protected ObjectManager om;

        public Map(MapName _name)
        {
            data = new MapData(_name);
            om = new ObjectManager();
        }

        virtual public void Update()
        {
            data.Update();
            om.Update();
        }

        virtual public void Draw(SpriteBatch sb)
        {
            data.Draw(sb);
            om.Draw(sb);
        }
    }
}
