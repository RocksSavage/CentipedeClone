using Microsoft.Xna.Framework;
using System;

namespace CS5410.Objects
{
    public class Flea : AnimatedSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        public Flea(Vector2 size, Vector2 center, GameAgents gameAgents, float speed) : base(size, center)
        {
            m_speed = speed;
            m_gameAgents = gameAgents;
        }
        public void update(GameTime gameTime)
        {
            this.moveDown(gameTime);
        }
        public void moveDown(GameTime gameTime)
        {
            var nextspc = new Vector2(this.m_center.X, m_center.Y + m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            var spriteExample = new AnimatedSprite(this.Size, nextspc);

            if (nextspc.Y > (gameBoard.Height + gameBoard.CellHeight))
            {
                m_gameAgents.m_rmFleaList.Add(this);
            }

            if (this.collide(m_gameAgents.m_player))
            {
                m_gameAgents.m_player.Lives--;
            }


            if (
                ((nextspc.Y - (gameBoard.CellHeight / 2)) % gameBoard.CellHeight < 5) && //only on grid spaces
                (nextspc.Y < gameBoard.ShroomRows * gameBoard.CellHeight))
            {
                Shrooms collider = m_gameAgents.shroomCollision(spriteExample);
                Random rmd = new Random();

                if (collider == null && rmd.Next(5) < 2)
                {
                    m_gameAgents.m_shroomsList.Add(
                        new Shrooms(
                            new Vector2(gameBoard.CellWidth, gameBoard.CellHeight),
                            m_center,
                            m_gameAgents)
                        );
                }
            }


            m_center.Y = nextspc.Y;

        }
    }
}
