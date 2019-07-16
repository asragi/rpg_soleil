using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// イージングを伴い出現・消滅するコンポーネント．
    /// </summary>
    abstract class EasingComponent : IComponent
    {
        private IComponent[] components;

        protected void AddComponents(IComponent[] comps)
        {
            if (components == null) components = comps;
            else components = components.Concat(comps).ToArray();
        }

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

        public EasingComponent() { }
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
            // Active処理
            if (isActive != nextActive)
            {
                if (nextActive) OnEnable();
                else OnDisable();
            }
            isActive = nextActive;


            if (components != null)
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
