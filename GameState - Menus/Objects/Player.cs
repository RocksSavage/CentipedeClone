using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Player : InanimateSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        private int m_livesRemaining = 3;
        private int m_originX;
        private int m_screenWidth;
        private int m_cellHeight;
        private int m_screenHeight;
        public Player(Vector2 size, Vector2 center, GameAgents game, float speed, int originX, int screenWidth, int cellHeight, int screenHeight) : base(size, center)
        { 
            m_speed = speed;
            m_gameAgents = game;
            m_originX = originX;
            m_screenWidth = screenWidth;
            m_cellHeight = cellHeight;
            m_screenHeight = screenHeight;
        }

        public void moveDown(GameTime gameTime)
        {
            var nextspc = new Vector2(this.m_center.X, m_center.Y + m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Shrooms collider = m_gameAgents.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null && nextspc.Y < gameBoard.Bottom)
                m_center.Y = nextspc.Y;
        }
        public void moveUp(GameTime gameTime)
        {
            var nextspc = new Vector2(this.m_center.X,m_center.Y - m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Shrooms collider = m_gameAgents.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null && nextspc.Y > gameBoard.PlayerBarrier)
                m_center.Y = nextspc.Y;
        }

        public void moveLeft(GameTime gameTime)
        {
            var nextspc = new Vector2(this.m_center.X - m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds, m_center.Y);
            Shrooms collider = m_gameAgents.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null && nextspc.X > gameBoard.Left)
                m_center.X = nextspc.X;
        }

        public void moveRight(GameTime gameTime)
        {
            var nextspc = new Vector2(this.m_center.X + m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds, m_center.Y);
            Shrooms collider = m_gameAgents.shroomCollision(new AnimatedSprite(this.Size, nextspc));
            if (collider == null && nextspc.X < gameBoard.Right)
                m_center.X = nextspc.X;
        }
        public void fire(GameTime gameTime)
        {
            m_gameAgents.addlazer(Center);
        }

        public int Lives
        {
            get { return this.m_livesRemaining; }
            set { 
                this.m_livesRemaining = value;
                if (this.m_livesRemaining == 0)
                    m_gameAgents.m_rmPlayerList.Add(this);
            }
        }
    }
}
