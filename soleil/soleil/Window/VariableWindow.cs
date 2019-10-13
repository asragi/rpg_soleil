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
        const TextureID BackTexture = TextureID.MessageWindowBack;
        private static readonly Vector BackDiff = new Vector(4, 4);
        /// <summary>
        /// Contentの端からの距離
        /// </summary>
        protected const int Spacing = 15;
        private static readonly Vector SpacingVec = new Vector(Spacing);
        protected readonly Vector Size;

        protected bool IsStatic;
        VariableRectangle windowTexture;
        VariableRectangle backTexture;

        public VariableWindow(Vector _pos, Vector _size, WindowTag _tag, WindowManager wm, bool isStatic = false)
            : base(LimitWindowPos(_pos, _size, isStatic), _tag, wm)
        {
            Size = _size;
            IsStatic = isStatic;
            windowTexture = new VariableRectangle(Texture, Pos, DiffPos, _size, Depth, isStatic);
            backTexture = new VariableRectangle(BackTexture, Pos + BackDiff, DiffPos, _size, Depth, isStatic);
            AddComponents(backTexture, windowTexture);
        }

        protected override float Alpha => windowTexture.Alpha;
        protected override Vector SpaceVector => SpacingVec;

        /// <summary>
        /// ウィンドウが画面の外にはみ出すのを防止する計算．
        /// </summary>
        private static Vector LimitWindowPos(Vector _pos, Vector _size, bool isStatic)
        {
            if (isStatic)
            {
                return CalcClamp(_pos, _size);
            }
            var cameraPos = CameraManager.GetInstance().NowCamera;
            return CalcClamp(_pos - cameraPos, _size) + cameraPos;

            Vector CalcClamp(Vector vec, Vector size)
            {
                int xLim = Game1.VirtualWindowSizeX;
                int yLim = Game1.VirtualWindowSizeY;
                double x = MathEx.Clamp(vec.X, (xLim - size.X), 0);
                double y = MathEx.Clamp(vec.Y, (yLim - size.Y), 0);
                return new Vector(x, y);
            }
        }
    }
}