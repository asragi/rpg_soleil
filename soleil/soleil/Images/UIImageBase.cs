using Microsoft.Xna.Framework;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Images
{
    abstract class UIImageBase : ImageBase, IComponent
    {
        public virtual Vector InitPos { get; set; }
        protected readonly Vector PosDiff;
        public Color Color { get; set; } = Color.White;
        public int FadeSpeed = MenuSystem.FadeSpeed;
        public UIImageBase(Vector pos, Vector? _posDiff, DepthID dep, bool centerOrigin = false, bool isStatic = true, float alpha = 0)
            : base(pos + (_posDiff ?? Vector.Zero), dep, centerOrigin, isStatic, alpha)
        {
            (InitPos, PosDiff) = (pos, (_posDiff ?? Vector.Zero));
        }

        public virtual void Call() => Call(true);
        public virtual void Quit() => Quit(true);

        public void Call(bool move = true)
        {
            Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            if (move) MoveToDefault();
        }

        public void MoveToDefault()
        {
            MoveTo(InitPos, FadeSpeed, MenuSystem.EaseFunc);
        }

        public void Quit(bool move = true)
        {
            Fade(FadeSpeed, MenuSystem.EaseFunc, false);
            if (move) MoveToBack();
        }

        public void MoveToBack()
        {
            MoveTo(InitPos + PosDiff, FadeSpeed, MenuSystem.EaseFunc);
        }
    }
}
