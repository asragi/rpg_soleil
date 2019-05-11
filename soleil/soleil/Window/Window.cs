using Soleil.Menu;

namespace Soleil
{
    /// <summary>
    /// Windowの基本クラス
    /// </summary>
    abstract class Window : MenuComponent
    {
        public const int FadeSpeed = 8;
        /// <summary>
        /// アニメーション時のズレ量
        /// </summary>
        public readonly Vector DiffPos = new Vector(0, 10);
        protected const DepthID Depth = DepthID.MessageBack;
        protected abstract float Alpha { get; }
        protected abstract Vector SpaceVector { get; }
        /// <summary>
        /// pos : 左上基準
        /// </summary>
        protected Vector Pos;
        protected Vector ContentPos => Pos + SpaceVector;
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public bool Dead { get; protected set; }
        /// <summary>
        /// ウィンドウ識別用変数. 重複可能.
        /// </summary>
        public WindowTag Tag { get; private set; }
        protected int Frame;
        bool quitStart = false;
        public Window(Vector _pos, WindowTag _tag, WindowManager wm)
        {
            Pos = _pos;
            Tag = _tag;
            Visible = true;
            Active = true;
            wm.Add(this);
        }

        /// <summary>
        /// 継承後の振る舞いはMove()で記述する.
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (quitStart & Alpha < 0.1) Destroy();
            // visibleなのにactiveという状態を回避したい
            Active = Visible ? Active : false;
            if (!Active) return;
            Frame++;

            Move();
        }

        /// <summary>
        /// 継承先でのUpdate()記述用関数.
        /// </summary>
        protected virtual void Move(){}

        public override void Quit()
        {
            base.Quit();
            quitStart = true;
        }

        public void Destroy() => Dead = true;

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            DrawContent(d);
        }

        protected virtual void DrawContent(Drawing d) { }
    }
}
