using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// MessageWindowに吹き出しをつけたもの
    /// </summary>
    class SpeechBubbleWindow: MessageWindow
    {
        private const int ArrowPositionFromLeft = Event.WindowEventBase.WindowPosDiffX;
        Image arrow;

        public SpeechBubbleWindow(Vector pos, Vector size, bool isStatic = false)
            :base(pos, size, isStatic)
        {
            arrow = new Image(
                TextureID.MessageWindowArrow,
                pos + new Vector(ArrowPositionFromLeft, size.Y),
                DiffPos, Depth, isStatic: isStatic);
            AddComponents(arrow);
        }
    }
}
