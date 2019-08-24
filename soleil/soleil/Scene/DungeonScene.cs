using Soleil.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class DungeonScene: Scene
    {
        DungeonMaster master;
        public DungeonScene(SceneManager sm, PersonParty party, DungeonName name)
            :base(sm)
        {
            master = new DungeonMaster(name);
        }

        public override void Update()
        {
            base.Update();
            master.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            master.Draw(sb);
        }
    }
}
