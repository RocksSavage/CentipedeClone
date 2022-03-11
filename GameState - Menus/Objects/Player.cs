﻿using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Player : InanimateSprite
    {
        private float m_speed;
        public Player(Vector2 size, Vector2 center, float speed) : base(size, center)
        { m_speed = speed; }

        public void moveDown(GameTime gameTime)
        {
            //dsf
        }
        public void moveUp(GameTime gameTime)
        {
            //TODO
            m_center.Y += (Center.Y * m_speed);
        }

        public void moveLeft(GameTime gameTime)
        {
            //TODO
        }

        public void moveRight(GameTime gameTime)
        {
            //TODO
        }
    }
}