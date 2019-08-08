using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    /// <summary>
    /// 会話シーンに登場するキャラクターグラフィックのクラス．
    /// </summary>
    class ConversationPerson
    {
        readonly int[] xPositionArray = new[] { 100, 200, 350, 500, 650 };
        const int Y = 100;

        public string Name { get; private set; }
        public int Position { get; private set; }
        public string Face { get; private set; }

        Dictionary<string, Image> images;

        public ConversationPerson(string name, int position)
        {
            Name = name;
            Position = position;

            // Create Face Images
            images = new Dictionary<string, Image>();
            var faces = FaceDictionary.GetFaces(name);
            foreach (var f in faces)
            {
                var tex = FaceDictionary.Get(Name, f);
                var pos = new Vector(xPositionArray[Position], Y);
                var image = new Image(tex, pos, Vector.Zero, DepthID.PlayerFront);
                images.Add(f, image);
            }
        }

        public void Init() => Init(FaceDictionary.GetFaces(Name)[0]);
        public void Init(string face) => SetFace(face);
        public void Quit()
        {
            if (Face == null) return;
            images[Face].Quit();
        }

        public void SetFace(string face)
        {
            if (Face == null)
            {
                images[face].Call();
            }
            if (Face != face)
            {
                // 表情変化時のトランジション処理
                if (Face != null) images[Face].Quit();
                images[face].Call();
            }
            Face = face;
        }

        public void Update()
        {
            foreach (var v in images.Values)
            {
                v.Update();
            }
        }

        public void Draw(Drawing d)
        {
            foreach (var v in images.Values)
            {
                v.Draw(d);
            }
        }
    }
}
