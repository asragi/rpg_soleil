using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    public enum PurposeName
    {
        Clear = -1,
        MeetPrincipal,
    }

    class PurposeHolder: INotifier // Singleton
    {
        private IListener[] listeners;

        private PurposeHolder() { Reset(); }
        public static PurposeHolder Instance { get; } = new PurposeHolder();
        public PurposeName PurposeName { get; private set; }

        public void Reset()
        {
            PurposeName = PurposeName.Clear;
            listeners = new IListener[(int)ListenerType.size];
        }

        public void SetListener(IListener l) => listeners[(int)l.Type] = l;

        public void SetPurpose(PurposeName p)
        {
            PurposeName = p;
            listeners.ForEach2(l => l?.OnListen(this));
        }
    }
}
