using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Player : InanimateSprite
    {
        private float m_speed;
        private GamePlayView m_gameView;
        public Player(Vector2 size, Vector2 center, GamePlayView game, float speed) : base(size, center)
        { 
            m_speed = speed;
            m_gameView = game;
        }

        public void moveDown(GameTime gameTime)
        {
            m_center.Y += m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void moveUp(GameTime gameTime)
        {
            //TODO 
            var nextspc = new Vector2(this.m_center.X,m_center.Y - m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Shrooms collider = m_gameView.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null)
                m_center.Y = nextspc.Y;
        }

        public void moveLeft(GameTime gameTime)
        {
            //TODO
        }

        public void moveRight(GameTime gameTime)
        {
            //TODO
        }
        public void fire(GameTime gameTime)
        {
            //TODO
        }
    }
}
