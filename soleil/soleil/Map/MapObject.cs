using Soleil.Event;

namespace Soleil
{
    abstract class MapObject
    {
        protected Vector pos;
        protected bool dead;
        protected int frame;
        protected EventSequence EventSequence;

        public MapObject(ObjectManager om)
        {
            dead = false;
            om.Add(this);

            EventSequence = new EventSequence();
        }

        virtual public void Update()
        {
            EventUpdate();
            frame++;
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
            return pos;
        }

        public bool IsDead()
        {
            return dead;
        }

        virtual public void OnCollisionEnter()
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
