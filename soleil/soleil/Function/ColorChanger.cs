using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    static class ColorChanger
    {
        static GraphicsDevice graphicDevice;
        public static void Init(GraphicsDevice gd)
        {
            graphicDevice = gd;
        }
        public static Texture2D ColorChange(Texture2D texture, Dictionary<Color, Color> dictionary)
        {
            int h=texture.Height, w=texture.Width;
            var tex = new Texture2D(graphicDevice, w, h);

            var data = new Color[w*h];
            texture.GetData(data);
            data = data.Select(p => dictionary.ContainsKey(p) ? dictionary[p] : p ).ToArray();
            tex.SetData(data);

            return tex;
        }
    }
}
