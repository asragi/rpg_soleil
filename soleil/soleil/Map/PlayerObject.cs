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
        CollideBox decideBox; // 決定キーを押したときに飛び出す判定
        int decideBoxCount;
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
            decideBox = new CollideBox(this, Vector.Zero, new Vector(10, 10), CollideLayer.PlayerHit, bm);
            decideBox.IsActive = false;
            SetAnimation();
        }

        private void SetAnimation()
        {
            var posDiff = new Vector(0, -40);
            var standAnims = new AnimationData[8];
            var sPeriod = 8;
            standAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneStandR,posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneStandDR, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneStandD, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneStandDL, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneStandL, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneStandUL, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneStandU, posDiff, true, sPeriod);
            standAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneStandUR, posDiff, true, sPeriod);
            SetStandAnimation(standAnims);

            var walkAnims = new AnimationData[8];
            var wPeriod = 8;
            walkAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneWalkR, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneWalkDR, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneWalkD, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneWalkDL, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneWalkL, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneWalkUL, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneWalkU, posDiff, true, wPeriod);
            walkAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneWalkUR, posDiff, true, wPeriod);
            SetWalkAnimation(walkAnims);

            var dashAnims = new AnimationData[8];
            var dPeriod = 5;
            dashAnims[(int)ObjectDir.R] = new AnimationData(AnimationID.LuneWalkR, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.DR] = new AnimationData(AnimationID.LuneWalkDR, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.D] = new AnimationData(AnimationID.LuneWalkD, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.DL] = new AnimationData(AnimationID.LuneWalkDL, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.L] = new AnimationData(AnimationID.LuneWalkL, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.UL] = new AnimationData(AnimationID.LuneWalkUL, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.U] = new AnimationData(AnimationID.LuneWalkU, posDiff, true, dPeriod);
            dashAnims[(int)ObjectDir.UR] = new AnimationData(AnimationID.LuneWalkUR, posDiff, true, dPeriod);
            SetDashAnimation(dashAnims);
        }

        public override void Update()
        {
            DecideBoxCheck();
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

            // 向きを変更する
            Direction = (dir == ObjectDir.None)? Direction : dir; // そもそもdir == None の場合がないようにしたい(TODO)
            // 行けたら行く
            Pos += WallCheck();
        }

        public void SetPosition(Vector _pos) => Pos = _pos;
        protected override void ChangeDepth() => Depth = DepthID.Player;
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

        /// <summary>
        /// 決定キーで起動するイベントを判定する用boxを一瞬出す．
        /// </summary>
        public void ProjectHitBox()
        {
            decideBox.SetLocalPos(new Vector(100, 0));
            decideBox.IsActive = true;
            decideBoxCount = 2;
        }

        /// <summary>
        /// 決定キーを押したときに出る判定boxの寿命判定
        /// </summary>
        private void DecideBoxCheck()
        {
            if (!decideBox.IsActive) return;
            decideBoxCount--;
            if (decideBoxCount > 0) return;
            decideBox.IsActive = false;
            decideBox.SetLocalPos(Vector.Zero);
        }

        #endregion

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
        }
    }
}
