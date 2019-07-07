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
        const int DashSpeed = 12;
        int speed;
        // カーソルが画面端にどれだけ近づいたらカメラの移動を開始するか
        const int LimitDistance = 100;
        UIImage cursor;
        WorldMapCamera camera;
        public WorldMapCursorLayer(WorldMapCamera cam)
        {
            cursor = new UIImage(TextureID.WorldMapCursor, Vector.One, null, DepthID.Effect, isStatic: false);
            camera = cam;
        }

        public void Move(Direction inputDir)
        {
            if (inputDir == Direction.N) return;
            Vector cursorPosUpd = cursor.Pos + new Vector(- speed, 0).Rotate(inputDir.Angle());
            cursor.Pos = new Vector(MathEx.Clamp(cursorPosUpd.X, WorldMap.WorldMapSize.X, 0), 
                                    MathEx.Clamp(cursorPosUpd.Y, WorldMap.WorldMapSize.Y, 0));
            speed = Speed;
            var updatePos = UpdateCameraPos(cursor.Pos, camera.GetPosition(), LimitDistance);
            camera.SetPosition(updatePos);

            Vector UpdateCameraPos(Vector cursorPos, Vector cameraPos, int limitDistance)
            {
                var diff = cursorPos - cameraPos;
                Vector cameraPosUpd = cameraPos + new Vector(
                    ModifyValue(diff.X, Game1.VirtualWindowSizeX, limitDistance),
                    ModifyValue(diff.Y, Game1.VirtualWindowSizeY, limitDistance)
                    );
                return new Vector(
                    MathEx.Clamp(cameraPosUpd.X, WorldMap.WorldMapSize.X - Game1.VirtualWindowSizeX, 0), 
                    MathEx.Clamp(cameraPosUpd.Y, WorldMap.WorldMapSize.Y - Game1.VirtualWindowSizeY, 0));

                double ModifyValue(double x, int windowSize, int limitDist)
                {
                    if (x < limitDist) return x - limitDist;
                    if (x > windowSize - limitDist) return x - (windowSize - limitDist);
                    return 0;
                }
            }
        }

        public void OnInputSubmitDown()
        {
            speed = DashSpeed;
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
