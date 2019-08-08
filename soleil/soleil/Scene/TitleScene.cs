using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TitleScene: Scene
    {
        private static readonly Vector WindowPos = new Vector(128, 186);
        private static readonly string[] Commands = new[]
        {
            "Load Game", "New Game", "Options", "Exit"
        };

        SelectableWindow selectWindow;
        Image background;

        public TitleScene(SceneManager sm)
            : base(sm)
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
            selectWindow = new SelectableWindow(WindowPos, true, Commands);
            selectWindow.Call();

            // Graphics
            background = new Image(TextureID.WhiteWindow, Vector.Zero, DepthID.BackGround, alpha: 1) { Color = ColorPalette.GlayBlue };
        }

        public override void Update()
        {
            base.Update();
            background.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            background.Draw(sb);
        }
    }
}
