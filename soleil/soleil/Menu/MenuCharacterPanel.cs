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
    class MenuCharacterPanel
    {
        readonly Vector FaceImgPos = new Vector(0, 0);
        readonly Vector HPPos = new Vector(120, 270);
        const int SpaceHPMP = 40; // HP表示とMP表示のy方向距離
        const int SpaceVal = 100; // "HP"とHPvalueのx方向表示距離
        Vector pos;
        Image faceImg;
        FontImage hpText, mpText;
        FontImage hpNumText, mpNumText;
        List<FontImage> fontImages;

        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;

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
            faceImg = new Image(0, Resources.GetTexture(textureID), pos + FaceImgPos, DepthID.MessageBack, false, true, 0);
            fontImages = new List<FontImage>();
            // hpmpImg
            hpText = new FontImage(FontID.Test, pos + HPPos, DepthID.MessageBack, true, 0);
            fontImages.Add(hpText);
            mpText = new FontImage(FontID.Test, pos + HPPos + new Vector(0, SpaceHPMP), DepthID.MessageBack, true, 0);
            fontImages.Add(mpText);
            hpText.Text = "HP";
            mpText.Text = "MP";
            hpNumText = new FontImage(FontID.Test, pos + HPPos + new Vector(SpaceVal,0), DepthID.MessageBack, true, 0);
            fontImages.Add(hpNumText);
            mpNumText = new FontImage(FontID.Test, pos + HPPos + new Vector(SpaceVal, SpaceHPMP), DepthID.MessageBack, true, 0);
            fontImages.Add(mpNumText);
            hpNumText.Text = hp.ToString();
            mpNumText.Text = mp.ToString();
            foreach (var item in fontImages)
            {
                item.Color = ColorPalette.DarkBlue;
            }
        }

        public void FadeIn()
        {
            faceImg.Fade(25, EaseFunc, true);
            foreach (var textImg in fontImages)
            {
                textImg.Fade(25, EaseFunc, true);
            }
        }

        public void FadeOut()
        {
            faceImg.Fade(25, EaseFunc, false);
            foreach (var textImg in fontImages)
            {
                textImg.Fade(25, EaseFunc, false);
            }
        }

        public void Update()
        {
            faceImg.Update();
            foreach (var textImg in fontImages)
            {
                textImg.Update();
            }
        }

        public void Draw(Drawing d)
        {
            faceImg.Draw(d);
            foreach (var textImg in fontImages)
            {
                textImg.Draw(d);
            }
        }
    }
}
