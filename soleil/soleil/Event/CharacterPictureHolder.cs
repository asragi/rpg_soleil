using Soleil.Misc;
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
        const int PictureNum = 5; // odd number
        const int ImageY = 200;
        const int Spacing = 200;
        CharacterPictureSet[] pictures;

        public CharacterPictureHolder()
        {
            pictures = new CharacterPictureSet[PictureNum];
        }

        public void Create(CharaName name, int position)
        {
            int xDiff = Spacing * (position - (PictureNum - PictureNum / 2));
            Vector _pos = new Vector(Game1.GameCenterX + xDiff, ImageY);
            pictures[position] = new CharacterPictureSet(name, _pos);
            pictures[position].Call();
        }

        private int FindByName(CharaName name)
        {
            int index = -1;
            for (int i = 0; i < pictures.Length; i++)
            {
                var target = pictures[i];
                if (target == null) continue;
                if (target.Name == name) index = i;
            }
            return index;
        }

        public void CallCharacter(CharaName name)
        {
            pictures[FindByName(name)].Call();
        }

        public void QuitCharacter(CharaName name)
        {
            pictures[FindByName(name)].Quit();
        }

        public void Update()
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i]?.Update();
            }
        }

        public void Draw(Drawing d)
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i]?.Draw(d);
            }
        }
    }
}
