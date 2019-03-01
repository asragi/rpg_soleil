using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// 画面にキャラクターの立ち絵を表示する
    /// </summary>
    class PictureCreateEvent: EventBase
    {
        CharaName name;
        int position;
        CharacterPictureHolder holder;

        public PictureCreateEvent(CharaName _name, int _position, CharacterPictureHolder _holder)
        {
            (name, position, holder) = (_name, _position, _holder);
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("Do");
            holder.Create(name, position);
        }

        public override void Execute()
        {
            base.Execute();
            Next();
        }
    }
}
