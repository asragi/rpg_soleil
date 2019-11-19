﻿using Soleil.Event;
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
            CameraPoints = new[] { new CameraPoint(480, 270) };
            MapCameraManager.SetMapSize(960, 540);
            MapCameraManager.SetCameraPoint(CameraPoints);

            EventSequences = new EventSequence[1];
            for (int i = 0; i < EventSequences.Length; i++)
            {
                EventSequences[i] = new EventSequence(ObjectManager.GetPlayer());
            }
            EventSequences[0].SetEventSet(
                    new MessageWindowEvent(new Vector(480, 270), 0, "test\ntesttest", speech: false, center: true, false),
                    new MessageWindowEvent(new Vector(480, 270), 0, "オープニングイベント用メッセージ", false, true, false),
                    new WaitEvent(60),
                    new FadeOutEvent(),
                    new WaitEvent(60),
                    new ChangeMapEvent(MapName.MagistolRoom, new Vector(400, 400), Direction.D, p, cam),
                    new FadeInEvent(),
                    new ChangeInputFocusEvent(InputFocus.Player)
                );
        }

        protected override void Start()
        {
            base.Start();
            EventSequences[0].StartEvent();
        }
    }
}