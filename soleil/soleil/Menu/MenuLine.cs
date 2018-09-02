using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニュー画面等で使う横に動く装飾用の罫線を描画するクラス
    /// </summary>
    class MenuLine
    {
        const int MoveSpeed = 1;
        Image[] lines;
        int texWidth;
        bool moveLeft;
        public MenuLine(int posY, bool moveToLeft)
        {
            moveLeft = moveToLeft;
            var tex = Resources.GetTexture(TextureID.MenuLine);
            texWidth = tex.Width;
            var texNum = Game1.VirtualWindowSizeX / texWidth + 3;
            lines = new Image[texNum];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new Image(0, tex, new Vector(i * texWidth, posY), DepthID.MessageBack, false);
            }
        }

        public void Update()
        {
            // Move Lines
            for (int i = 0; i < lines.Length; i++)
            {
                var tmp = lines[i].Pos;
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
                lines[i].Pos = tmp;
            }
        }

        public void Draw(Drawing d)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Draw(d);
            }
        }
    }
}
