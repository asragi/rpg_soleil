using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// マップの固定オブジェクトのうち，animationするもの．
    /// </summary>
    class AnimationConstruct : MapConstruct
    {
        Animation anim;
        public AnimationConstruct(Vector _pos, Animation _anim, MapDepth depth, ObjectManager om)
            : base(_pos, TextureID.White, depth, om)
        {
            anim = _anim;
        }

        public override void Update()
        {
            base.Update();
            anim.Move();
        }

        public override void Draw(Drawing d)
        {
            // baseをDrawしない
            anim.Draw(d, Pos);
        }
    }
}
