using Microsoft.Xna.Framework;
using System;

namespace CS5410.Objects
{
    public class AnimatedSprite
    {
        private readonly Vector2 m_size;
        protected Vector2 m_center;
        protected float m_rotation = 0;

        public AnimatedSprite(Vector2 size, Vector2 center)
        {
            m_size = size;
            m_center = center;
        }

        public Vector2 Size
        {
            get { return m_size; }
        }

        public Vector2 Center
        {
            get { return m_center; }
        }

        public bool collide(AnimatedSprite other)
        {
            //Vector2 rect1 = this.m_center - (this.Size / 2);
            Rectangle thisRec = new Rectangle((this.m_center - (this.Size / 2)).ToPoint(),this.Size.ToPoint());
            Rectangle othRec = new Rectangle((other.m_center - (other.Size / 2)).ToPoint(), other.Size.ToPoint());

            return thisRec.Intersects(othRec);
        }

        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }

    }
}
