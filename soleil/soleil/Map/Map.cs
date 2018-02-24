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
    class Map
    {
        protected MapData data;
        protected ObjectManager om;

        public Map(MapName _name)
        {
            data = new MapData(_name);
        }

        public void Update()
        {
            data.Update();
            om.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            data.Draw(sb);
            om.Draw(sb);
        }
    }
}
