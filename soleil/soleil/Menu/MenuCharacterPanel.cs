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
        UIImage faceImg;
        FontImage hpText, mpText, lvText;
        FontImage hpNumText, mpNumText, lvNumText;
        readonly Vector posDiff = new Vector(50, 0);

        public int FrameWait
        {
            set
            {
                var target = new ImageBase[] { faceImg, hpText, mpText, lvText, hpNumText, mpNumText, lvNumText };
                foreach (var item in target)
                {
                    item.FrameWait = value;
                }
            }
        }

        // ほんとは引数でキャラクターIDとかを渡してデータを参照する感じにしたいよね．
        public MenuCharacterPanel(Person p, Vector _pos, TextureID textureID)
        {
            pos = _pos;
            int hp, mp;
            hp = p.Score.HPMAX;
            mp = p.Score.MPMAX;
            // Images
            faceImg = new UIImage(textureID, pos + FaceImgPos, posDiff, DepthID.MenuBottom, false, true, 0);
            // hpmpImg
            var font = FontID.CorpM;
            hpText = new FontImage(font, pos + HPPos, posDiff, DepthID.MenuBottom, true, 0);
            mpText = new FontImage(font, pos + HPPos + new Vector(0, SpaceHPMP), posDiff, DepthID.MenuBottom, true, 0);
            lvText = new FontImage(font, pos + HPPos + new Vector(30, -SpaceHPMP), posDiff, DepthID.MenuBottom);
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
            AddComponents(new IComponent[] { faceImg, hpText, hpNumText, mpText, mpNumText, lvText, lvNumText });
        }
    }
}
