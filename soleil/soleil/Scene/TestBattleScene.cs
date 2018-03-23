using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestBattleScene:Scene
    {
        BattleField bf;
        public TestBattleScene(SceneManager sm)
            : base(sm)
        {
            bf = new BattleField();
        }

        override public void Update()
        {
            bf.Move();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            bf.Draw(sb);
            base.Draw(sb);
        }
    }
}
