using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Lazer : InanimateSprite
    {
        private float m_speed;
        private int m_cellHeight;
        private GameAgents m_gameAgents;
        public Lazer(Vector2 size, Vector2 center, GameAgents gameAgents, float speed, int cellHeight) : base(size, center)
        { 
            m_speed = speed;
            m_gameAgents = gameAgents;
            m_cellHeight = cellHeight;
        }
        public void update(GameTime gameTime)
        {
            this.moveUp(gameTime);
        }
        public void moveUp(GameTime gameTime)
        {
            //TODO 
            var nextspc = new Vector2(this.m_center.X,m_center.Y - m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            var spriteExample = new AnimatedSprite(this.Size, nextspc);
            Shrooms collider = m_gameAgents.shroomCollision(spriteExample);
            
            if (nextspc.Y < (0 - m_cellHeight) ||
                m_gameAgents.animatedSpriteCollisionAndDeath(spriteExample))
            {
                if (m_gameAgents.m_lazerList.Contains(this))
                    this.m_gameAgents.m_rmLazerList.Add(this);
                return;
            }

            if (collider != null)
            {
                collider.Damage += 1;
                m_gameAgents.m_score += 1;
                if (m_gameAgents.m_lazerList.Contains(this))
                    this.m_gameAgents.m_rmLazerList.Add(this);
                return;
            }
            m_center.Y = nextspc.Y;

        }
    }
}
