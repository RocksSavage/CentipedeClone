using Microsoft.Xna.Framework;
using System;

namespace CS5410.Objects
{
    public class AnimatedSprite
    {
        private readonly Vector2 m_size;
        protected Vector2 m_center;
        protected float m_rotation = 0;

        // uncomment out these lines to keep going with the madnes
        //GamePlayView m_gameView;

        public AnimatedSprite(Vector2 size, Vector2 center/*, GamePlayView game*/)
        {
            m_size = size;
            m_center = center;
            //m_gameView = game;
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
            // TODO

            return false;
        }

        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }

        public static explicit operator AnimatedSprite(Shrooms v)
        {
            throw new NotImplementedException();
        }
    }
}
