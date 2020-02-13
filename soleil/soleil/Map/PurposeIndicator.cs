using Soleil.Menu;
using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// Map上で次の行動を表示するディスプレイ
    /// </summary>
    class PurposeIndicator: MenuComponent, IListener
    {
        private static readonly Vector Position = new Vector(950, 150);
        private static readonly Vector TextDiff = new Vector(15, 0);

        readonly RightAlignText text;

        public PurposeIndicator()
        {
            text = new RightAlignText(FontID.CorpMini, Position, TextDiff, DepthID.Message);
            text.ActivateOutline(1);
            AddComponents(text);

            var purposeHolder = PurposeHolder.Instance;
            purposeHolder.SetListener(this);
            SetPurpose(purposeHolder.PurposeName);
        }

        public ListenerType Type => ListenerType.PurposeIndicator;
        private string Text { get => text.Text; set => text.Text = value; }

        public void OnListen(INotifier i)
        {
            if (i is PurposeHolder purposeHolder)
            {
                SetPurpose(purposeHolder.PurposeName);
                return;
            }
        }

        private void SetPurpose(PurposeName name)
        {
            if (name == PurposeName.Clear)
            {
                int beforeTextNum = Text.Length;
                Text = string.Empty;
                if (beforeTextNum != 0) Quit();
                return;
            }
            Text = name switch
            {
                PurposeName.MeetPrincipal => "校長先生に挨拶しよう",
                _ => throw new NotImplementedException("Invalid PurposeName!")
            };
            Call();
            return;
        }
    }
}
