using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// プレイヤーの位置によって描画レイヤーを変更するマップオブジェクトのクラス.
    /// </summary>
    class AdjustConstruct : MapConstruct
    {
        int adjustY;
        PlayerObject player;
        public AdjustConstruct(TextureID tex, int adjustY,ObjectManager om)
            : this(Vector.Zero, tex, adjustY, om) { }
        public AdjustConstruct(Vector _pos,TextureID tex,int _adjustY, ObjectManager om)
            : base(_pos,tex,MapDepth.Adjust,om)
        {
            player = om.GetPlayer();
            adjustY = _adjustY;
        }

        public override void Update()
        {
            AdjustLayer();
            base.Update();
        }

        /// <summary>
        /// 描画レイヤーを変更する.
        /// </summary>
        private void AdjustLayer()
        {
            if (player.GetPosition().Y < adjustY) // プレイヤーの方が画面上部にいる場合
            {
                depthId = HigherLayer;
                return;
            }
            depthId = LowerLayer;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
        }
    }
}
