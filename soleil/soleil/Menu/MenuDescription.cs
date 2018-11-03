using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    class MenuDescription
    {
        protected FontImage fontImage;
        public String Text { set { fontImage.Text = value; } }

        public MenuDescription(Vector _pos)
        {
            fontImage = new FontImage(FontID.KkBlack, _pos, DepthID.Message, true, 0);
        }

        public virtual void Call()
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
        }

        public virtual void Quit()
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
        }

        public void Fade(int duration, EFunc _easeFunc, bool isFadeIn)
        {
            fontImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public void MoveTo(Vector target, int duration, EFunc _easeFunc)
        {
            fontImage.MoveTo(target, duration, _easeFunc);
        }

        public virtual void Update()
        {
            fontImage.Update();
        }

        public virtual void Draw(Drawing d)
        {
            fontImage.Draw(d);
        }
    }
}
