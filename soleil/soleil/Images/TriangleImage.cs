using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Images
{
    /// <summary>
    /// 跳ねるような動きをする汎用三角形画像．
    /// </summary>
    class TriangleImage: Image
    {
        private static TextureID Texture = TextureID.Triangle;

        private readonly Vector VibDirection;
        private Vector delta;

        public TriangleImage(Vector pos, Vector posdiff, DepthID depth, bool centerOrigin = true, bool isStatic = true, float alpha = 0, float angle = 0)
            :base(Texture, pos, posdiff, depth, centerOrigin, isStatic, alpha)
        {
            Angle = (float)Math.PI * (angle / 180);
            VibDirection = new Vector(0, -1).Rotate(angle);
        }

        public override Vector Pos { get => base.Pos + delta; set => base.Pos = value; }

        /// <summary>
        /// 振動周期
        /// </summary>
        public int Duration { private get; set; } = 50;
        /// <summary>
        /// 振幅
        /// </summary>
        public int Amplitude { private get; set; } = 5;

        public override void Update()
        {
            base.Update();
            delta = VibDirection * Amplitude * Math.Abs(Math.Sin(((float)Frame/Duration) * Math.PI));
        }
    }
}
