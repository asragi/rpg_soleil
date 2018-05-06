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
        List<Image> images;
        int index;
        public ImageManager()
        {
            images = new List<Image>();
        }

        /// <summary>
        /// Imageを生成する。
        /// </summary>
        /// <returns>作られたImageのIDを返す。</returns>
        public int Create(TextureID id, Vector pos, DepthID depth, bool centerOrigin=true, bool isStatic = true)
        {
            // Imageを生成
            Image img = new Image(index,Resources.GetTexture(id),pos,depth,centerOrigin,isStatic);
            // Listに登録
            images.Add(img);
            // IDを返し、ID振り分け用indexを進める
            return index++;
        }

        public void Destroy(int id)
        {
            images.Find(s => s.Id == id).IsDead = true;
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
