using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class UIImage : Image
    {
        Vector posDiff;
        public UIImage(TextureID tex, Vector pos,Vector _posDiff, DepthID dep, bool centerOrigin = false, bool isStatic = true, float alpha = 0)
            :base(0,Resources.GetTexture(tex),pos,dep,centerOrigin,isStatic,alpha)
        {
            posDiff = _posDiff;
        }

        public void Call()
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            MoveTo(Pos, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }

        public void Quit()
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            MoveTo(Pos + posDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }
    }
}
