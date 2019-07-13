using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Soleil.Images;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class Image : ImageBase
    {
        Texture2D tex;
        bool origin;
        public int Id { get; private set; }
        public Rectangle Rectangle { get; set; }
        public Vector Size { get; set; } = Vector.One;
        public override Vector ImageSize => new Vector(tex.Width, tex.Height);

        public Image(TextureID id, Vector pos, DepthID dep, bool centerOrigin = false, bool isStatic = true, float alpha = 0)
            : this(id, pos, Vector.Zero, dep, centerOrigin, isStatic, alpha) { }

        public Image(TextureID id, Vector pos, Vector _posDiff, DepthID dep, bool centerOrigin = false, bool isStatic = true, float alpha = 0)
            : base(pos, _posDiff, dep, centerOrigin, isStatic, alpha)
        {
            tex = Resources.GetTexture(id);
            Rectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = centerOrigin;
        }

        public void TexChange(TextureID id, bool centerOrigin = false)
        {
            tex = Resources.GetTexture(id);
            Rectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = centerOrigin;
        }

        public override void Draw(Drawing d)
        {
            var tmp = d.CenterBased;
            d.CenterBased = origin;
            if (IsStatic) d.DrawUI(Pos, tex, Rectangle, Color, DepthID, Size, Alpha, Angle);
            else d.DrawWithColor(Pos, tex, Rectangle, DepthID, Color * Alpha, Size, Angle);
            d.CenterBased = tmp;
        }
    }
}
