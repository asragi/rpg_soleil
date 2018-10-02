using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// Imageを管理するクラス
    /// </summary>
    class ImageManager
    {
        List<ImageBase> images;
        public ImageManager()
        {
            images = new List<ImageBase>();
        }

        /// <summary>
        /// Imageを生成する。
        /// </summary>
        /// <returns>作られたImageを返す。</returns>
        public Image CreateImg(TextureID texid, Vector pos, DepthID depth, bool centerOrigin=true, bool isStatic = true, int id = 0)
        {
            // Imageを生成
            Image img = new Image(id, Resources.GetTexture(texid), pos, depth, centerOrigin, isStatic);
            // Listに登録
            images.Add(img);
            return img;
        }

        /// <summary>
        /// 全てのImageをTargetに向けてdurationフレームかけて移動させる。
        /// </summary>
        public void MoveToAll(Vector target, int duration, Func<double,double,double,double,double> easeFunc)
        {
            images.ForEach(img => img.MoveTo(target, duration, easeFunc));
        }

        public void FadeAll(int duration, Func<double,double,double,double,double> easeFunc, bool isFadein)
        {
            images.ForEach(img => img.Fade(duration, easeFunc, isFadein));
        }

        public void Update()
        {
            images.RemoveAll(s => s.IsDead);
            images.ForEach(s => s.Update());
        }

        public void Draw(Drawing d)
        {
            images.ForEach(s => s.Draw(d));
        }
    }
}
