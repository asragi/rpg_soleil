using Soleil.Menu;
using System;

namespace Soleil.Title
{
    /// <summary>
    /// TitleSceneでうごめく線状の装飾
    /// </summary>
    class TitleOrnamentBar: IComponent
    {
        private const TextureID Texture = TextureID.MenuLine;
        private const int MoveSpeed = 1;
        private const DepthID Depth = DepthID.Message;

        private readonly int texWidth, texNum;
        private readonly Image[] lines;
        private readonly double angle;
        private readonly Vector standardPos;
        
        /// <param name="linePoints">直線を通る2点</param>
        public TitleOrnamentBar((Vector, Vector) linePoints) {
            Vector a = linePoints.Item1, b = linePoints.Item2;
            standardPos = a;
            angle = Math.Atan((a.Y - b.Y) / (a.X - b.X));

            texWidth = Resources.GetTexture(Texture).Width;
            // 画面内に映る直線の長さが最大となるのは対角線の場合
            // 対角線の長さ < (縦 + 横) より (縦 + 横) をtexture幅で割った枚数あれば十分
            texNum = (Game1.VirtualWindowSizeX + Game1.VirtualWindowSizeY) / texWidth + 1;
            lines = new Image[texNum];
            for (int i = 0; i < texNum; i++)
            {
                lines[i] = new Image(
                    Texture,
                    a + new Vector(texWidth * Math.Cos(angle), texWidth * Math.Sin(angle)) * (i - 1),
                    Depth
                    )
                { Angle = (float)angle };
            }
        }

        public void Call() => lines.ForEach2(s => s.Call(move: false));
        public void Quit() => lines.ForEach2(s => s.Quit(move: false));
        public void Update()
        {
            lines.ForEach2(s => s.Update());

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Pos += new Vector(MoveSpeed * Math.Cos(angle), MoveSpeed * Math.Sin(angle));
                if (lines[i].Pos.X > standardPos.X + Game1.VirtualWindowSizeX + texWidth || lines[i].Pos.Y > standardPos.Y + Game1.VirtualWindowSizeY + texWidth)
                {
                    lines[i].Pos -= new Vector(texWidth * Math.Cos(angle), texWidth * Math.Sin(angle)) * (lines.Length - 1);
                }
            }
        }

        public void Draw(Drawing d) => lines.ForEach2(s => s.Draw(d));
    }
}
