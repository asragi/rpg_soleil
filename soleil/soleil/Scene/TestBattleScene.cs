﻿using System;
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
        public TestBattleScene(
            SceneManager sm, PersonParty party, CharacterType[] _enemies)
            : base(sm)
        {
            bf = BattleField.GetInstance();
            var enemies = new List<EnemyCharacter>();
            foreach (var name in _enemies)
            {
                enemies.Add(new EnemyCharacter("敵", name));
            }
            bf.SetSceneManager(sm);
            bf.InitBattle(party, enemies);
            Audio.PlayMusic(MusicID.Battle1);
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
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
