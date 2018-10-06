using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// マップの固定オブジェクトのうち，animationするもの．
    /// </summary>
    class FadeAnimationConstruct : MapConstruct
    {
        Image[] images;
        int frame;
        public FadeAnimationConstruct(Vector _pos, TextureID[] textureIDs, int interval, MapDepth depth, ObjectManager om)
            : base(_pos, TextureID.White, depth, om)
        {
            images = new Image[textureIDs.Length];
            for (int i = 0; i < textureIDs.Length; i++)
            {
                images[i] = new Image(0, Resources.GetTexture(textureIDs[i]), _pos, LowerLayer, false, false, (i == 0) ? 1 : 0);
            }
        }

        public override void Update()
        {
            frame++;
            base.Update();
        }

        public override void Draw(Drawing d)
        {
            // baseをDrawしない
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Draw(d);
            }
        }
    }
}
