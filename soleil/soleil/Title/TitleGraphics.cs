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
        Image back, logo;
        TitleOrnamentBar bar1, bar2;
        IComponent[] components;

        public TitleGraphics()
        {
            back = new Image(TextureID.TitleBack, Vector.Zero, DepthID.BackGround);
            logo = new Image(TextureID.TitleLogo, LogoPos, DepthID.MenuBack);
            bar1 = new TitleOrnamentBar((new Vector(35, 18), new Vector(41, 52)));
            bar2 = new TitleOrnamentBar((new Vector(500, 536.5), new Vector(603.5, 518.2)));
            components = new IComponent[] { back, logo, bar1, bar2 };
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

        public void CallBackImage() => back.Call();

        public void Update() => components.ForEach2(s => s.Update());
        public void Draw(Drawing d) => components.ForEach2(s => s.Draw(d));
    }
}
