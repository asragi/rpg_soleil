﻿using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニュー内に存在する入力を受け取る子要素の抽象クラス
    /// </summary>
    abstract class MenuChild : EasingComponent
    {
        EasingComponent parent;
        public MenuChild(EasingComponent _parent)
        {
            parent = _parent;
        }

        /// <summary>
        /// ウィンドウを閉じ親にActiveを戻す
        /// </summary>
        protected virtual void ReturnParent()
        {
            parent.IsActive = true;
            IsActive = false;
        }

        public override void Call()
        {
            Call(true);
        }

        public void Call(bool active)
        {
            base.Call();
            IsActive = active;
        }

        public virtual void Input(Direction dir)
        {
            if (KeyInput.GetKeyPush(Key.A)) { OnInputSubmit(); return; }
            if (KeyInput.GetKeyPush(Key.B)) { OnInputCancel(); return; }
            if (dir == Direction.U) OnInputUp();
            if (dir == Direction.D) OnInputDown();
            if (dir == Direction.R) OnInputRight();
            if (dir == Direction.L) OnInputLeft();
        }

        public virtual void OnInputRight() { }
        public virtual void OnInputLeft() { }
        public virtual void OnInputUp() { }
        public virtual void OnInputDown() { }
        public virtual void OnInputSubmit() { }
        public virtual void OnInputCancel() { }

    }
}
