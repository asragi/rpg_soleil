using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Playerのグラフィックを表示するためのwrapper．
    /// PlayerObjectをDungeonで使いやすいようにする．
    /// </summary>
    class PlayerObjectWrap
    {
        private readonly Vector InitialPosition = new Vector(10, 400);

        private readonly PlayerObject Player;
        private readonly BoxManager bm;
        private readonly ObjectManager om;


        public PlayerObjectWrap()
        {
            bm = new BoxManager();
            om = new ObjectManager();
            Player = new PlayerObject(om, bm);
            Player.SetPosition(InitialPosition);
        }

        public void Update()
        {
            bm.Update();
            om.Update();
        }

        public void Draw(Drawing d)
        {
            bm.Draw(d);
            om.Draw(d);
        }
    }
}
