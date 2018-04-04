using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// Map上の描画深度を管理する列挙型.
    /// </summary>
    public enum MapDepth
    {
        Ground,
        Adjust,
        Top,
    }
    /// <summary>
    /// マップ上の障害物などに関するクラス
    /// </summary>
    class MapConstruct : MapObject
    {
        protected const DepthID LowerLayer = DepthID.PlayerBack;
        protected const DepthID HigherLayer = DepthID.PlayerFront;
        Texture2D texture;
        MapDepth mapDepth;
        protected DepthID depthId;

        public MapConstruct(TextureID tex,MapDepth dep,ObjectManager om)
            : this(Vector.Zero,tex,dep,om){}

        public MapConstruct(Vector _pos, TextureID tex,MapDepth dep, ObjectManager om)
            :base(om)
        {
            pos = _pos;
            texture = Resources.GetTexture(tex);
            mapDepth = dep;
            switch (mapDepth)
            {
                case MapDepth.Ground:
                    depthId = LowerLayer;
                    break;
                case MapDepth.Adjust:
                    depthId = 0;
                    break;
                case MapDepth.Top:
                    depthId = HigherLayer;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Update()
        {
            base.Update();
        }


        public override void Draw(Drawing d)
        {
            var t = d.CenterBased;
            d.CenterBased = false;
            d.Draw(pos, texture, depthId);
            d.CenterBased = t;
            base.Draw(d);
        }
    }
}
