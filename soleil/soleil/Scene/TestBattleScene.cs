using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Battle;

namespace Soleil
{
    class TestBattleScene : Scene
    {
        BattleField bf;
        public TestBattleScene(SceneManager sm, PersonParty party)
            : base(sm)
        {
            bf = BattleField.GetInstance();
            bf.SetSceneManager(sm);
            var enemies = new List<EnemyCharacter> {
                new EnemyCharacter("敵" + 1.ToString(), CharacterType.TestEnemy),
                new EnemyCharacter("敵" + 2.ToString(), CharacterType.TestEnemy),
                new EnemyCharacter("敵" + 3.ToString(), CharacterType.TestEnemy),
            };
            bf.InitBattle(party, enemies);
        }

        override public void Update()
        {
            bf.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            bf.Draw(sb);
            base.Draw(sb);
        }
    }
}
