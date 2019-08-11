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
    class MoneyComponent : MenuComponent
    {
        // Font設定
        readonly FontID ValFont = FontID.CorpM;
        // 場所設定
        public static readonly Vector MoneyPos = new Vector(150, 0);
        // 通貨単位位置設定
        public readonly static Vector CurrencyPos = new Vector(0, 5);

        MoneyWallet moneyWallet;
        int money;
        TextImage moneyText;
        Image currency;

        // 背景表示
        BackBarImage backBar;

        public MoneyComponent(Vector _pos, Vector posDiff)
        {
            moneyText = new RightAlignText(ValFont, _pos + MoneyPos, posDiff, DepthID.MenuBottom);
            currency = new Image(TextureID.Currency, _pos + CurrencyPos, posDiff, DepthID.MenuBottom);
            moneyWallet = PlayerBaggage.GetInstance().MoneyWallet;
            backBar = new BackBarImage(_pos + new Vector(-BackBarImage.EdgeSize, 0), posDiff, 220, false);
            AddComponents(new IComponent[] { backBar, moneyText, currency });
            Refresh();
        }

        public void Refresh()
        {
            money = moneyWallet.Val;
            moneyText.Text = money.ToString();
        }

        public override void Update()
        {
            base.Update();
            if (money != moneyWallet.Val) Refresh();
        }
    }
}
