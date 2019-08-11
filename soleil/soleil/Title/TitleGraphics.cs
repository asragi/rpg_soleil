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
        IComponent[] components;

        public TitleGraphics()
        {
            back = new Image(TextureID.TitleBack, Vector.Zero, DepthID.BackGround);
            logo = new Image(TextureID.TitleLogo, LogoPos, DepthID.MenuBack);
            components = new[] { back, logo };
        }

        public void CallLogo()
        {
            logo.Call();
        }

        public void CallBackImage() => back.Call();

        public void Update() => components.ForEach2(s => s.Update());
        public void Draw(Drawing d) => components.ForEach2(s => s.Draw(d));
    }
}
