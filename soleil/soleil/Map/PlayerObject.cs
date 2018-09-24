namespace Soleil.Map
{
    class PlayerObject : DashCharacter
    {
        // const
        const int MoveSpeed = 3;
        const int RunSpeed = 8;
        const int MoveBoxNum = 11; // 移動先を判定するboxの個数（奇数）
        const int CheckBoxAngle = 15; // 移動先から左右n度刻みに判定用Boxを設置

        // Variables
        bool movable, visible;
        CollideBox[] moveBoxes; // 移動先が移動可能かどうかを判定するBox
        int speed;

        public PlayerObject(ObjectManager om, BoxManager bm)
            : base(new Vector(800,800),null,om,bm,false)
        {
            movable = true;
            visible = true;
            speed = MoveSpeed;
            om.SetPlayer(this);

            moveBoxes = new CollideBox[MoveBoxNum];
            for (int i = 0; i < MoveBoxNum; i++)
            {
                moveBoxes[i] = new CollideBox(this, Vector.Zero, DefaultBoxSize, CollideLayer.Player, bm);
            }
            SetAnimation();
        }

        private void SetAnimation()
        {
            var posDiff = new Vector(0, -40);
            var standAnims = new AnimationData[9];
            var sPeriod = 8;
            standAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneStandR,posDiff, true, sPeriod);
            standAnims[(int)Direction.RD] = new AnimationData(AnimationID.LuneStandDR, posDiff, true, sPeriod);
            standAnims[(int)Direction.D] = new AnimationData(AnimationID.LuneStandD, posDiff, true, sPeriod);
            standAnims[(int)Direction.LD] = new AnimationData(AnimationID.LuneStandDL, posDiff, true, sPeriod);
            standAnims[(int)Direction.L] = new AnimationData(AnimationID.LuneStandL, posDiff, true, sPeriod);
            standAnims[(int)Direction.LU] = new AnimationData(AnimationID.LuneStandUL, posDiff, true, sPeriod);
            standAnims[(int)Direction.U] = new AnimationData(AnimationID.LuneStandU, posDiff, true, sPeriod);
            standAnims[(int)Direction.RU] = new AnimationData(AnimationID.LuneStandUR, posDiff, true, sPeriod);
            SetStandAnimation(standAnims);

            var walkAnims = new AnimationData[9];
            var wPeriod = 8;
            walkAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneWalkR, posDiff, true, wPeriod);
            walkAnims[(int)Direction.RD] = new AnimationData(AnimationID.LuneWalkDR, posDiff, true, wPeriod);
            walkAnims[(int)Direction.D] = new AnimationData(AnimationID.LuneWalkD, posDiff, true, wPeriod);
            walkAnims[(int)Direction.LD] = new AnimationData(AnimationID.LuneWalkDL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.L] = new AnimationData(AnimationID.LuneWalkL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.LU] = new AnimationData(AnimationID.LuneWalkUL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.U] = new AnimationData(AnimationID.LuneWalkU, posDiff, true, wPeriod);
            walkAnims[(int)Direction.RU] = new AnimationData(AnimationID.LuneWalkUR, posDiff, true, wPeriod);
            SetWalkAnimation(walkAnims);

            var dashAnims = new AnimationData[9];
            var dPeriod = 5;
            dashAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneWalkR, posDiff, true, dPeriod);
            dashAnims[(int)Direction.RD] = new AnimationData(AnimationID.LuneWalkDR, posDiff, true, dPeriod);
            dashAnims[(int)Direction.D] = new AnimationData(AnimationID.LuneWalkD, posDiff, true, dPeriod);
            dashAnims[(int)Direction.LD] = new AnimationData(AnimationID.LuneWalkDL, posDiff, true, dPeriod);
            dashAnims[(int)Direction.L] = new AnimationData(AnimationID.LuneWalkL, posDiff, true, dPeriod);
            dashAnims[(int)Direction.LU] = new AnimationData(AnimationID.LuneWalkUL, posDiff, true, dPeriod);
            dashAnims[(int)Direction.U] = new AnimationData(AnimationID.LuneWalkU, posDiff, true, dPeriod);
            dashAnims[(int)Direction.RU] = new AnimationData(AnimationID.LuneWalkUR, posDiff, true, dPeriod);
            SetDashAnimation(dashAnims);
        }

        public override void Update()
        {
            base.Update();
        }

        public void Stand()
        {
            MoveState = MoveState.Stand;
        }

        public void Walk()
        {
            MoveState = MoveState.Walk;
            speed = MoveSpeed;
        }
        public void Run()
        {
            MoveState = MoveState.Dash;
            speed = RunSpeed;
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.N:
                    NeutralizeCollideBoxes();
                    break;
                default:
                    SetCollideBoxes((int)KeyInput.GetDegreeDirection(1));
                    break;
            }

            // 向きを変更する
            Direction = (dir == Direction.N)? Direction : dir; // そもそもdir == None の場合がないようにしたい(TODO)
            // 行けたら行く
            Pos += WallCheck();
        }

        public void SetPosition(Vector _pos) => Pos = _pos;

        #region Box
        private Vector WallCheck()
        {
            for (int i = 0; i < moveBoxes.Length; i++)
            {
                if(moveBoxes[i].GetWallCollide() == false)
                   return moveBoxes[i].GetLocalPos();
            }
            return Vector.Zero; // どこにも移動できなさそうなとき
        }

        private void NeutralizeCollideBoxes()
        {
            for (int i = 0; i < moveBoxes.Length; i++)
            {
                moveBoxes[i].SetLocalPos(Vector.Zero);
            }
        }

        /// <summary>
        /// 移動先が移動可能か判定するboxを複数生成し位置を決定する.
        /// </summary>
        private void SetCollideBoxes(int centerAngle)
        {
            var speedVector = new Vector(-speed, 0);
            for (int i = 0; i < moveBoxes.Length; i++)
            {
                // 不思議計算でいい感じにする
                var modifyAngle = CheckBoxAngle * ((i + 1) / 2); // 0,15,15,30,30,45,45,.....
                if (i % 2 == 0) modifyAngle *= -1; // 0,15,-15,30,-30,45,...
                var resultAngle = centerAngle + modifyAngle;
                var resultPos = speedVector.Rotate(resultAngle);
                moveBoxes[i].SetLocalPos(resultPos);
            }
        }
        #endregion

        public override void Draw(Drawing sb)
        {
            sb.Draw(Pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
