using Soleil.Menu;
using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum FaceType
    {
        Normal,
        Smile,
        size
    }

    /// <summary>
    /// キャラクターごとの立ち絵のセット
    /// </summary>
    class CharacterPictureSet: IComponent
    {
        FaceType faceType;
        CharaName name;
        CharacterPicture[] pictures;

        public Vector pos;
        public Vector Pos {
            get => pos;
            set
            {
                pos = value;
                for (int i = 0; i < pictures.Length; i++)
                {
                    pictures[i].Pos = pos;
                }
            }
        }

        public CharacterPictureSet(CharaName _name, Vector _pos)
        {
            name = _name;
            pictures = new CharacterPicture[(int)FaceType.size];
            for (int i = 0; i < pictures.Length; i++)
            {
                var face = (FaceType)i;
                var texID = CharacterPictureMap.GetTexture(name, face);
                pictures[i] = new CharacterPicture(texID, _pos, null, false);
            }
            Pos = _pos;
        }

        public void ChangeFace(FaceType faceTo)
        {
            pictures[(int)faceType].Quit();
            faceType = faceTo;
            pictures[(int)faceType].Call();
        }

        public void Call()
        {
            pictures[(int)faceType].Call();
        }

        public void Quit()
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i].Quit();
            }
        }

        public void Update()
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i].Update();
            }
        }

        public void Draw(Drawing d)
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i].Draw(d);
            }
        }
    }
}
