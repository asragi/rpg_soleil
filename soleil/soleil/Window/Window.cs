using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    class Window
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

        static Texture2D frameTexture, skinTexture;
        /// <summary>
        /// pos : 左上基準
        /// </summary>
        protected Vector pos;
        Vector size;
        public bool active { get; set; }
        public bool visible { get; set; }
        public bool Dead { get; protected set; }
        protected int frame;

        public Window(Vector _pos, Vector _size, WindowManager wm)
        {
            frameTexture = frameTexture ?? Resources.GetTexture(TextureID.FrameTest);
            pos = _pos;
            size = _size;
            visible = true;
            active = true;
            wm.Add(this);
        }


        /// <summary>
        /// 継承後の振る舞いはMove()で記述する.
        /// </summary>
        public void Update()
        {
            // visibleなのにactiveという状態を回避したい
            active = visible ? active : false;
            if (!active) return;
            frame++;

            Move();
        }

        /// <summary>
        /// 継承先でのUpdate()記述用関数.
        /// </summary>
        protected virtual void Move(){}

        /// <summary>
        /// 演出付きでウィンドウを出現させる(ウィンドウが出現しきったかどうかを返す)
        /// </summary>
        public bool PopUpWindow()
        {
            if (visible) return true;
            return true;
        }

        /// <summary>
        /// 演出付きでウィンドウを消滅させる(消滅しきったかどうかを返す)
        /// </summary>
        public bool VanishWindow()
        {
            if (!visible) return true;
            return true;
        }

        public void Destroy()
        {
            Dead = true;
        }

        public void Draw(Drawing d)
        {
            DrawSkin(d);
            DrawFrame(d);
            DrawContent(d);
        }

        private void DrawSkin(Drawing d)
        {
            d.DrawUI(pos + new Vector(size.X, size.Y) / 2, frameTexture,
                new Rectangle(FrameSize, FrameSize, frameTexture.Width - 2 * FrameSize, frameTexture.Height - 2 * FrameSize),
                DepthID.Frame, new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize)));
        }

        private void DrawFrame(Drawing d)
        {
            // 左上のフレーム角
            d.DrawUI(pos + new Vector(FrameSize / 2, FrameSize / 2), frameTexture, new Rectangle(0, 0, FrameSize, FrameSize), DepthID.Frame, Vector.One);
            // 右上のフレーム角
            d.DrawUI(pos + new Vector(FrameSize / 2 + size.X - FrameSize, FrameSize / 2), frameTexture, new Rectangle(frameTexture.Width - FrameSize, 0, FrameSize, FrameSize), DepthID.Frame, Vector.One);
            // 左上のフレーム角
            d.DrawUI(pos + new Vector(FrameSize / 2, size.Y - FrameSize / 2), frameTexture, new Rectangle(0, frameTexture.Height - FrameSize, FrameSize, FrameSize), DepthID.Frame, Vector.One);
            // 右下のフレーム角
            d.DrawUI(pos + new Vector(size.X - FrameSize / 2, size.Y - FrameSize / 2), frameTexture, new Rectangle(frameTexture.Width - FrameSize, frameTexture.Height - FrameSize, FrameSize, FrameSize), DepthID.Frame, Vector.One);

            // 上部
            d.DrawUI(pos + new Vector(size.X / 2, FrameSize / 2), frameTexture, new Rectangle(FrameSize, 0, frameTexture.Width - 2 * FrameSize, FrameSize), DepthID.Frame, new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), 1));
            // 左
            d.DrawUI(pos + new Vector(FrameSize / 2, size.Y / 2), frameTexture, new Rectangle(0, FrameSize, FrameSize, frameTexture.Height - 2 * FrameSize), DepthID.Frame, new Vector(1, (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize)));
            // 右
            d.DrawUI(pos + new Vector(-FrameSize / 2 + size.X, size.Y / 2), frameTexture, new Rectangle(frameTexture.Width - FrameSize, FrameSize, FrameSize, frameTexture.Height - 2 * FrameSize), DepthID.Frame, new Vector(1, (size.Y - 2 * FrameSize) / (frameTexture.Height - 2 * FrameSize)));
            // 下
            d.DrawUI(pos + new Vector(size.X / 2, size.Y - FrameSize / 2), frameTexture, new Rectangle(FrameSize, frameTexture.Height - FrameSize, frameTexture.Width - 2 * FrameSize, FrameSize), DepthID.Frame, new Vector((size.X - 2 * FrameSize) / (frameTexture.Width - 2 * FrameSize), 1));
        }

        virtual public void DrawContent(Drawing d)
        {

        }
    }
}
