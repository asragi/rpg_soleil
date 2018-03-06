namespace Soleil
{
    /// <summary>
    /// オブジェクト・マップの接触・衝突を管理する判定Boxクラス
    /// </summary>
    class CollideBox
    {
        public Vector size { get; }
        Vector localPos;
        Vector parentPos;
        MapObject parent;
        CollideLayer layer;

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(MapObject _parent, Vector _localPos, Vector _size, CollideLayer _layer, BoxManager bm)
        {
            parent = _parent;
            parentPos = parent.GetPosition();
            localPos = _localPos;
            size = _size;
            layer = _layer;
            bm.Add(this);
        }

        void CollideEnter()
        {
            parent.OnCollisionEnter();
        }

        void CollideStay()
        {
            parent.OnCollisionStay();
        }

        void CollideExit()
        {
            parent.OnCollisionExit();
        }
        public Vector WorldPos() => parent.GetPosition() + localPos; // あまりよくない
    }
}
