using Soleil.Event;
using Soleil.Map.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.Maps.Magistol
{
    class SunnyInLuneRoom : SunnyObj
    {
        public SunnyInLuneRoom(Vector pos, PersonParty party, ObjectManager om, BoxManager bm)
            : base (pos, om, bm)
        {
            EventSequence.SetEventSet(
                new MessageWindowEvent(this, "財布はルーネに任せるから"),
                new MessageWindowEvent(this, "とりあえず校長先生に挨拶してから行こう"),
                new CharacterActivateEvent(party, Misc.CharaName.Sunny, true),
                new MoveToPlayerEvent(this, om.GetPlayer(), _frame: 40),
                new CharacterDisappearEvent(this),
                new ChangeInputFocusEvent(InputFocus.Player)
                );
        }

        public override void OnCollisionEnter(CollideObject collide)
        {
            base.OnCollisionEnter(collide);
            if (collide.Layer != CollideLayer.PlayerHit) return;
            FaceToPlayer();
            EventSequence.StartEvent();
        }
    }
}
