using Soleil.Event;

namespace Soleil.Map
{
    abstract class MapObject
    {
        protected Vector Pos;
        protected bool Dead;
        protected int Frame;

        public MapObject(ObjectManager om)
        {
            Dead = false;
            om.Add(this);

        }

        virtual public void Update()
        {
            Frame++;
        }

        virtual public void Draw(Drawing d) { }

        public Vector GetPosition()
        {
            return Pos;
        }

        public bool IsDead()
        {
            return Dead;
        }

        virtual public void OnCollisionEnter(CollideBox collide)
        {

        }

        virtual public void OnCollisionStay()
        {

        }

        virtual public void OnCollisionExit()
        {

        }
    }
}
