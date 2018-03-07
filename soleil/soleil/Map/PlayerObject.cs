using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Soleil
{
    class PlayerObject : MapObject
    {
        bool movable, visible;
        CollideBox existanceBox;
        // AnimationSet anim;
        public PlayerObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(200, 200);
            existanceBox = new CollideBox(this, Vector.Zero, new Soleil.Vector(30, 30), CollideLayer.Player,bm);
        }

        public override void Update()
        {
            // for Debug
            int speed = 3;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X)) speed = 6;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) pos.Y += -speed;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down)) pos.Y += speed;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right)) pos.X += speed;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) pos.X += -speed;


            base.Update();
        }

        public override void Draw(Drawing sb)
        {
            sb.Draw(pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
