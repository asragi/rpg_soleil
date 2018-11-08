﻿using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Images
{
    abstract class UIImageBase : ImageBase, IComponent
    {
        readonly Vector initPos;
        readonly Vector posDiff;
        public UIImageBase(Vector pos, Vector? _posDiff, DepthID dep, bool centerOrigin = false, bool isStatic = true, float alpha = 0)
            : base(pos + (_posDiff ?? Vector.Zero), dep, centerOrigin, isStatic, alpha)
        {
            (initPos, posDiff) = (pos, (_posDiff ?? Vector.Zero));
        }

        public void Call() => Call(true);
        public void Quit() => Quit(true);

        public void Call(bool move = true)
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            if (move) MoveToDefault();
        }

        public void MoveToDefault()
        {
            MoveTo(initPos, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }

        public void Quit(bool move = true)
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            if (move) MoveToBack();
        }

        public void MoveToBack()
        {
            MoveTo(initPos + posDiff, MenuSystem.FadeSpeed, MenuSystem.EaseFunc);
        }
    }
}
