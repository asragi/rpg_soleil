using Soleil.Event;

namespace Soleil.Map
{
    abstract class MapObject
    {
        protected Vector Pos;
        protected bool Dead;
        protected int Frame;
        protected EventSequence EventSequence;

        public MapObject(ObjectManager om)
        {
            Dead = false;
            om.Add(this);

            EventSequence = new EventSequence();
        }

        virtual public void Update()
        {
            EventUpdate();
            Frame++;
        }

        virtual public void EventUpdate()
        {
            EventSequence.Update();
        }

        virtual public void Draw(Drawing sb)
        {

        }

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
