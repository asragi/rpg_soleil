using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class MenuComponent : IComponent
    {
        private IComponent[] components;
        protected IComponent[] Components
        {
            set
            {
                if (components == null) components = value;
                else components = components.Concat(value).ToArray();
            }
        }

        bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                if (isActive) OnEnable();
                else OnDisable();
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
            if (components != null)
            {
                foreach (var item in components)
                {
                    item.Quit();
                }
            }
        }

        public virtual void Call() {
            if (components != null)
            {
                foreach (var item in components)
                {
                    item.Call();
                }
            }
        }

        public virtual void Update()
        {
            if(components != null)
            {
                foreach (var item in components)
                {
                    item.Update();
                }
            }
        }
        public virtual void Draw(Drawing d) {
            if (components != null)
            {
                foreach (var item in components)
                {
                    item.Draw(d);
                }
            }
        }
    }
}
