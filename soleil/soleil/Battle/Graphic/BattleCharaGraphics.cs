using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Soleil.Battle
{
    class BattleCharaGraphics
    {
        BattleCharaAnimation bcAnimation;
        StatusUI statusUI;
        public BattleCharaGraphics(Character chara, Vector statusPos, Vector charaPos)
        {
            statusUI = new StatusUI(chara.Status.AScore.HPMAX, chara.Status.AScore.MPMAX, statusPos);
            bcAnimation = new BattleCharaAnimation(charaPos);
        }

        public void Damage(int decreasedHP = 0, int decreasedMP = 0)
        {
            statusUI.Damage(decreasedHP, decreasedMP);
        }

        public void Attack(int consumeMP = 0)
        {
            bcAnimation.SetMotion(BattleCharaMotionType.Magic);
            statusUI.Damage(decreasedMP: consumeMP);
        }

        public void Update()
        {
            statusUI.Update();
            bcAnimation.Update();
        }

        public void Draw(Drawing d)
        {
            statusUI.Draw(d);
            bcAnimation.Draw(d);
        }
    }
}
