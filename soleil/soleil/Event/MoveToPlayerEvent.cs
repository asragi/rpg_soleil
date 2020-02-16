using Soleil.Map;
using System;

namespace Soleil.Event
{
    /// <summary>
    /// プレイヤーの位置に向かって移動する
    /// </summary>
    class MoveToPlayerEvent: EventBase
    {
        readonly WalkCharacter character;
        readonly PlayerObject player;
        readonly float speed;
        readonly int assignedFrame;
        Vector targetPos;
        Vector walkVec;
        int frame;

        public MoveToPlayerEvent(WalkCharacter chara, PlayerObject p, int _frame = -1, float _speed = -1)
        {
            if ((_frame == -1 && _speed == -1) || (_frame != -1 && _speed != -1))
            {
                throw new ArgumentException("_speedか_frameのいずれかを指定してください。");
            }
            character = chara;
            player = p;
            speed = _speed;
            assignedFrame = _frame;
        }

        public override void Reset()
        {
            base.Reset();
            frame = 0;
        }

        public override void Start()
        {
            base.Start();
            targetPos = player.GetPosition();
            Vector fromPos = character.GetPosition();

            // Assigned by Frame
            if (assignedFrame > 0)
            {
                frame = assignedFrame;
                walkVec = (targetPos - fromPos) / assignedFrame;
                return;
            }
            // Assigned by speed
            walkVec = (targetPos - fromPos).GetUnit() * speed;
            frame = (int)((targetPos - fromPos).GetLength() / speed);
        }

        public override void Execute()
        {
            base.Execute();
            if (frame <= 0)
            {
                character.MoveState = MoveState.Stand;
                Next();
                return;
            }
            frame--;
            character.Move(walkVec);
        }
    }
}
