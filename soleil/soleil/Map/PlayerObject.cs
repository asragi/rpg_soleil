namespace Soleil
{
    public enum PlayerMoveDir:int { None=-1,R=0,UR=315,U=270,UL=225,L=180,DL=135,D=90,DR=45}

    class PlayerObject : MapObject
    {
        // const
        const int MoveSpeed = 3;
        const int RunSpeed = 8;
        const int MoveBoxNum = 11; // 移動先を判定するboxの個数（奇数）
        const int CheckBoxAngle = 15; // 移動先から左右n度刻みに判定用Boxを設置
        // 衝突判定大きさ
        const int CollideBoxWidth = 30;
        const int CollideBoxHeight = 30;

        // Variables
        bool movable, visible;
        CollideBox existanceBox;
        CollideBox[] moveBoxes; // 移動先が移動可能かどうかを判定するBox
        int speed;

        public PlayerObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            movable = true;
            visible = true;
            speed = MoveSpeed;
            om.SetPlayer(this);
            pos = new Vector(800, 800);

            var collideSize = new Vector(CollideBoxWidth, CollideBoxHeight);
            existanceBox = new CollideBox(this, Vector.Zero, collideSize, CollideLayer.Player,bm);

            moveBoxes = new CollideBox[MoveBoxNum];
            for (int i = 0; i < MoveBoxNum; i++)
            {
                moveBoxes[i] = new CollideBox(this, Vector.Zero, collideSize, CollideLayer.Player, bm);
            }
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

        public void Move(PlayerMoveDir dir)
        {
            var delta = new Vector(speed, 0);
            switch (dir)
            {
                case PlayerMoveDir.None:
                    delta = Vector.Zero;
                    NeutralizeCollideBoxes();
                    break;
                default:
                    delta = delta.Rotate((int)dir);
                    SetCollideBoxes((int)dir);
                    break;
            }
            pos += WallCheck();
        }

        public void SetPosition(Vector _pos) => pos = _pos;

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
            sb.Draw(pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
