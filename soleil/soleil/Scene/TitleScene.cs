using Microsoft.Xna.Framework;
using Soleil.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TitleScene: Scene
    {
        TitleMaster master;
        Image background;

        public TitleScene(SceneManager sm)
            : base(sm)
        {
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);

            master = new TitleMaster();

            // Graphics
            background = new Image(TextureID.WhiteWindow, Vector.Zero, DepthID.BackGround, alpha: 1) { Color = ColorPalette.GlayBlue };
        }

        public override void Update()
        {
            base.Update();
            background.Update();
            master.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            background.Draw(sb);
            master.Draw(sb);
        }
    }
}
