using Microsoft.Xna.Framework;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class VariableRectangle: MenuComponent
    {
        /// <summary>
        /// ウィンドウフレームの幅
        /// </summary>
        const int FrameSize = 10;
        readonly Image skinImg;
        readonly Image[] frameImgs;

        public VariableRectangle(TextureID texture, Vector pos, Vector diffPos, Vector size, DepthID depth, bool isStatic = false)
        {
            bool center = true;
            var tex = Resources.GetTexture(texture);
            int fadeSpeed = MenuSystem.FadeSpeed;

            frameImgs = new Image[]
            {
                // 左上
                new Image(texture, pos + new Vector(FrameSize / 2, FrameSize / 2), diffPos, depth, center, isStatic),
                // 右上
                new Image(texture, pos + new Vector(FrameSize / 2 + size.X - FrameSize, FrameSize / 2), diffPos,depth,center, isStatic),
                // 左下
                new Image(texture, pos + new Vector(FrameSize / 2, size.Y - FrameSize / 2), diffPos,depth,center, isStatic),
                // 右下
                new Image(texture, pos + new Vector(size.X - FrameSize / 2, size.Y - FrameSize / 2), diffPos,depth,center, isStatic),
                // 上部
                new Image(texture, pos + new Vector(size.X / 2, FrameSize / 2), diffPos,depth,center, isStatic),
                // 左
                new Image(texture, pos + new Vector(FrameSize / 2, size.Y / 2), diffPos,depth,center, isStatic),
                // 右
                new Image(texture, pos + new Vector(-FrameSize / 2 + size.X, size.Y / 2), diffPos,depth,center, isStatic),
                // 下
                new Image(texture, pos + new Vector(size.X / 2, size.Y - FrameSize / 2), diffPos,depth,center, isStatic),
            };

            var rects = new[]
{
                new Rectangle(0, 0, FrameSize, FrameSize),
                new Rectangle(tex.Width - FrameSize, 0, FrameSize, FrameSize),
                new Rectangle(0, tex.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(tex.Width - FrameSize, tex.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(FrameSize, 0, tex.Width - 2 * FrameSize, FrameSize),
                new Rectangle(0, FrameSize, FrameSize, tex.Height - 2 * FrameSize),
                new Rectangle(tex.Width - FrameSize, FrameSize, FrameSize, tex.Height - 2 * FrameSize),
                new Rectangle(FrameSize, tex.Height - FrameSize, tex.Width - 2 * FrameSize, FrameSize)
            };
            var sizes = new[]
            {
                Vector.One,
                Vector.One,
                Vector.One,
                Vector.One,
                new Vector((size.X - 2 * FrameSize) / (tex.Width - 2 * FrameSize), 1),
                new Vector(1, (size.Y - 2 * FrameSize) / (tex.Height - 2 * FrameSize)),
                new Vector(1, (size.Y - 2 * FrameSize) / (tex.Height - 2 * FrameSize)),
                new Vector((size.X - 2 * FrameSize) / (tex.Width - 2 * FrameSize), 1),
            };
            for (int i = 0; i < frameImgs.Length; i++)
            {
                frameImgs[i].Rectangle = rects[i];
                frameImgs[i].Size = sizes[i];
                frameImgs[i].FadeSpeed = fadeSpeed;
            }

            skinImg = new Image(texture, pos + new Vector(size.X, size.Y) / 2, diffPos, depth, center, isStatic, 0);
            skinImg.FadeSpeed = fadeSpeed;
            skinImg.Rectangle = new Rectangle(FrameSize, FrameSize, tex.Width - 2 * FrameSize, tex.Height - 2 * FrameSize);
            skinImg.Size = new Vector((size.X - 2 * FrameSize) / (tex.Width - 2 * FrameSize), (size.Y - 2 * FrameSize) / (tex.Height - 2 * FrameSize));
            AddComponents(frameImgs);
            AddComponents(skinImg);
        }

        public float Alpha
        {
            get => skinImg.Alpha;
            set
            {
                frameImgs.ForEach2(i => i.Alpha = value);
                skinImg.Alpha = value;
            }
        }
    }
}
