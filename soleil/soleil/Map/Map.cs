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
        protected CollideBox cm;
        PlayerObject player;

        public Map(MapName _name)
        {
            data = new MapData(_name);
            om = new ObjectManager();
            player = new PlayerObject(om);
            CollideBox.Init(player, data);
        }

        virtual public void Update()
        {
            CollideBox.Update();
            data.Update();
            om.Update();
        }

        virtual public void Draw(Drawing sb)
        {
            CollideBox.Draw(sb);
            data.Draw(sb);
            om.Draw(sb);
        }
    }
}
