using Soleil.Images;
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
        // Font設定
        readonly FontID ValFont = FontID.KkMini;
        readonly FontID CurrencyFont = FontID.KkGoldMini;
        // 場所設定
        readonly Vector InitPos;
        readonly Vector PositionDiff = new Vector(-30, 0);
        // 通貨単位位置設定
        readonly int CurrencyPosY;
        const int CurrencyPosX = 120;
        readonly Vector CurrencyPos;
        readonly Vector CurrencyOffset = new Vector(0, -1);

        MoneyWallet moneyWallet;
        int money;
        FontImage moneyText;
        FontImage currency;

        // 背景表示
        public bool EnableBack = true;
        BackBarImage backBar;

        public MoneyComponent(Vector _pos)
        {
            InitPos = _pos;
            CurrencyPosY = (int)Resources.GetFont(ValFont).MeasureString("0").Y - (int)(Resources.GetFont(CurrencyFont).MeasureString(MoneyWallet.Currency).Y);
            CurrencyPos = new Vector(CurrencyPosX, CurrencyPosY) + CurrencyOffset;
            moneyText = new FontImage(ValFont, _pos - PositionDiff, DepthID.Message, true, 0);
            currency = new FontImage(CurrencyFont, _pos + CurrencyPos-PositionDiff, DepthID.Message, true, 0);
            currency.Text = MoneyWallet.Currency;
            moneyWallet = PlayerBaggage.GetInstance().MoneyWallet;
            backBar = new BackBarImage(_pos - new Vector(BackBarImage.EdgeSize,0), (int)CurrencyPos.X + 120, false);
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
            backBar.Call();
        }

        public void Quit()
        {
            moneyText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            currency.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            moneyText.MoveTo(InitPos - PositionDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
            currency.MoveTo(InitPos + CurrencyPos - PositionDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
            backBar.Quit();
        }

        public void Update()
        {
            moneyText.Update();
            currency.Update();
            backBar.Update();
            if (money != moneyWallet.Val) Refresh();
        }

        public void Draw(Drawing d)
        {
            moneyText.Draw(d);
            currency.Draw(d);
            if (EnableBack) backBar.Draw(d);
        }
    }
}
