using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// UIシステムの抽象クラス．システムの持つコンポーネントをまとめて管理する．
    /// </summary>
    abstract class SystemBase : MenuChild
    {
        protected MenuComponent[] Components;
        protected Image[] Images;

        public SystemBase(MenuComponent parent)
            : base(parent) { }

        public override void Call()
        {
            base.Call();
            foreach (var item in Components)
            {
                item.Call();
            }
            foreach (var item in Images)
            {
                item.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            }
        }

        public override void Quit()
        {
            base.Quit();
            foreach (var item in Components)
            {
                item.Quit();
            }
            foreach (var item in Images)
            {
                item.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (var item in Components)
            {
                item.Update();
            }
            foreach (var item in Images)
            {
                item.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            foreach (var item in Components)
            {
                item.Draw(d);
            }
            foreach (var item in Images)
            {
                item.Draw(d);
            }
        }
    }
}
