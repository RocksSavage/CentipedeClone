using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CS5410.Objects
{
    public class Centipede : AnimatedSprite
    {
        private float m_speed;
        private GameAgents m_gameAgents;
        public bool isHead;
        public bool isPoisoned;
        List<Centipede> train;
        public bool west;
        public bool north;
        AnimatedSprite exceptionPos;
        public Centipede(Vector2 size, Vector2 center, GameAgents gameAgents, float speed, bool west, bool isHead = false, bool north = false) : base(size, center)
        {
            m_speed = speed;
            m_gameAgents = gameAgents;
            this.west = west;
            this.north = north;
            exceptionPos = null;
            isPoisoned = false;
        }
        public List<Centipede> layTrain(Vector2 startPos)
        {
            //TODO
            m_gameAgents.m_centipedeList.Add(
                new Centipede(
                    new Vector2(gameBoard.CellWidth, gameBoard.CellHeight),
                    startPos,
                    m_gameAgents,
                    150f,
                    false));
            return null;
        }
        public void update(GameTime gameTime)
        {

            var nextspc = new Vector2(
                (this.m_center.X + ((west ? -1 : 1) * m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds)),
                (this.m_center.Y)
                );

           

            this.move(gameTime, nextspc);
        }
        public void move(GameTime gameTime, Vector2 nextspc)
        {
            var spriteExample = new AnimatedSprite(this.Size, nextspc);

            Shrooms collider = m_gameAgents.shroomCollision(spriteExample, exceptionPos);

            if (collider != null && collider.isPoisoned)
                this.isPoisoned=true;

            if (north && 
                (m_center.Y < gameBoard.ShroomRowSpace)) // hits glass ceiling
                north = false;

            if ((m_center.Y > gameBoard.Height)) // hits floor
            {
                north = true;
                isPoisoned = false;
            }

            if (isPoisoned || nextspc.X < gameBoard.Left || nextspc.X > gameBoard.Right || collider != null)
            {
                nextspc.Y += (north ? -1 : 1)*gameBoard.CellHeight;
                nextspc.X = m_center.X;
                this.west = !west;
                
                exceptionPos = new AnimatedSprite(this.Size, nextspc);
            }

            if (this.collide(m_gameAgents.m_player))
            {
                m_gameAgents.m_player.Lives--;
            }
            //    if (this.collide(m_gameAgents.m_player))
            //{
            //    m_gameAgents.m_player.Lives--;
            //}


            //    if (
            //        ((nextspc.X - (gameBoard.CellWidth / 2)) % gameBoard.CellWidth < 5)) //only on grid spaces
            //    {
            //        Shrooms collider = m_gameAgents.shroomCollision(spriteExample);
            //        Random rmd = new Random();

            //        if (collider != null)
            //        {
            //            collider.isPoisoned = true;
            //        }
            //    }


            m_center = nextspc;
        }

    }
}
