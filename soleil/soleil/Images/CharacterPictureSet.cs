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
    class CharacterPictureSet
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
            Pos = _pos;
        }
    }
}
