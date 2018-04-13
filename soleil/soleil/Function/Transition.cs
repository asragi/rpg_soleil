using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    public enum TransitionMode { None,FadeIn,FadeOut}

    class Transition
    {
        const int DefaultTransitionSpeed = 21;

        int transitionSpeed;
        Color transitionColor;

        TransitionMode mode;
        Texture2D white;
        Color[] texData;
        Texture2D rule;
        int[] alphaData; // Rule画像からalpha値の配列を生成
        int[] nowAlpha; // 適用する現在のalpha値

        public Transition()
        {
            transitionSpeed = DefaultTransitionSpeed;
            transitionColor = Color.AliceBlue;
            mode = TransitionMode.None;
            rule = Resources.GetTexture(TextureID.Rule1);
            white = Resources.GetTexture(TextureID.WhiteWindow);
            texData = new Color[white.Width * white.Height];
            alphaData = new int[rule.Width * rule.Height];
            nowAlpha = new int[rule.Width * rule.Height];
            SetRule();
        }

        // ここだけで1Fくらいかかりそう
        void SetRule()
        {
            var data = new Color[rule.Width * rule.Height];
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
            var addNum = 255; // 初期の透明度に加減算する値
            if (mode == TransitionMode.FadeOut) addNum *= -1;
            for (int i = 0; i < nowAlpha.Length; i++)
            {
                nowAlpha[i] = alphaData[i] + addNum;
            }
            white.GetData(texData);
        }

        private void FadeOutUpdate()
        {
            var stop = true;
            for (int i = 0; i < texData.Length; i++)
            {
                nowAlpha[i] += transitionSpeed;
                texData[i] = transitionColor * (MathEx.Clamp(nowAlpha[i], 255, 0)/255.0f);
                if (nowAlpha[i] < 255) stop = false; // 1pixelでも不透明になりきっていなければ処理を止めない
            }
            white.SetData(texData);
            if (stop) SetMode(TransitionMode.None);
        }

        private void FadeInUpdate()
        {
            var stop = true;
            for (int i = 0; i < texData.Length; i++)
            {
                nowAlpha[i] -= transitionSpeed;
                texData[i] = transitionColor * (MathEx.Clamp(nowAlpha[i], 255, 0) / 255.0f);
                if (nowAlpha[i] >= 0) stop = false; // 1pixelでも透明になりきっていなければ処理を止めない
            }
            white.SetData(texData);
            if (stop) SetMode(TransitionMode.None);
        }

        public void Draw(Drawing d)
        {
            var tmp = d.CenterBased;
            d.CenterBased = false;
            d.SetDrawAbsolute();
            d.Draw(Vector.Zero, white, DepthID.Debug);
            d.SetDrawNormal();
            d.CenterBased = tmp;
        }

        private int ReturnLuminance(Color c) => (int)(0.3 * c.R + 0.6 * c.G + 0.1 * c.B);
    }
}
