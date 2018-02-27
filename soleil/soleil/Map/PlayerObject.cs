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
        // AnimationSet anim;
        public PlayerObject(ObjectManager om)
            : base(om)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Drawing sb)
        {
            sb.Draw(pos,Resources.GetTexture(TextureID.White),DepthID.Item);
            base.Draw(sb);
        }
    }
}
