using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusMenu : MenuChild
    {
        Image faceImgLune, faceImgSun;
        FontImage HPLune, HPSun, MPLune, MPSun;
        List<FontImage> fontImages;

        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;

        public StatusMenu(MenuComponent parent)
            :base(parent)
        {
            faceImgLune = new Image(0, Resources.GetTexture(TextureID.FrameTest), new Vector(370, 150), DepthID.MessageBack, false, true, 0);
            faceImgSun = new Image(0, Resources.GetTexture(TextureID.FrameTest), new Vector(620, 150), DepthID.MessageBack, false, true, 0);

            // SetText
            fontImages = new List<FontImage>();
            HPLune = new FontImage(FontID.Test, new Vector(470, 350), DepthID.MessageBack, true, 0);
            HPLune.Text = "300";
            fontImages.Add(HPLune);
            HPSun = new FontImage(FontID.Test, new Vector(720, 350), DepthID.MessageBack, true, 0);
            HPSun.Text = "654";
            fontImages.Add(HPSun);
            MPLune = new FontImage(FontID.Test, new Vector(470, 400), DepthID.MessageBack, true, 0);
            MPLune.Text = "864";
            fontImages.Add(MPLune);
            MPSun = new FontImage(FontID.Test, new Vector(720, 400), DepthID.MessageBack, true, 0);
            MPSun.Text = "425";
            fontImages.Add(MPSun);
            foreach (var textImg in fontImages)
            {
                textImg.Color = ColorPalette.DarkBlue;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        /// <summary>
        /// メニューが立ち上がる時の処理
        /// </summary>
        public void FadeIn()
        {
            faceImgSun.Fade(30, EaseFunc, true);
            faceImgLune.Fade(30, EaseFunc, true);
            foreach (var textImg in fontImages)
            {
                textImg.Fade(30, EaseFunc, true);
            }
        }

        /// <summary>
        /// メニューが閉じるときの処理
        /// </summary>
        public void FadeOut()
        {
            faceImgSun.Fade(30, EaseFunc, false);
            faceImgLune.Fade(30, EaseFunc, false);
            foreach (var textImg in fontImages)
            {
                textImg.Fade(30, EaseFunc, false);
            }
        }

        public override void Update()
        {
            base.Update();

            // Image Update
            faceImgSun.Update();
            faceImgLune.Update();
            foreach (var textImg in fontImages)
            {
                textImg.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            // Image Draw
            faceImgSun.Draw(d);
            faceImgLune.Draw(d);
            foreach (var textImg in fontImages)
            {
                textImg.Draw(d);
            }
        }

        // Input
        public override void OnInputRight() { }
        public override void OnInputLeft() { }
        public override void OnInputUp() { }
        public override void OnInputDown(){ }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
