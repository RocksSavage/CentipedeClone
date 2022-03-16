using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CS5410
{
	public class GameAgents
	{
        public List<Objects.Shrooms> m_shroomsList = new List<Objects.Shrooms>();
		public List<Objects.Lazer> m_lazerList = new List<Objects.Lazer>();
        public List<Objects.Lazer> m_rmLazerList = new List<Objects.Lazer>();

        public Objects.Player m_player;

        private int m_cellHeight;
        private int m_cellWidth;

		public GameAgents(int cellHeight, int cellWidth)
        {
            m_cellHeight = cellHeight;
            m_cellWidth = cellWidth;
        }
        public void addlazer(Vector2 center)
        {
            // Note to Trent
            // something here is wrong. 
            // The lazer spawns in the complete wrong location. (in the deep negatives on the Y axis)
            // fix that. 

            m_lazerList.Add(
                new Objects.Lazer(
                    new Vector2(m_cellWidth, m_cellHeight),
                    center/* - (new Vector2(center.X, center.Y - (m_cellHeight / 2)))*/,
                    this,
                    150f,
                    m_cellHeight)
                );
        }
        public void unregisterAnimatedSprites()
        {
            m_lazerList.RemoveAll(item => m_rmLazerList.Contains(item));
        }
        public bool animatedSpriteCollisionAndDeath(Objects.AnimatedSprite model)
        {
            foreach(Objects.Shrooms ee in m_shroomsList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                {
                    ee.Damage = ee.Damage++;
                    return true;
                }
            }
            //foreach(Objects. ee in m_lazerList)
            //{
            //}
            return false;
        }
        public Objects.Shrooms shroomCollision(Objects.AnimatedSprite model)
        {
            foreach (Objects.Shrooms ee in m_shroomsList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                    return ee;
            }
            return null;
        }
        public void playerCollision(Objects.AnimatedSprite model)
        {
            //TODO
        }
    }
}
