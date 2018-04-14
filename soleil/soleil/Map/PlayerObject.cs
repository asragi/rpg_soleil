using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Soleil
{
    public enum PlayerMoveDir { None,R,UR,U,UL,L,DL,D,DR}

    class PlayerObject : MapObject
    {
        // const
        const int MoveSpeed = 3;
        const int RunSpeed = 6;
        const int MoveBoxNum = 7; // 移動先を判定するboxの個数
        const int CheckBoxAngle = 15; // 移動先から左右n度刻みに判定用Boxを設置

        bool movable, visible;
        CollideBox existanceBox;
        CollideBox[] moveBoxes; // 移動先が移動可能かどうかを判定するBox
        // AnimationSet anim;
        int speed;
        public PlayerObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            movable = true;
            visible = true;
            speed = MoveSpeed;
            om.SetPlayer(this);
            pos = new Vector(200, 200);
            existanceBox = new CollideBox(this, Vector.Zero, new Soleil.Vector(30, 30), CollideLayer.Player,bm);
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

        public void Move(Vector dir)
        {
            pos += dir * speed;
        }

        public void Move(PlayerMoveDir dir)
        {
            var delta = new Vector(speed, 0);
            switch (dir)
            {
                case PlayerMoveDir.None:
                    delta = Vector.Zero;
                    break;
                case PlayerMoveDir.R:
                    delta = delta.Rotate(0);
                    break;
                case PlayerMoveDir.UR:
                    delta = delta.Rotate(315);
                    break;
                case PlayerMoveDir.U:
                    delta = delta.Rotate(270);
                    break;
                case PlayerMoveDir.UL:
                    delta = delta.Rotate(225);
                    break;
                case PlayerMoveDir.L:
                    delta = delta.Rotate(180);
                    break;
                case PlayerMoveDir.DL:
                    delta = delta.Rotate(135);
                    break;
                case PlayerMoveDir.D:
                    delta = delta.Rotate(90);
                    break;
                case PlayerMoveDir.DR:
                    delta = delta.Rotate(45);
                    break;
                default:
                    break;
            }
            pos += delta;
        }


        public override void Draw(Drawing sb)
        {
            sb.Draw(pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
