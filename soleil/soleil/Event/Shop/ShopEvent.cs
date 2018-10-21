using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event.Shop;

namespace Soleil.Event
{
    class ShopEvent : EventBase
    {
        ShopSystem shopSystem;
        public ShopEvent()
        {
            shopSystem = new ShopSystem();
        }

        public override void Start()
        {
            base.Start();
            shopSystem.Call();
        }
        public override void Execute()
        {
            base.Execute();
            shopSystem.Update();
            if (shopSystem.IsQuit) Next();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            shopSystem.Draw(d);
        }
    }
}
