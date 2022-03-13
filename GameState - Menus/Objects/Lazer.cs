using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Lazer : InanimateSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        public Lazer(Vector2 size, Vector2 center, GameAgents gameAgents, float speed) : base(size, center)
        { 
            m_speed = speed;
            m_gameAgents = gameAgents;
        }
        public void update(GameTime gameTime)
        {
            this.moveUp(gameTime);
        }
        public void moveUp(GameTime gameTime)
        {
            //TODO 
            var nextspc = new Vector2(this.m_center.X,m_center.Y - m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Shrooms collider = m_gameAgents.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null)
                m_center.Y = nextspc.Y;
        }
    }
}
