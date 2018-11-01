using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// 所持金表示コンポーネント．
    /// </summary>
    class MoneyComponent
    {
        readonly Vector InitPos;
        readonly Vector PositionDiff = new Vector(-30, 0);
        readonly Vector CurrencyPos = new Vector(200, 0);
        MoneyWallet moneyWallet;
        int money;
        FontImage moneyText;
        FontImage currency;

        public MoneyComponent(Vector _pos)
        {
            InitPos = _pos;
            moneyText = new FontImage(FontID.WhiteOutlineGrad, _pos - PositionDiff, DepthID.Message, true, 0);
            currency = new FontImage(FontID.WhiteOutlineGrad, _pos + CurrencyPos-PositionDiff, DepthID.Message, true, 0);
            currency.Text = MoneyWallet.Currency;
            moneyWallet = PlayerBaggage.GetInstance().MoneyWallet;
            Refresh();
        }

        public void Refresh()
        {
            money = moneyWallet.Val;
            moneyText.Text = money.ToString();
        }

        public void Call()
        {
            moneyText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            currency.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            moneyText.MoveTo(InitPos, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
            currency.MoveTo(InitPos + CurrencyPos, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }

        public void Quit()
        {
            moneyText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            currency.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            moneyText.MoveTo(InitPos - PositionDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
            currency.MoveTo(InitPos + CurrencyPos - PositionDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }

        public void Update()
        {
            moneyText.Update();
            currency.Update();
            if (money != moneyWallet.Val) Refresh();
        }

        public void Draw(Drawing d)
        {
            moneyText.Draw(d);
            currency.Draw(d);
        }
    }
}
