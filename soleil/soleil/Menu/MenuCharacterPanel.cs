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
        readonly Vector HPPos = new Vector(120, 270);
        const int SpaceHPMP = 40; // HP表示とMP表示のy方向距離
        const int SpaceVal = 100; // "HP"とHPvalueのx方向表示距離
        Vector pos;
        UIImage faceImg;
        FontImage hpText, mpText;
        FontImage hpNumText, mpNumText;

        // ほんとは引数でキャラクターIDとかを渡してデータを参照する感じにしたいよね．
        public MenuCharacterPanel(Vector _pos, TextureID textureID)
        {
            pos = _pos;
            int hp, mp;
            if(textureID == TextureID.MenuLune) // DEBUG
            {
                hp = 297;
                mp = 834;
            }
            else
            {
                hp = 654;
                mp = 425;
            }
            // Images
            faceImg = new UIImage(textureID, pos + FaceImgPos, Vector.Zero, DepthID.MenuBottom, false, true, 0);
            // hpmpImg
            var font = FontID.KkBlack;
            hpText = new FontImage(font, pos + HPPos, DepthID.MenuBottom, true, 0);
            mpText = new FontImage(font, pos + HPPos + new Vector(0, SpaceHPMP), DepthID.MenuBottom, true, 0);
            hpText.Text = "HP";
            mpText.Text = "MP";
            hpNumText = new FontImage(font, pos + HPPos + new Vector(SpaceVal,0), DepthID.MenuBottom, true, 0);
            mpNumText = new FontImage(font, pos + HPPos + new Vector(SpaceVal, SpaceHPMP), DepthID.MenuBottom, true, 0);
            hpNumText.Text = hp.ToString();
            mpNumText.Text = mp.ToString();
            Components = new IComponent[] { faceImg, hpText, hpNumText, mpText, mpNumText };
        }
    }
}
