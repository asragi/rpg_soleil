using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// キャラクターを指定して立ち絵を削除する．
    /// </summary>
    class DestroyPictureEvent : EventBase
    {
        CharaName name;
        CharacterPictureHolder holder;

        public DestroyPictureEvent(CharaName _name, CharacterPictureHolder _holder)
        {
            (name, holder) = (_name, _holder);
        }

        public override void Start()
        {
            base.Start();
            holder.QuitCharacter(name);
        }

        public override void Execute()
        {
            base.Execute();
            Next();
        }
    }
}
