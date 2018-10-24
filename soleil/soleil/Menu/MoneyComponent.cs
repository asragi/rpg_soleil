using Soleil.Map;
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
        readonly Vector CurrencyPos = new Vector(200, 0);
        MoneyWallet moneyWallet;
        FontImage moneyText;
        FontImage currency;

        public MoneyComponent(Vector _pos)
        {
            moneyText = new FontImage(FontID.Test, _pos, DepthID.Message, true, 0);
            moneyText.Color = ColorPalette.DarkBlue;
            moneyText.EnableShadow = true;
            moneyText.ShadowPos = new Vector(3, 3);
            moneyText.ShadowColor = ColorPalette.GlayBlue;
            currency = new FontImage(FontID.Test, _pos + CurrencyPos, DepthID.Message, true, 0);
            currency.Color = ColorPalette.DarkBlue;
            currency.EnableShadow = true;
            currency.ShadowPos = new Vector(3, 3);
            currency.ShadowColor = ColorPalette.GlayBlue;
            moneyWallet = PlayerBaggage.GetInstance().MoneyWallet;
        }

        public void Refresh()
        {
            moneyText.Text = moneyWallet.Val.ToString();
        }

        public void Call()
        {

        }

        public void Quit()
        {

        }

        public void Update()
        {
            moneyText.Update();
            currency.Update();
        }

        public void Draw(Drawing d)
        {
            moneyText.Draw(d);
            currency.Draw(d);
        }
    }
}
