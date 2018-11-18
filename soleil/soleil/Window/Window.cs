using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// Windowの基本クラス
    /// </summary>
    class Window : MenuComponent
    {
        // todo:右下にくるくるするやつ

        /// <summary>
        /// Contentの端からの距離
        /// </summary>
        protected const int Spacing = 20;
        /// <summary>
        /// ウィンドウフレームの幅
        /// </summary>
        const int FrameSize = 10;
        const int FadeSpeed = 8;
        readonly Vector DiffPos = new Vector(0, 10);

        static Texture2D frameTexture;
        UIImage skinImg;
        UIImage[] frameImgs;
        /// <summary>
        /// pos : 左上基準
        /// </summary>
        protected Vector pos;
        Vector size;
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public bool Dead { get; protected set; }
        /// <summary>
        /// ウィンドウ識別用変数. 重複可能.
        /// </summary>
        public WindowTag Tag { get; private set; }
        protected int frame;
        bool quitStart = false;
        public Window(Vector _pos, Vector _size,WindowTag _tag, WindowManager wm)
        {
            var texID = TextureID.FrameTest;
            frameTexture = Resources.GetTexture(texID);
            pos = _pos;
            size = _size;
            Tag = _tag;
            Visible = true;
            Active = true;

            frameImgs = new UIImage[]
            {
                // 左上
                new UIImage(texID, pos + new Vector(FrameSize / 2, FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 右上
                new UIImage(texID, pos + new Vector(FrameSize / 2 + size.X - FrameSize, FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 左下
                new UIImage(texID, pos + new Vector(FrameSize / 2, size.Y - FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 右下
                new UIImage(texID, pos + new Vector(size.X - FrameSize / 2, size.Y - FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 上部
                new UIImage(texID, pos + new Vector(size.X / 2, FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 左
                new UIImage(texID, pos + new Vector(FrameSize / 2, size.Y / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 右
                new UIImage(texID, pos + new Vector(-FrameSize / 2 + size.X, size.Y / 2), DiffPos,DepthID.Frame,true, false, 1),
                // 下
                new UIImage(texID, pos + new Vector(size.X / 2, size.Y - FrameSize / 2), DiffPos,DepthID.Frame,true, false, 1),
            };
            var rects = new[]
            {
                new Rectangle(0, 0, FrameSize, FrameSize),
                new Rectangle(frameTexture.Width - FrameSize, 0, FrameSize, FrameSize),
                new Rectangle(0, frameTexture.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(frameTexture.Width - FrameSize, frameTexture.Height - FrameSize, FrameSize, FrameSize),
                new Rectangle(FrameSize, 0, frameTexture.Width - 2 * FrameSize, FrameSize),
                new Rectangle(0, FrameSize, FrameSize, frameTexture.Height - 2 * FrameSize),
                new Rectangle(frameTexture.Width - FrameSize, FrameSize, FrameSize, frameTexture.Height - 2 * FrameSize),
                new Rectangle(FrameSize, frameTexture.Height - FrameSize, frameTexture.Width - 2 * FrameSize, FrameSize)
            };
            var sizes = new[]
            {
                Vector.One,
                Vector.One,
                Vector.One,
                Vector.One,
                new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), 1),
                new Vector(1, (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize)),
                new Vector(1, (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize)),
                new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), 1),
            };

            for (int i = 0; i < frameImgs.Length; i++)
            {
                frameImgs[i].Rectangle = rects[i];
                frameImgs[i].Size = sizes[i];
                frameImgs[i].FadeSpeed = FadeSpeed;
            }

            skinImg = new UIImage(TextureID.FrameTest, pos + new Vector(size.X, size.Y) / 2, DiffPos, DepthID.Frame, true, false, 1);
            skinImg.FadeSpeed = FadeSpeed;
            skinImg.Rectangle = new Rectangle(FrameSize, FrameSize, frameTexture.Width - 2 * FrameSize, frameTexture.Height - 2 * FrameSize);
            skinImg.Size = new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize));

            Components = frameImgs.Concat(new[] { skinImg }).ToArray();
            wm.Add(this);
        }


        /// <summary>
        /// 継承後の振る舞いはMove()で記述する.
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (quitStart & skinImg.Alpha < 0.1) Destroy();
            // visibleなのにactiveという状態を回避したい
            Active = Visible ? Active : false;
            if (!Active) return;
            frame++;

            Move();
        }

        /// <summary>
        /// 継承先でのUpdate()記述用関数.
        /// </summary>
        protected virtual void Move(){}

        public override void Call()
        {
            skinImg.Call();
            for (int i = 0; i < frameImgs.Length; i++) frameImgs[i].Call();
        }

        public override void Quit()
        {
            quitStart = true;
            skinImg.Quit();
            for (int i = 0; i < frameImgs.Length; i++) frameImgs[i].Quit();
        }

        public void Destroy()
        {
            Dead = true;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            DrawContent(d);
        }

        virtual public void DrawContent(Drawing d)
        {

        }
    }
}
