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
    class PlayerObject : MapObject
    {
        // const
        const int MoveSpeed = 3;
        const int RunSpeed = 6;

        bool movable, visible;
        CollideBox existanceBox;
        // AnimationSet anim;
        int speed;
        public PlayerObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
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


        public override void Draw(Drawing sb)
        {
            sb.Draw(pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
