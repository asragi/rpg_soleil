using Soleil.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニューにおけるキャラクターのステータス表示コンポーネント
    /// </summary>
    class MenuCharacterPanel : MenuComponent
    {
        readonly Vector FaceImgPos = new Vector(0, 0);
        readonly Vector HPPos = new Vector(18, 260);
        const int SpaceHPMP = 27; // HP表示とMP表示のy方向距離
        const int SpaceVal = 105; // "HP"とHPvalueのx方向表示距離
        Vector pos;
        Image faceImg;
        TextImage hpText, mpText, lvText;
        TextImage hpNumText, mpNumText, lvNumText;
        readonly Vector posDiff = new Vector(50, 0);

        Person person;

        // animation
        int targetHP, targetMP;
        // Gauge
        readonly Vector GaugeDiff = new Vector(0, 20);
        readonly Vector GaugeSize = new Vector(105, 6);
        UIGauge hpGauge, mpGauge;

        public int FrameWait
        {
            set
            {
                var target = new ImageBase[] { faceImg, hpText, mpText, lvText, hpNumText, mpNumText, lvNumText };
                foreach (var item in target)
                {
                    item.FrameWait = value;
                }
                hpGauge.FrameWait = value;
                mpGauge.FrameWait = value;
            }
        }

        // ほんとは引数でキャラクターIDとかを渡してデータを参照する感じにしたいよね．
        public MenuCharacterPanel(Person p, Vector _pos, TextureID textureID)
        {
            person = p;
            pos = _pos;
            int hp, mp, hpMax, mpMax;
            hp = p.Score.HP;
            mp = p.Score.MP;
            hpMax = p.Score.HPMAX;
            mpMax = p.Score.MPMAX;

            // Images
            faceImg = new Image(textureID, pos + FaceImgPos, posDiff, DepthID.MenuBottom, false, true, 0);
            // hpmpImg
            var font = FontID.CorpM;
            hpText = new TextImage(font, pos + HPPos, posDiff, DepthID.MenuBottom, true, 0);
            mpText = new TextImage(font, pos + HPPos + new Vector(0, SpaceHPMP), posDiff, DepthID.MenuBottom, true, 0);
            lvText = new TextImage(font, pos + HPPos + new Vector(30, -SpaceHPMP), posDiff, DepthID.MenuBottom);
            hpText.Text = "HP";
            mpText.Text = "MP";
            lvText.Text = "Lv";
            hpText.ActivateOutline(1);
            mpText.ActivateOutline(1);
            lvText.ActivateOutline(1);
            hpNumText = new RightAlignText(font, pos + HPPos + new Vector(SpaceVal,0), posDiff, DepthID.MenuBottom);
            mpNumText = new RightAlignText(font, pos + HPPos + new Vector(SpaceVal, SpaceHPMP), posDiff, DepthID.MenuBottom);
            lvNumText = new RightAlignText(font, pos + HPPos + new Vector(SpaceVal, -SpaceHPMP), posDiff, DepthID.MenuBottom);
            hpNumText.Text = hp.ToString();
            mpNumText.Text = mp.ToString();
            lvNumText.Text = "3";
            hpNumText.Color = ColorPalette.GlayBlue;
            mpNumText.Color = ColorPalette.GlayBlue;
            hpNumText.ActivateOutline(1);
            mpNumText.ActivateOutline(1);
            lvNumText.ActivateOutline(1);
            // HP MP bar
            hpGauge = new UIGauge(pos + HPPos + GaugeDiff, posDiff, GaugeSize, false, DepthID.MenuBottom);
            mpGauge = new UIGauge(pos + HPPos + GaugeDiff + new Vector(0, SpaceHPMP), posDiff, GaugeSize, false, DepthID.MenuBottom);
            hpGauge.Refresh((double)hp / hpMax);
            mpGauge.Refresh((double)mp / mpMax);
            AddComponents(new IComponent[] { faceImg, hpGauge, mpGauge, hpText, hpNumText, mpText, mpNumText, lvText, lvNumText });

            targetHP = hp;
            targetMP = mp;
        }

        public override void Update()
        {
            base.Update();
            UpdateStatusVal();

            void UpdateStatusVal()
            {
                if(person.Score.HP != targetHP)
                {
                    targetHP = person.Score.HP;
                    hpNumText.Text = targetHP.ToString();
                    hpGauge.Refresh((double)targetHP / person.Score.HPMAX);
                }

                if(person.Score.MP != targetMP)
                {
                    targetMP = person.Score.MP;
                    mpNumText.Text = targetMP.ToString();
                    hpGauge.Refresh((double)targetMP / person.Score.HPMAX);
                }
            }
        }
    }
}
