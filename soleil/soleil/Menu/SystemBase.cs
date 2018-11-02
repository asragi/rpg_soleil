﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// UIシステムの抽象クラス．システムの持つコンポーネントをまとめて管理する．
    /// </summary>
    abstract class SystemBase : MenuComponent
    {
        protected MenuComponent[] Components;
        protected Image[] Images;

        public SystemBase()
            : base() { }

        public override void Call()
        {
            base.Call();
            foreach (var item in Components)
            {
                item.Call();
            }
            foreach (var item in Images)
            {
                item.Update();
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
                item.Update();
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
