using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// ワールドマップを閲覧するためにプレイヤーが操作するカーソル．
    /// </summary>
    class WorldMapCursor : ICollideObject
    {
        public Vector Pos
        {
            get => img.Pos;
            set => img.Pos = value;
        }
        Image img;
        CollideBox collideBox;
        public WorldMapCursor(BoxManager bm)
        {
            img = new Image(TextureID.WorldMapCursor, Vector.Zero, DepthID.Effect, isStatic: false);
            collideBox = new CollideBox(this, Vector.Zero, Vector.One, CollideLayer.Player, bm);
        }

        public Vector GetPosition() => Pos;

        public void Call(bool move)
        {
            img.Call(move);
            collideBox.IsActive = true;
        }
        public void Quit(bool move)
        {
            img.Quit(move);
            collideBox.IsActive = false;
        }
        public void Update() => img.Update();
        public void Draw(Drawing d) => img.Draw(d);

        public void OnCollisionEnter(CollideBox cb) { }
        public void OnCollisionExit(CollideBox cb) { }
        public void OnCollisionStay(CollideBox cb) { }
    }
}
