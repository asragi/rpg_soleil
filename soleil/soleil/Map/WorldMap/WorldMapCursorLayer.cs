using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapCursorLayer
    {
        const int Speed = 5;
        UIImage cursor;
        public WorldMapCursorLayer()
        {
            cursor = new UIImage(TextureID.MenuSave1, Vector.One, null, DepthID.Effect);
        }

        public void Move(Direction inputDir)
        {
            if (inputDir == Direction.N) return;
            cursor.Pos += new Vector(- Speed, 0).Rotate(inputDir.Angle());
        }

        public void Init()
        {
            cursor.Call(false);
        }

        public void Quit()
        {
            cursor.Quit(false);
        }

        public void Update()
        {
            cursor.Update();
        }

        public void Draw(Drawing d)
        {
            cursor.Draw(d);
        }
    }
}
