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
            var standAnims = new AnimationData[8];
            var sPeriod = 8;
            standAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneWalkL, true, sPeriod);
            standAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            standAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneWalkR, true, sPeriod);
            SetStandAnimation(standAnims);

            var walkAnims = new AnimationData[8];
            var wPeriod = 8;
            walkAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneWalkR, true, wPeriod);
            walkAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneWalkR, true, wPeriod);
            walkAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneWalkR, true, wPeriod);
            walkAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneWalkL, true, wPeriod);
            walkAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneWalkL, true, wPeriod);
            walkAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneWalkL, true, wPeriod);
            walkAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneWalkR, true, wPeriod);
            walkAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneWalkR, true, wPeriod);
            SetWalkAnimation(walkAnims);

            var dashAnims = new AnimationData[8];
            var dPeriod = 8;
            dashAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneWalkR, true, dPeriod);
            dashAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneWalkR, true, dPeriod);
            dashAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneWalkR, true, dPeriod);
            dashAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneWalkL, true, dPeriod);
            dashAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneWalkL, true, dPeriod);
            dashAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneWalkL, true, dPeriod);
            dashAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneWalkR, true, dPeriod);
            dashAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneWalkR, true, dPeriod);
            SetDashAnimation(dashAnims);
        }

        public override void Update()
        {
            base.Update();
        }
        public void Walk()
        {
            speed = MoveSpeed;
        }
        public void Run()
        {
            speed = RunSpeed;
        }

        public void Move(ObjectDir dir)
        {
            var delta = new Vector(speed, 0);
            switch (dir)
            {
                case ObjectDir.None:
                    delta = Vector.Zero;
                    NeutralizeCollideBoxes();
                    break;
                default:
                    delta = delta.Rotate(dir.GetAngle());
                    SetCollideBoxes(dir.GetAngle());
                    break;
            }
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
            var speedVector = new Vector(speed, 0);
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
