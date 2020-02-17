using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Title
{
    class TitleGraphics
    {
        private static readonly Vector LogoPos = new Vector(140, 70);
        private static readonly Vector ShadowPos = new Vector(416, 295);
        private static readonly Vector CityPos = new Vector(256, 44);
        private static readonly Vector EyeCatchPos = new Vector(388, -39);
        Image back, logo;
        Image eyecatch, shadow, citySilhouette;
        TitleOrnamentBar bar1, bar2;
        IComponent[] components;

        public TitleGraphics()
        {
            back = new Image(TextureID.TitleBack, Vector.Zero, DepthID.BackGround);
            logo = new Image(TextureID.TitleLogo, LogoPos, new Vector(0, -20), DepthID.MenuBack);
            logo.FadeSpeed = 80;
            bar1 = new TitleOrnamentBar((new Vector(35, 18), new Vector(41, 52)));
            bar2 = new TitleOrnamentBar((new Vector(500, 536.5), new Vector(603.5, 518.2)));
            DepthID eyeCatchDepth = DepthID.Player;
            eyecatch = new Image(TextureID.TitleCharacter, EyeCatchPos, eyeCatchDepth);
            shadow = new Image(TextureID.TitleKage, ShadowPos, eyeCatchDepth);
            citySilhouette = new Image(TextureID.TitleSilhouette, CityPos, eyeCatchDepth);
            components = new IComponent[] { back, logo, citySilhouette, shadow, eyecatch, bar1, bar2 };
        }

        public void CallLogo()
        {
            logo.Call();
        }

        public void CallBar()
        {
            bar1.Call();
            bar2.Call();
        }

        public void CallCharacter()
        {
            eyecatch.Call();
            shadow.Call();
        }

        public void CallCitySilhouette() => citySilhouette.Call();

        public void CallBackImage() => back.Call();

        public void Update() => components.ForEach2(s => s.Update());
        public void Draw(Drawing d) => components.ForEach2(s => s.Draw(d));
    }
}
