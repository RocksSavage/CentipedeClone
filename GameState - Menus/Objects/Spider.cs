using Microsoft.Xna.Framework;
using System;

namespace CS5410.Objects
{
    public class Spider : AnimatedSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        bool south = false;
        bool west = false;
        public Spider(Vector2 size, Vector2 center, GameAgents gameAgents, float speed, bool west) : base(size, center)
        { 
            m_speed = speed;
            m_gameAgents = gameAgents;
            this.west = west;
        }
        public void update(GameTime gameTime)
        {
            if (m_center.Y > (gameBoard.Height - gameBoard.CellHeight - gameBoard.HalfCellWidth))
                south = true;

            else if (m_center.Y < (gameBoard.ShroomRows * gameBoard.CellHeight))
                south = false;



            var nextspc = new Vector2(
                (this.m_center.X + ((west ? -1 : 1) * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds)),
                (this.m_center.Y + ((south ? 1 : -1) * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                );


            this.moveDiagonal(gameTime, nextspc);
        }
        public void moveDiagonal(GameTime gameTime, Vector2 nextspc)
        {
            var spriteExample = new AnimatedSprite(this.Size, nextspc);

            if (nextspc.Y > (gameBoard.Height + gameBoard.CellHeight))
            {
                m_gameAgents.m_rmSpiderList.Add(this);
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
            m_center = nextspc;
        }
    }
}
