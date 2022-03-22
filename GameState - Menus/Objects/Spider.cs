using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Spider : AnimatedSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        bool south = false;
        bool west = false;
        public Spider(Vector2 size, Vector2 center, GameAgents gameAgents, float speed, bool west, bool south) : base(size, center)
        {
            m_speed = speed;
            m_gameAgents = gameAgents;
            this.west = west;
            this.south = south;
        }
        public void update(GameTime gameTime)
        {
            if (m_center.Y > gameBoard.Height)
                south = false;
            else if (m_center.Y < (500))
                south = true;

            var nextspc = new Vector2(
                (this.m_center.X + ((west ? -1 : 1) * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds)),
                (this.m_center.Y + ((south ? 1 : -1) * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                );


            this.moveDiagonal(gameTime, nextspc);
        }
        public void moveDiagonal(GameTime gameTime, Vector2 nextspc)
        {
            var spriteExample = new AnimatedSprite(this.Size, nextspc);

            if (nextspc.X > (gameBoard.Right) || nextspc.X < gameBoard.Left)
            {
                m_gameAgents.m_rmSpiderList.Add(this);
            }

            if (this.collide(m_gameAgents.m_player))
            {
                m_gameAgents.m_player.Lives--;
            }

            Shrooms collider = m_gameAgents.shroomCollision(spriteExample);

            if (collider != null)
            {
                m_gameAgents.m_rmShroomsList.Add(collider);
            }

            m_center = nextspc;
        }
    }
}
