﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class StatusTargetSelectBase : MenuChild
    {
        protected StatusMenu StatusMenu;

        public StatusTargetSelectBase(MenuChild parent)
            : base(parent)
        {

        }

        public override void Call()
        {
            base.Call();
            StatusMenu.CallCursor();
        }

        public void SetRefs(StatusMenu sm) => StatusMenu = sm;

        public override void OnInputRight() => StatusMenu.OnInputRight();

        public override void OnInputLeft() => StatusMenu.OnInputLeft();

        public override void OnInputCancel()
        {
            StatusMenu.QuitCursor();
            ReturnParent();
        }
    }
}
