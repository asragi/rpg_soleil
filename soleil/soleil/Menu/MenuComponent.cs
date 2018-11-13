using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class MenuComponent : IComponent
    {
        protected IComponent[] Components;
        bool isActive;
        bool nextActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                nextActive = value;
            }
        }

        public MenuComponent() { }
        /// <summary>
        /// IsActiveがtrueになったときに行われる処理
        /// </summary>
        protected virtual void OnEnable() { }
        /// <summary>
        /// IsActiveがfalseになったときに行われる処理
        /// </summary>
        protected virtual void OnDisable() { }

        public virtual void Quit() {
            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item.Quit();
                }
            }
        }

        public virtual void Call() {
            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item.Call();
                }
            }
        }

        public virtual void Update()
        {
            // Active処理
            if (isActive != nextActive)
            {
                if (nextActive) OnEnable();
                else OnDisable();
            }
            isActive = nextActive;


            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item.Update();
                }
            }
        }
        public virtual void Draw(Drawing d) {
            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item.Draw(d);
                }
            }
        }
    }
}
