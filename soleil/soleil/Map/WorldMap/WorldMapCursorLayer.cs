using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// カーソル移動で地図を眺めるモードの処理を管理するクラス
    /// </summary>
    class WorldMapCursorLayer
    {
        const int Speed = 5;
        UIImage cursor;
        WorldMapCamera camera;
        public WorldMapCursorLayer(WorldMapCamera cam)
        {
            cursor = new UIImage(TextureID.MenuSave1, Vector.One, null, DepthID.Effect, isStatic: false);
            camera = cam;
        }

        public void Move(Direction inputDir)
        {
            if (inputDir == Direction.N) return;
            cursor.Pos += new Vector(- Speed, 0).Rotate(inputDir.Angle());
            camera.SetPosition(UpdateCameraPos(cursor.Pos));

            Vector UpdateCameraPos(Vector cursorPos)
            {
                return cursorPos;
            }
        }

        public void Init(Vector pos)
        {
            cursor.Pos = pos;
            cursor.Call(false);
        }

        public void Quit(Vector pos)
        {
            camera.SetDestination(pos);
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
