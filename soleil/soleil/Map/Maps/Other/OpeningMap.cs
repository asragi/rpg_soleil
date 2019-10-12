using Soleil.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class OpeningMap: MapBase
    {
        public OpeningMap(PersonParty p, Camera cam)
            : base(MapName.Opening, p, cam)
        {
            MapCameraManager.SetMapSize(960, 540);

            CameraPoints = new[] { new CameraPoint(480, 270) };

            EventSequences = new EventSequence[1];
            for (int i = 0; i < EventSequences.Length; i++)
            {
                EventSequences[i] = new EventSequence(ObjectManager.GetPlayer());
            }
            EventSequences[0].SetEventSet(
                    new MessageWindowEvent(Vector.Zero, 0, "test")
                );
        }

        protected override void Start()
        {
            base.Start();
            EventSequences[0].StartEvent();
        }
    }
}
