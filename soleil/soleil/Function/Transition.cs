using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    public enum TransitionMode { None,FadeIn,FadeOut}

    class Transition
    {
        const int TransitionTime = 20; // リソースの枚数と対応するため変更時注意
        TransitionMode mode;
        Texture2D[] white;
        int index;
        bool stop;

        public Transition()
        {
            white = new Texture2D[TransitionTime];
            for (int i = 0; i < TransitionTime; i++)
            {
                white[i] = Resources.GetTexture(TextureID.Rule0 + i);
            }
            index = 0;
            stop = true;
            mode = TransitionMode.None;
        }

        public void SetMode(TransitionMode _mode)
        {
            mode = _mode;
            switch (mode)
            {
                case TransitionMode.None:
                    break;
                case TransitionMode.FadeOut:
                    RefreshForFade(mode);
                    break;
                case TransitionMode.FadeIn:
                    RefreshForFade(mode);
                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            switch (mode)
            {
                case TransitionMode.FadeOut:
                    FadeOutUpdate();
                    break;
                case TransitionMode.FadeIn:
                    FadeInUpdate();
                    break;
                default:
                    break;
            }
        }

        private void RefreshForFade(TransitionMode mode)
        {
            switch (mode)
            {
                case TransitionMode.FadeIn:
                    index = 0;
                    stop = false;
                    break;
                case TransitionMode.FadeOut:
                    index = TransitionTime - 1;
                    stop = false;
                    break;
                default:
                    System.Console.WriteLine("Transition error");
                    break;
            }
        }

        private void FadeOutUpdate()
        {
            if (stop || index <= 0)
            {
                stop = true;
                SetMode(TransitionMode.None);
                return;
            }
            index--;
        }

        private void FadeInUpdate()
        {
            if (stop || index >= TransitionTime - 1)
            {
                stop = true;
                SetMode(TransitionMode.None);
                return;
            }
            index++;
        }

        public void Draw(Drawing d)
        {
            if (TransitionMode.None == mode && index == TransitionTime - 1) return;
            var tmp = d.CenterBased;
            d.CenterBased = false;
            d.SetDrawAbsolute();
            d.Draw(Vector.Zero, white[index], DepthID.Debug);
            d.SetDrawNormal();
            d.CenterBased = tmp;
        }

        private int ReturnLuminance(Color c) => (int)(0.3 * c.R + 0.6 * c.G + 0.1 * c.B);
    }
}
