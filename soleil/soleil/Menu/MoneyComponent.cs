using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    /// <summary>
    /// 所持金表示コンポーネント．
    /// </summary>
    class MoneyComponent
    {
        FontImage text;

        public MoneyComponent(Vector _pos)
        {
            text = new FontImage(FontID.Test, _pos, DepthID.Message, true, 0);
            text.Color = ColorPalette.DarkBlue;
            text.EnableShadow = true;
            text.ShadowPos = new Vector(3, 3);
            text.ShadowColor = ColorPalette.GlayBlue;
        }

        public void Call()
        {

        }

        public void Quit()
        {

        }

        public void Update()
        {

        }

        public void Draw(Drawing d)
        {

        }
    }
}
