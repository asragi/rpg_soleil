using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 立ち絵を制御するクラス．
    /// </summary>
    class CharacterPictureHolder
    {
        const int PictureNum = 5;
        CharacterPictureSet[] pictures;

        public CharacterPictureHolder()
        {
            pictures = new CharacterPictureSet[PictureNum];
        }
    }
}
