using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Soleil.Battle
{
    /// <summary>
    /// Animationの画像自体を指す
    /// </summary>
    enum BattleCharaAnimationType
    {
        Stand,
        Chant,
        Magic,
        Crisis,
        Victory,
        Down,

        Size,
    }

    /// <summary>
    /// 再生するAnimationを管理するclass
    /// BattleCharaMotionTypeを通じて再生の仕方を指定する
    /// </summary>
    class BattleCharaAnimation
    {
        public Vector Pos;
        Animation[] animation;
        BattleCharaAnimationType currentAnimationType;
        BattleCharaMotion bcMotion;
        BattleCharaMotionType currentMotionType;
        Character character;
        public BattleCharaAnimation(Vector pos, Character chara)
        {
            Pos = pos;
            character = chara;
            currentAnimationType = BattleCharaAnimationType.Stand;
            animation = new Animation[(int)BattleCharaAnimationType.Size];
            switch (chara.CharacterType)
            {
                case CharacterType.Lune:
                case CharacterType.Tella: //とりあえず
                    animation[(int)BattleCharaAnimationType.Stand] = new Animation(new AnimationData(AnimationID.BattleLuneStanding, true, 10));
                    animation[(int)BattleCharaAnimationType.Chant] = new Animation(new AnimationData(AnimationID.BattleLuneChant, true, 10));
                    animation[(int)BattleCharaAnimationType.Magic] = new Animation(new AnimationData(AnimationID.BattleLuneMagic, false, 10));
                    animation[(int)BattleCharaAnimationType.Crisis] = new Animation(new AnimationData(AnimationID.BattleLuneCrisis, true, 10));
                    animation[(int)BattleCharaAnimationType.Victory] = new Animation(new AnimationData(AnimationID.BattleLuneVictory, false, 5));
                    animation[(int)BattleCharaAnimationType.Down] = new Animation(new AnimationData(AnimationID.BattleLuneDown, true));
                    break;
                case CharacterType.Sunny:
                    animation[(int)BattleCharaAnimationType.Stand] = new Animation(new AnimationData(AnimationID.BattleSunnyStanding, true, 10));
                    animation[(int)BattleCharaAnimationType.Chant] = new Animation(new AnimationData(AnimationID.BattleSunnyStanding, true, 10));
                    animation[(int)BattleCharaAnimationType.Magic] = new Animation(new AnimationData(AnimationID.BattleSunnyMagic, new Vector(-30, 0), false, 10));
                    animation[(int)BattleCharaAnimationType.Crisis] = new Animation(new AnimationData(AnimationID.BattleSunnyStanding, true, 10));
                    animation[(int)BattleCharaAnimationType.Victory] = new Animation(new AnimationData(AnimationID.BattleSunnyStanding, true, 10));
                    animation[(int)BattleCharaAnimationType.Down] = new Animation(new AnimationData(AnimationID.BattleSunnyStanding, true, 10));
                    break;
            }

            SetMotion(BattleCharaMotionType.Stand);
        }


        public void Update()
        {
            var (retAnimType, retMotionType) = bcMotion.Update(this);
            if (retAnimType != currentAnimationType)
            {
                animation[(int)currentAnimationType].Reset();
                currentAnimationType = retAnimType;
            }
            if (retMotionType != currentMotionType)
            {
                currentMotionType = retMotionType;
                SetMotion(currentMotionType);
            }
            animation[(int)currentAnimationType].Move();
        }

        /// <summary>
        /// 再生するBattleCharaMotionを指定する
        /// </summary>
        public void SetMotion(BattleCharaMotionType motion)
        {
            switch (motion)
            {
                case BattleCharaMotionType.Stand:
                    bcMotion = new BattleCharaMotionChangingWithHP(character);
                    break;
                case BattleCharaMotionType.Chant:
                    bcMotion = new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Chant, 60);
                    break;
                case BattleCharaMotionType.Magic:
                    bcMotion = new BattleCharaMotionSeq(motion, new List<BattleCharaMotion>{
                        new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Chant, 40),
                        new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Magic, 75)
                        });
                    break;
                case BattleCharaMotionType.Victory:
                    bcMotion = new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Victory, 300);
                    break;
                case BattleCharaMotionType.Down:
                    bcMotion = new BattleCharaMotion(motion, BattleCharaAnimationType.Down);
                    break;
            }
        }

        public void Draw(Drawing d) =>
            animation[(int)currentAnimationType].Draw(d, Pos);
    }
}
