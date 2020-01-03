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
            CameraPoints = new[] { new CameraPoint(480, 270) };
            MapCameraManager.SetMapSize(960, 540);
            MapCameraManager.SetCameraPoint(CameraPoints);

            EventSequences = new EventSequence[1];
            for (int i = 0; i < EventSequences.Length; i++)
            {
                EventSequences[i] = new EventSequence(ObjectManager.GetPlayer());
            }
            EventSequences[0].SetEventSet(
                    new MessageWindowEvent(new Vector(480, 270), 0, "魔術学校マギストル......", speech: false, center: true, false),
                    new MessageWindowEvent(new Vector(480, 270), 0, "卒業式を終えた春休み\nマギストルを卒業したルーネとサニーは\n卒業旅行に出かける......", false, true, false),
                    new MessageWindowEvent(new Vector(480, 270), 0, "卒業旅行を通して\n新たな「術」を覚えるために......", false, true, false),
                    new WaitEvent(60),
                    new MessageWindowEvent(new Vector(480, 270), 0, "そしてまだ見ぬ両親に会うために.", false, true, false),
                    new WaitEvent(60),
                    new FadeOutEvent(),
                    new WaitEvent(60),
                    new ChangeMapEvent(MapName.MagistolRoom, new Vector(400, 400), Direction.D, p, cam),
                    new FadeInEvent(),
                    new WaitEvent(50),
                    new MessageWindowEvent(new Vector(480, 270), 0, "現在はプロトタイプ版です！\nマップをじっくり眺めてみたり\n散策をお楽しみください！", false, true),
                    new ChangeInputFocusEvent(InputFocus.Player)
                );
        }

        public override MusicID MapMusic => MusicID.MagicCity;

        protected override void Start()
        {
            base.Start();
            EventSequences[0].StartEvent();
        }
    }
}
