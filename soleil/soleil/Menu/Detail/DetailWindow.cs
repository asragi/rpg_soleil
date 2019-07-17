using Soleil.Battle;
using Soleil.Menu.Detail;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Misc;

namespace Soleil.Menu
{
    /// <summary>
    /// 選ばれているものの詳細を表示するウィンドウ
    /// </summary>
    class DetailWindow: MenuComponent
    {
        public static readonly Vector EffectPos = new Vector(164, 11);
        public static readonly Vector Spacing = new Vector(0, 56);
        public readonly static FontID Font = FontID.CorpM;
        ArmorDetail armorDetail;
        Image backImg;

        EquipEffectImg luneEquip, sunnyEquip;

        public DetailWindow(Vector pos, PersonParty party)
        {
            armorDetail = new ArmorDetail(pos);
            backImg = new Image(TextureID.White, pos, DepthID.MessageBack);
            backImg.Size = new Vector(311, 140); // tmp
            backImg.Color = ColorPalette.DarkBlue;

            luneEquip = new EquipEffectImg(pos + EffectPos, TextureID.WorldMapIcon, party.Get(CharaName.Lune));
            sunnyEquip = new EquipEffectImg(pos + EffectPos + Spacing, TextureID.WorldMapIcon, party.Get(CharaName.Sunny));
            AddComponents(new IComponent[] { backImg, armorDetail, luneEquip, sunnyEquip });
        }

        public void Refresh(ItemPanelBase selectedPanel)
        {
            armorDetail.Refresh(selectedPanel);
            luneEquip.Refresh(selectedPanel);
            sunnyEquip.Refresh(selectedPanel);
        }

        /// <summary>
        /// 装備品の性能を比較して表示するクラス
        /// </summary>
        class EquipEffectImg: MenuComponent
        {
            private static readonly DepthID Depth = DepthID.Message;
            private static readonly Vector EffectPos = new Vector(98, 20);
            EquipSet equip;
            Image faceImg;
            TextImage upImg, downImg;
            bool quit;
            public EquipEffectImg(Vector pos, TextureID faceTex, Person p)
            {
                equip = p.Equip;
                faceImg = new Image(faceTex, pos, Depth);

                // tmp
                upImg = new TextImage(FontID.CorpM, pos + EffectPos, Depth);
                upImg.Text = "UP";
                upImg.Color = ColorPalette.AliceBlue;
                downImg = new TextImage(FontID.CorpM, pos + EffectPos, Depth);
                downImg.Text = "DOWN";
                downImg.Color = ColorPalette.AliceBlue;
                AddComponents(new[] { faceImg });
            }

            public void Refresh(ItemPanelBase itemPanel)
            {
                if (quit) return;
                var data = ItemDataBase.Get(itemPanel.ID);
                int diff = GetDiff(data, equip);

                switch (diff)
                {
                    case int val when val > 0:
                        upImg.Alpha = 1;
                        downImg.Alpha = 0;
                        return;
                    case int val when val < 0:
                        upImg.Alpha = 0;
                        downImg.Alpha = 1;
                        return;
                    default:
                        DeleteEffect();
                        return;
                }


                int GetDiff(IItem dat, EquipSet _equip)
                {
                    switch (dat)
                    {
                        case WeaponData wd:
                            return wd.AttackData.Magical - _equip.Weapon.AttackData.Magical;
                        case ArmorData am:
                            return am.DefData.Physical - _equip.Armor.DefData.Physical;
                        default: return 0;
                    }
                }
            }

            public override void Call()
            {
                base.Call();
                quit = false;
            }

            public override void Quit()
            {
                base.Quit();
                DeleteEffect();
                quit = true;
            }

            public override void Update()
            {
                base.Update();
                upImg.Update();
                downImg.Update();
            }

            public override void Draw(Drawing d)
            {
                base.Draw(d);
                upImg.Draw(d);
                downImg.Draw(d);
            }

            private void DeleteEffect()
            {
                upImg.Alpha = 0;
                downImg.Alpha = 0;
            }
        }
    }
}
