using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event.Shop;
using Soleil.Item;

namespace Soleil.Event
{
    class ShopEvent : EventBase
    {
        ShopSystem shopSystem;
        BoolSet boolSet;
        int target;

        public ShopEvent(ShopName name, PersonParty party, BoolSet _boolSet, int _target)
            : this(name, party)
        {
            (boolSet, target) = (_boolSet, _target);
        }

        public ShopEvent(ShopName name, PersonParty party)
        {
            shopSystem = new ShopSystem(name, party);
        }

        public override void Start()
        {
            base.Start();
            shopSystem.Call();
        }
        public override void Execute()
        {
            base.Execute();
            shopSystem.Input(InputSmoother(KeyInput.GetStickInclineDirection(1)));
            shopSystem.Update();
            if (shopSystem.IsQuit)
            {
                ChangeBoolSet();
                Next();
            }

            void ChangeBoolSet() // 購入したか否かを見て値を代入．
            {
                if (boolSet == null) return;
                boolSet[target] = shopSystem.Purchased;
            }
        }

        int waitFrame;
        bool inputCheck;
        int headWaitCount;
        const int InputWait = 6;
        const int HeadWait = 20;
        /// <summary>
        /// MapInputManagerからのコピペ．追々リファクタリングして統合します．
        /// </summary>
        private Direction InputSmoother(Direction dir)
        {
            waitFrame--;
            if (dir != Direction.N)
            {
                headWaitCount--;
                if (headWaitCount > 0 && inputCheck) return Direction.N;
                if (waitFrame > 0) return Direction.N;
                waitFrame = InputWait;
                inputCheck = true;
                return dir;
            }
            inputCheck = false;
            waitFrame = 0;
            headWaitCount = HeadWait;
            return Direction.N;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            shopSystem.Draw(d);
        }
    }
}
