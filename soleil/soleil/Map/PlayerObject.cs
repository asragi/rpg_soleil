namespace Soleil.Map
{
    class PlayerObject : DashCharacter
    {
        // const
        const int MoveSpeed = 3;
        const int RunSpeed = 8;
        const int MoveBoxNum = 11; // 移動先を判定するboxの個数（奇数）
        const int CheckBoxAngle = 15; // 移動先から左右n度刻みに判定用Boxを設置
        const int BoxSizeX = 25;
        const int BoxSizeY = 10;
        protected override CollideLayer CollideLayer => CollideLayer.Player;
        // Variables
        public bool Movable, visible;
        CollideBox[] moveBoxes; // 移動先が移動可能かどうかを判定するBox
        CollideBox decideBox; // 決定キーを押したときに飛び出す判定
        readonly Vector DecideBoxDist = new Vector(-20, 0);
        int decideBoxCount;
        int speed;
        Direction nextMoveDir;

        public PlayerObject(ObjectManager om, BoxManager bm)
            : base("player", new Vector(700, 400), new Vector(BoxSizeX, BoxSizeY), om, bm, false)
        {
            Movable = true;
            visible = true;
            speed = MoveSpeed;
            om.SetPlayer(this);

            moveBoxes = new CollideBox[MoveBoxNum];
            for (int i = 0; i < MoveBoxNum; i++)
            {
                moveBoxes[i] = new CollideBox(this, Vector.Zero, new Vector(BoxSizeX, BoxSizeY), CollideLayer.PlayerBox, bm);
            }
            decideBox = new CollideBox(this, Vector.Zero, new Vector(10, 10), CollideLayer.PlayerHit, bm);
            decideBox.IsActive = false;
            SetAnimation();
        }

        private void SetAnimation()
        {
            var posDiff = new Vector(0, -50);
            var standAnims = new AnimationData[9];
            var sPeriod = 8;
            standAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneStandR, posDiff, true, sPeriod);
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
            DecideBoxCheck();
            UpdatePosition();

            void UpdatePosition()
            {
                switch (nextMoveDir)
                {
                    case Direction.N:
                        MoveState = MoveState.Stand;
                        NeutralizeCollideBoxes();
                        break;
                    default:
                        // 前のフレームで変更されたmoveBoxの位置に，行けたら行く
                        (Pos, MoveState) = WallCheck(Pos);
                        SetCollideBoxes(nextMoveDir.Angle());
                        break;
                }
                // 向きを変更する
                Direction = (nextMoveDir == Direction.N) ? Direction : nextMoveDir;
                // nextMoveDirをリセットする．
                nextMoveDir = Direction.N;
            }
        }

        public void Walk()
        {
            speed = MoveSpeed;
        }
        public void Run()
        {
            speed = RunSpeed;
        }

        public override void Move(Direction dir)
        {
            // 既に入力がある場合，そちらを優先．
            if (nextMoveDir != Direction.N) return;
            nextMoveDir = dir;
        }

        public void SetPosition(Vector _pos) => Pos = _pos;
        protected override void ChangeDepth() => Depth = DepthID.Player;
        #region Box
        private (Vector, MoveState) WallCheck(Vector _pos)
        {
            for (int i = 0; i < moveBoxes.Length; i++)
            {
                // 他キャラクターとの衝突確認
                if (moveBoxes[i].GetCollideCharacter()) continue;
                if (moveBoxes[i].WallCollide) continue;
                return (_pos + moveBoxes[i].LocalPos, (speed == RunSpeed) ? MoveState.Dash : MoveState.Walk);
            }
            return (_pos, MoveState.Stand); // どこにも移動できなさそうなとき
        }

        private void NeutralizeCollideBoxes()
        {
            for (int i = 0; i < moveBoxes.Length; i++)
            {
                moveBoxes[i].LocalPos = Vector.Zero;
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
                moveBoxes[i].LocalPos = resultPos;
            }
        }

        /// <summary>
        /// 決定キーで起動するイベントを判定する用boxを一瞬出す．
        /// </summary>
        public void ProjectHitBox()
        {
            decideBox.LocalPos = DecideBoxDist.Rotate(Direction.Angle());
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
            decideBox.LocalPos = Vector.Zero;
        }

        #endregion
    }
}
