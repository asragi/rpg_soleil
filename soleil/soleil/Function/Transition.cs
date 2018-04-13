using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    public enum TransitionMode { None,FadeIn,FadeOut}

    class Transition
    {
        const int DefaultTransitionSpeed = 21;
        int transitionSpeed;
        TransitionMode mode;
        Texture2D white;
        Color[] texData;
        Texture2D rule;
        int[] alphaData; // Rule画像からalpha値の配列を生成
        int[] nowAlpha; // 適用する現在のalpha値

        public Transition()
        {
            transitionSpeed = DefaultTransitionSpeed;
            mode = TransitionMode.None;
            rule = Resources.GetTexture(TextureID.Rule1);
            white = Resources.GetTexture(TextureID.WhiteWindow);
            texData = new Color[white.Width * white.Height];
            SetRule();
        }

        // ここだけで1Fくらいかかりそう
        void SetRule()
        {
            var data = new Color[rule.Width * rule.Height];
            alphaData = new int[data.Length];
            nowAlpha = new int[data.Length];
            rule.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                alphaData[i] = ReturnLuminance(data[i]);
            }
        }

        public void SetMode(TransitionMode _mode)
        {
            mode = _mode;
            switch (mode)
            {
                case TransitionMode.None:
                    break;
                case TransitionMode.FadeOut:
                    break;
                case TransitionMode.FadeIn:
                    RefreshForFadeIn();
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
                    break;
                case TransitionMode.FadeIn:
                    FadeInUpdate();
                    break;
                default:
                    break;
            }
        }


        private void RefreshForFadeIn()
        {
            for (int i = 0; i < nowAlpha.Length; i++)
            {
                nowAlpha[i] = alphaData[i] + 255;
            }
            white.GetData(texData);
        }

        private void FadeInUpdate()
        {
            for (int i = 0; i < texData.Length; i++)
            {
                nowAlpha[i] -= transitionSpeed;
                texData[i] = texData[i] * (MathEx.Clamp(nowAlpha[i], 255, 0) / 255.0f);
            }
            white.SetData(texData);
        }

        public void Draw(Drawing d)
        {
            var tmp = d.CenterBased;
            d.CenterBased = false;
            d.Draw(Vector.Zero, white, DepthID.Debug);
            d.CenterBased = tmp;
        }

        private int ReturnLuminance(Color c) => (int)(0.3 * c.R + 0.6 * c.G + 0.1 * c.B);
    }
}
