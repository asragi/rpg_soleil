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
        Image logo;
        IComponent[] components;

        public TitleGraphics()
        {
            logo = new Image(TextureID.BackBar, LogoPos, DepthID.MenuBack);
            components = new[] { logo };
        }

        public void CallLogo()
        {
            logo.Call();
        }

        public void Update() => components.ForEach2(s => s.Update());
        public void Draw(Drawing d) => components.ForEach2(s => s.Draw(d));
    }
}
