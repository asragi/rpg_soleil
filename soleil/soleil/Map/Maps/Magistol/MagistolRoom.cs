using Soleil.Event;
using Soleil.Map.Maps.Magistol;

namespace Soleil.Map
{
    /// <summary>
    /// 開始位置．ルーネの部屋
    /// </summary>
    class MagistolRoom : MapBase
    {
        public override MusicID MapMusic => MusicID.MagicCity;
        public MagistolRoom(PersonParty party, Camera cam)
           : base(MapName.MagistolRoom, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol1_back, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(314, 72), TextureID.Magistol1_wall, 362, om),
                new MapConstruct(new Vector(175, 131), TextureID.Magistol1_dark, MapDepth.Top, om),
                new MapConstruct(new Vector(495, 408), TextureID.Magistol1_cloth, MapDepth.Top, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            new MapChangeObject((new Vector(280, 264), new Vector(376, 308)), MapName.MagistolCol1, new Vector(1232, 1222), Direction.U, om, bm, party, cam);
            new SunnyInLuneRoom(new Vector(400, 400), om, bm);
            new Shelf((new Vector(572, 389), new Vector(650, 414)), om, bm);
        }

        class Shelf: MapObject
        {
            EventSequence eventSequence;
            public Shelf((Vector, Vector) pos, ObjectManager om, BoxManager bm)
                :base(om)
            {
                var player = om.GetPlayer();
                new CollideLine(this, pos, CollideLayer.Character, bm);
                eventSequence = new EventSequence(player);

                eventSequence.SetEventSet(
                    new MessageWindowEvent(player, "古びた本が並んでいる..."),
                    new MessageWindowEvent(player, "術に関する本のようだ"),
                    new SelectWindowEvent(player, "陽術について", "陰術について", "魔術について", "やめる"),
                    new NumEventBranch(eventSequence, () => WindowManager.GetInstance().GetDecideIndex(),
                        new EventUnit[] {
                            new MessageWindowEvent(player,
                            "【陽術】\n" +
                            "フレアで発展した術カテゴリのひとつ．\n" +
                            "熱や光の操作を主に対象としている．"),
                            new MessageWindowEvent(player,
                            "熱による攻撃や光のエネルギーによる強化・回復など\n" +
                            "単純で取り扱いやすい術が多い特徴がある．\n" +
                            "相反の関係にある【陰術】と同時に習得することはできない．"),
                        },
                        new EventUnit[] {
                            new MessageWindowEvent(player,
                            "【陰術】\n" +
                            "ソムニアで発展した術カテゴリのひとつ．\n" +
                            "熱や光の操作を主に対象としている．"),
                            new MessageWindowEvent(player,
                            "生体の筋力を弛緩させる術や動作を停止させる術など\n" +
                            "外部の脅威を取り除く術が多い特徴がある．\n" +
                            "相反の関係にある【陽術】と同時に習得することはできない．"),
                        },
                        new EventUnit[] {
                            new MessageWindowEvent(player,
                            "【魔術】\n" +
                            "マギストルで研究開発された術カテゴリのひとつ．\n" +
                            "電子や魔素の操作を主に対象としている．"),
                            new MessageWindowEvent(player,
                            "攻撃・強化・回復など一通りの術が揃い，\n" +
                            "魔術のみで多くの場面に対応できるようになっている．\n" +
                            "マギストルでの修学を通して誰もが習得することができる．"),
                        },
                        new EventUnit[] { }
                    ),
                    new ChangeInputFocusEvent(InputFocus.Player)
                );
            }

            public override void Update()
            {
                base.Update();
                eventSequence.Update();
            }

            public override void Draw(Drawing d)
            {
                base.Draw(d);
                eventSequence.Draw(d);
            }
            public override void OnCollisionEnter(CollideObject collide)
            {
                base.OnCollisionEnter(collide);
                if (collide.Layer != CollideLayer.PlayerHit) return;
                eventSequence.StartEvent();
            }
        }
    }
}
