using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Soleil
{
    /// <summary>
    /// 枠サイズが可変のウィンドウ．
    /// </summary>
    class VariableWindow : Window
    {
        // ----- Constants
        const TextureID Texture = TextureID.MessageWindow;
        /// <summary>
        /// Contentの端からの距離
        /// </summary>
        protected const int Spacing = 40;
        /// <summary>
        /// ウィンドウフレームの幅
        /// </summary>
        const int FrameSize = 40;

        // -----
        protected override float Alpha => skinImg.Alpha;
        readonly Vector SpacingVec = new Vector(Spacing);
        protected override Vector SpaceVector => SpacingVec;
        readonly Vector SpaceVec = new Vector(Spacing);
        static Texture2D frameTextureForCalc;
        readonly UIImage skinImg;
        readonly UIImage[] frameImgs;

        Vector size;

        public VariableWindow(Vector _pos, Vector _size, WindowTag _tag, WindowManager wm, bool isStatic = false)
            :base(_pos, _tag, wm)
        {
            var center = true;
            frameTextureForCalc = Resources.GetTexture(Texture);
            size = _size;

            frameImgs = new UIImage[]
            {
                // 左上
                new UIImage(Texture, Pos + new Vector(FrameSize / 2, FrameSize / 2), DiffPos,Depth, center, isStatic),
                // 右上
                new UIImage(Texture, Pos + new Vector(FrameSize / 2 + size.X - FrameSize, FrameSize / 2), DiffPos,Depth,center, isStatic),
                // 左下
                new UIImage(Texture, Pos + new Vector(FrameSize / 2, size.Y - FrameSize / 2), DiffPos,Depth,center, isStatic),
                // 右下
                new UIImage(Texture, Pos + new Vector(size.X - FrameSize / 2, size.Y - FrameSize / 2), DiffPos,Depth,center, isStatic),
                // 上部
                new UIImage(Texture, Pos + new Vector(size.X / 2, FrameSize / 2), DiffPos,Depth,center, isStatic),
                // 左
                new UIImage(Texture, Pos + new Vector(FrameSize / 2, size.Y / 2), DiffPos,Depth,center, isStatic),
                // 右
                new UIImage(Texture, Pos + new Vector(-FrameSize / 2 + size.X, size.Y / 2), DiffPos,Depth,center, isStatic),
                // 下
                new UIImage(Texture, Pos + new Vector(size.X / 2, size.Y - FrameSize / 2), DiffPos,Depth,center, isStatic),
            };
            var rects = new[]
            {
                new Rectangle(0, 0, FrameSize, FrameSize),
                new Rectangle(frameTextureForCalc.Width - FrameSize, 0, FrameSize, FrameSize),
                new Rectangle(0, frameTextureForCalc.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(frameTextureForCalc.Width - FrameSize, frameTextureForCalc.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(FrameSize, 0, frameTextureForCalc.Width - 2 * FrameSize, FrameSize),
                new Rectangle(0, FrameSize, FrameSize, frameTextureForCalc.Height - 2 * FrameSize),
                new Rectangle(frameTextureForCalc.Width - FrameSize, FrameSize, FrameSize, frameTextureForCalc.Height - 2 * FrameSize),
                new Rectangle(FrameSize, frameTextureForCalc.Height - FrameSize, frameTextureForCalc.Width - 2 * FrameSize, FrameSize)
            };
            var sizes = new[]
            {
                Vector.One,
                Vector.One,
                Vector.One,
                Vector.One,
                new Vector((size.X - 2 * FrameSize) / (frameTextureForCalc.Width - 2 * FrameSize), 1),
                new Vector(1, (size.Y - 2 * FrameSize) / (frameTextureForCalc.Height - 2 * FrameSize)),
                new Vector(1, (size.Y - 2 * FrameSize) / (frameTextureForCalc.Height - 2 * FrameSize)),
                new Vector((size.X - 2 * FrameSize) / (frameTextureForCalc.Width - 2 * FrameSize), 1),
            };

            for (int i = 0; i < frameImgs.Length; i++)
            {
                frameImgs[i].Rectangle = rects[i];
                frameImgs[i].Size = sizes[i];
                frameImgs[i].FadeSpeed = FadeSpeed;
            }

            skinImg = new UIImage(Texture, Pos + new Vector(size.X, size.Y) / 2, DiffPos, Depth, center, isStatic, 0);
            skinImg.FadeSpeed = FadeSpeed;
            skinImg.Rectangle = new Rectangle(FrameSize, FrameSize, frameTextureForCalc.Width - 2 * FrameSize, frameTextureForCalc.Height - 2 * FrameSize);
            skinImg.Size = new Vector((size.X - 2 * FrameSize) / (frameTextureForCalc.Width - 2 * FrameSize), (size.Y - 2 * FrameSize) / (frameTextureForCalc.Height - 2 * FrameSize));

            AddComponents(frameImgs.Concat(new[] { skinImg }).ToArray());
        }

        public override void Call()
        {
            base.Call();
            skinImg.Call();
            for (int i = 0; i < frameImgs.Length; i++) frameImgs[i].Call();
        }

        public override void Quit()
        {
            base.Quit();
            skinImg.Quit();
            for (int i = 0; i < frameImgs.Length; i++) frameImgs[i].Quit();
        }
    }
}