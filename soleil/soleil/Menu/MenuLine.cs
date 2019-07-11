using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    /// <summary>
    /// メニュー画面等で使う横に動く装飾用の罫線を描画するクラス
    /// </summary>
    class MenuLine : MenuComponent
    {
        const int MoveSpeed = 1;
        Image[] lines;
        int texWidth;
        bool moveLeft;

        // オシャレアニメ
        int posY;
        int diffY;
        bool isDiff;
        int easeFrame = 100000;
        double startY, destinationY;
        public MenuLine(int _posY, int _diffY, bool moveToLeft)
        {
            (posY, diffY, startY, destinationY) = (_posY, _diffY, _posY, _posY + _diffY);
            moveLeft = moveToLeft;
            var tex = Resources.GetTexture(TextureID.MenuLine);
            texWidth = tex.Width;
            var texNum = Game1.VirtualWindowSizeX / texWidth + 3;
            lines = new Image[texNum];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new Image(TextureID.MenuLine, new Vector(i * texWidth, _posY), Vector.Zero, DepthID.MenuTop);
            }
        }

        public override void Call()
        {
            base.Call();
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Call(move:false);
            }
        }

        public override void Quit()
        {
            base.Quit();
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Quit(move:false);
            }
        }

        public override void Update()
        {
            base.Update();
            easeFrame++;
            // Move Lines
            for (int i = 0; i < lines.Length; i++)
            {
                var tmp = lines[i].Pos;
                // X
                if (moveLeft)
                {
                    tmp.X -= MoveSpeed;
                }
                else
                {
                    tmp.X += MoveSpeed;
                }
                // 完全に画面の外に出たら最初に先頭に戻す
                if(moveLeft && tmp.X < -texWidth)
                {
                    tmp.X += (lines.Length - 1) * texWidth;
                }
                else if(!moveLeft && tmp.X > Game1.VirtualWindowSizeX)
                {
                    tmp.X -= (lines.Length - 1) * texWidth;
                }

                // Y
                tmp.Y = Ease();
                lines[i].Pos = tmp;
            }
            // Image Update
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Update();
            }

            double Ease()
            {
                if (easeFrame > MenuSystem.FadeSpeed) return (isDiff)? posY + diffY : posY;
                return MenuSystem.EaseFunc(easeFrame, MenuSystem.FadeSpeed, destinationY, startY);
            }
        }

        public void StartMove(bool _isDiff)
        {
            isDiff = _isDiff;
            easeFrame = 0;
            startY = lines[0].Pos.Y;
            destinationY = isDiff ? posY + diffY : posY;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Draw(d);
            }
        }
    }
}
