using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class EarnMoneyEvent: EventBase
    {
        private readonly ToastMaster toastMaster;
        private readonly int value;

        public EarnMoneyEvent(int _value, ToastMaster tm)
        {
            value = _value;
            toastMaster = tm;
        }

        public override void Execute()
        {
            base.Execute();
            // Display Toast
            toastMaster.PopUpAlert(TextureID.IconJewel, string.Empty, value);
            // Add Money to Player Wallet
            var wallet = PlayerBaggage.GetInstance().MoneyWallet;
            wallet.Add(value);
            Next();
        }
    }
}
