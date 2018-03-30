using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestObject :MapObject
    {
        CollideBox exi;
        MapEventManager eventManager;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
            eventManager = MapEventManager.GetInstance();
        }

        public override void OnCollisionEnter()
        {
            EventA();
            base.OnCollisionEnter();
        }

        private void EventA()
        {
            eventManager.CreateSelectWindow(pos, new Vector(120, 145), 0, "はい", "いいえ", "わからん");
        }

        private void EventAUpdate()
        {
            switch (eventManager.ReturnSelectIndex(0))
            {
                case 0:
                    Console.WriteLine("hai");
                    eventManager.DestroyWindow(0);
                    eventManager.SetFocusPlayer();
                    break;
                case 1:
                    Console.WriteLine("iie");
                    eventManager.DestroyWindow(0);
                    eventManager.SetFocusPlayer();
                    break;
                case 2:
                    Console.WriteLine("wakaran");
                    eventManager.DestroyWindow(0);
                    eventManager.SetFocusPlayer();
                    break;
                default:
                    Console.WriteLine("test");
                    break;
            }
        }

        public override void Update()
        {
            EventAUpdate();
            base.Update();
        }


        public override void OnCollisionExit()
        {
            eventManager.DestroyWindow(0);
            base.OnCollisionExit();
        }
    }
}
