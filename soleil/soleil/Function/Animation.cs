using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    class Animation
    {
        public readonly int Height, Width;
        readonly Vector position;
        readonly DepthID depth;
        //readonly AnimationID ID;
        readonly Texture2D texture;
        readonly int xNum, yNum, sheetNum;
        bool isEnd = false;
        int x, y;

        //interval frames pass and animation sheet steps.
        int interval = 1;
        int frame = 0;

        //the viewing sheet of graph.
        int sheets;

        bool flipHorizontally; // アニメーションを左右反転させるフラグ.
        bool repeat;
        public int Sheets
        {
            set
            {
                sheets = value;
                //set position.
                x = sheets % xNum;
                y = sheets / xNum;
            }
            get { return sheets; }
        }

        public Animation(AnimationID id, int interval, DepthID depth, bool repeat = false)
        {
            //ID = id;
            texture = Resources.GetTexture(id);
            xNum = Resources.AnimeSplit[(int)id, 0];
            yNum = Resources.AnimeSplit[(int)id, 1];
            sheetNum = xNum * yNum;
            Width = Resources.Animes[(int)id].Width / xNum;
            Height = Resources.Animes[(int)id].Height / yNum;

            this.interval = interval;
            this.depth = depth;
            position = new Vector(0, 0);
            this.repeat = repeat;
        }

        public Animation(AnimationID id, int interval, DepthID depth, Vector position, bool repeat = false)
        {
            //ID = id;
            texture = Resources.GetTexture(id);
            xNum = Resources.AnimeSplit[(int)id, 0];
            yNum = Resources.AnimeSplit[(int)id, 1];
            sheetNum = xNum * yNum;
            Width = Resources.Animes[(int)id].Width / xNum;
            Height = Resources.Animes[(int)id].Height / yNum;

            this.interval = interval;
            this.depth = depth;
            this.position = position;
            this.repeat = repeat;
        }

        public Animation(AnimationData data)
        {
            //ID = data.AnimationID;;
            texture = Resources.GetTexture(data.AnimationID, data.ColorID);
            xNum = Resources.AnimeSplit[(int)data.AnimationID, 0];
            yNum = Resources.AnimeSplit[(int)data.AnimationID, 1];
            sheetNum = xNum * yNum;
            Width = Resources.Animes[(int)data.AnimationID].Width / xNum;
            Height = Resources.Animes[(int)data.AnimationID].Height / yNum;
            flipHorizontally = data.Flip;

            this.interval = data.Interval;
            this.depth = data.DepthID;
            this.position = data.Position;
            this.repeat = data.Repeat;
        }

        public Animation(EffectAnimationData data)
        {
            //ID = data.AnimationID;;
            texture = Resources.GetTexture(data.AnimationID);
            xNum = Resources.EffectAnimeSplit[(int)data.AnimationID, 0];
            yNum = Resources.EffectAnimeSplit[(int)data.AnimationID, 1];
            sheetNum = xNum * yNum;
            Width = Resources.EffectAnimes[(int)data.AnimationID].Width / xNum;
            Height = Resources.EffectAnimes[(int)data.AnimationID].Height / yNum;

            this.interval = data.Interval;
            this.depth = data.DepthID;
            this.position = data.Position;
            this.repeat = data.Repeat;
        }

        public void Reset()
        {
            frame = 0;
            Sheets = 0;
            isEnd = false;
        }
        public void End()
        {
            frame = interval * sheetNum;
            Sheets = sheetNum - 1;
        }
        public bool IsEnd()
            => isEnd;

        //If drawing was delayed, frame would count.
        public void Move() => frame++;

        void AnimationNext()
        {
            if (frame >= interval)
            {
                //frame -= (int)(frame/interval)*interval;
                frame -= interval;
                if (repeat)
                    Sheets = (Sheets + 1) % sheetNum;
                else
                {
                    if (sheetNum - 1 < Sheets + 1)
                        isEnd = true;
                    else
                        Sheets = Sheets + 1;
                }
            }
        }
        public void Draw(Drawing d, Vector2 pos, float size = 1, float angle = 0)
        {
            AnimationNext();
            if (flipHorizontally)
                d.DrawFlipHorizontally(pos + position.YAxialInversion(), texture, new Rectangle(x * Width, y * Height, Width, Height), depth, size, angle);
            else
                d.Draw(pos + position, texture, new Rectangle(x * Width, y * Height, Width, Height), depth, size, angle);
        }
        public void Draw(Drawing d, Vector2 pos, Color color, float size = 1, float angle = 0)
        {
            AnimationNext();
            if (flipHorizontally)
                d.DrawFlipHorizontallyWithColor(pos + position.YAxialInversion(), texture, new Rectangle(x * Width, y * Height, Width, Height), depth, color, size, angle);
            else
                d.DrawWithColor(pos + position, texture, new Rectangle(x * Width, y * Height, Width, Height), depth, color, size, angle);
        }

        /// <summary>
        /// AnimのDepth設定を無視して描画する．
        /// </summary>
        public void DrawWithDepth(Drawing d, Vector2 pos, Color color, DepthID _depth, float size = 1, float angle = 0)
        {
            AnimationNext();
            if (flipHorizontally)
                d.DrawFlipHorizontallyWithColor(pos + position.YAxialInversion(), texture, new Rectangle(x * Width, y * Height, Width, Height), _depth, color, size, angle);
            else
                d.DrawWithColor(pos + position, texture, new Rectangle(x * Width, y * Height, Width, Height), _depth, color, size, angle);

        }


        public void DrawFlipHorizontally(Drawing d, Vector2 pos, float size = 1, float angle = 0)
        {
            AnimationNext();
            if (!flipHorizontally)
                d.DrawFlipHorizontally(pos + position.YAxialInversion(), texture, new Rectangle(x * Width, y * Height, Width, Height), depth, size, angle);
            else
                d.Draw(pos + position, texture, new Rectangle(x * Width, y * Height, Width, Height), depth, size, angle);
        }
        public void DrawFlipHorizontally(Drawing d, Vector2 pos, Color color, float size = 1, float angle = 0)
        {
            AnimationNext();
            if (!flipHorizontally)
                d.DrawFlipHorizontallyWithColor(pos + position.YAxialInversion(), texture, new Rectangle(x * Width, y * Height, Width, Height), depth, color, size, angle);
            else
                d.DrawWithColor(pos + position, texture, new Rectangle(x * Width, y * Height, Width, Height), depth, color, size, angle);
        }
    }
}
