using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class MenuComponent
    {
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

        public virtual void Update() { }
        public virtual void Draw(Drawing d) { }
    }
}
