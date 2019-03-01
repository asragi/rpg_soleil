using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// キャラクター立ち絵と表情の対応を管理するクラス．
    /// </summary>
    static class CharacterPictureMap
    {
        static TextureID[,] textures;

        static CharacterPictureMap()
        {
            textures = Set();
        }

        private static TextureID[,] Set()
        {
            var result = new TextureID[(int)CharaName.size, (int)FaceType.size];
            // Lune
            var t = (int)CharaName.Lune;
            result[t, (int)FaceType.Normal] = TextureID.MenuLune;
            result[t, (int)FaceType.Normal] = TextureID.MenuLune;
            // Sunny
            t = (int)CharaName.Sunny;
            result[t, (int)FaceType.Normal] = TextureID.MenuSun;
            result[t, (int)FaceType.Normal] = TextureID.MenuSun;
            return result;
        }

        public static TextureID GetTexture(CharaName name, FaceType face)
            => textures[(int)name, (int)face];
    }
}
