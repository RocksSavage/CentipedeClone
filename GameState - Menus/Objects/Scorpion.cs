using Microsoft.Xna.Framework;
using System;

namespace CS5410.Objects
{
    public class Scorpion : AnimatedSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        bool west = false;
        public Scorpion(Vector2 size, Vector2 center, GameAgents gameAgents, float speed, bool west) : base(size, center)
        {
            m_speed = speed;
            m_gameAgents = gameAgents;
            this.west = west;
        }
        public void update(GameTime gameTime)
        {

            var nextspc = new Vector2(
                (this.m_center.X + (-1 * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds)),
                (this.m_center.Y)
                );


            this.move(gameTime, nextspc);
        }
        public void move(GameTime gameTime, Vector2 nextspc)
        {
            var spriteExample = new AnimatedSprite(this.Size, nextspc);

            if (nextspc.X < gameBoard.Left || nextspc.X > gameBoard.Right)
            {
                m_gameAgents.m_rmScorpionList.Add(this);
            }

            if (this.collide(m_gameAgents.m_player))
            {
                m_gameAgents.m_player.Lives--;
            }


            if (
                ((nextspc.X - (gameBoard.CellWidth / 2)) % gameBoard.CellWidth < 5)) //only on grid spaces
            {
                Shrooms collider = m_gameAgents.shroomCollision(spriteExample);
                Random rmd = new Random();

                if (collider != null)
                {
                    collider.isPoisoned = true;
                }
            }


            m_center = nextspc;
        }
    }
}
