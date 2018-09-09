using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : MenuChild
    {
        readonly Vector WindowPos = new Vector(300, 0);
        readonly Vector WindowStartPos = new Vector(800, 0);
        Image backImage;
        FontImage test;

        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            test = new FontImage(FontID.Test, new Vector(500, 100), DepthID.Message, true, 0);
            test.Text = "フェニックスの手羽先A";
            test.Color = ColorPalette.DarkBlue;
            test.EnableShadow = true;
            test.ShadowColor = ColorPalette.GlayBlue;
            test.ShadowPos = new Vector(2, 2);
            backImage = new Image(0, Resources.GetTexture(TextureID.WhiteWindow), WindowStartPos, DepthID.MessageBack, false, true, 0);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // Transition Images
            backImage.MoveTo(WindowStartPos, 35, Easing.OutCubic);
            backImage.Fade(35, Easing.OutCubic, false);
            test.MoveTo(WindowStartPos + new Vector(100, 80), 35, Easing.OutCubic);
            test.Fade(35, Easing.OutCubic, false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Transition Images
            backImage.MoveTo(WindowPos, 35, Easing.OutCubic);
            backImage.Fade(35, Easing.OutCubic, true);
            test.MoveTo(WindowPos + new Vector(100, 80), 35, Easing.OutCubic);
            test.Fade(35, Easing.OutCubic, true);
        }

        public override void Update()
        {
            base.Update();
            backImage.Update();
            test.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            backImage.Draw(d);
            test.Draw(d);
        }

        public override void OnInputRight() { }
        public override void OnInputLeft() { }
        public override void OnInputUp() { Console.WriteLine("Up"); }
        public override void OnInputDown() { Console.WriteLine("Down"); }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
