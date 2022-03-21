using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CS5410
{
	public class GameAgents
	{
        public List<Objects.Shrooms> m_shroomsList = new List<Objects.Shrooms>();
        public List<Objects.Shrooms> m_rmShroomsList = new List<Objects.Shrooms>();
		public List<Objects.Lazer> m_lazerList = new List<Objects.Lazer>();
        public List<Objects.Lazer> m_rmLazerList = new List<Objects.Lazer>();
        public List<Objects.Player> m_playerList = new List<Objects.Player>();
        public List<Objects.Player> m_rmPlayerList = new List<Objects.Player>();
        public List<Objects.Flea> m_fleaList = new List<Objects.Flea>();
        public List<Objects.Flea> m_rmFleaList = new List<Objects.Flea>();
        public List<Objects.Spider> m_spiderList = new List<Objects.Spider>();
        public List<Objects.Spider> m_rmSpiderList = new List<Objects.Spider>(); 
        public List<Objects.Scorpion> m_scorpionList = new List<Objects.Scorpion>();
        public List<Objects.Scorpion> m_rmScorpionList = new List<Objects.Scorpion>(); 

        public int m_score = 0;
        public Objects.Player m_player;

        GamePlayView m_gamePlayView;

		public GameAgents(GamePlayView gamePlayView)
        {
            m_gamePlayView = gamePlayView;
        }
        public void addlazer(Vector2 center)
        {
            // Note to Trent
            // something here is wrong. 
            // The lazer spawns in the complete wrong location. (in the deep negatives on the Y axis)
            // fix that. 

            m_lazerList.Add(
                new Objects.Lazer(
                    new Vector2(gameBoard.CellWidth, gameBoard.CellHeight),
                    center,
                    this,
                    250f,
                    gameBoard.CellHeight)
                );
        }
        public void unregisterAnimatedSprites()
        {
            

            m_playerList.RemoveAll(item => m_rmPlayerList.Contains(item));
            m_shroomsList.RemoveAll(item => m_rmShroomsList.Contains(item));
            m_lazerList.RemoveAll(item => m_rmLazerList.Contains(item));
            m_fleaList.RemoveAll(item => m_rmFleaList.Contains(item));
            m_spiderList.RemoveAll(item => m_rmSpiderList.Contains(item));
            m_scorpionList.RemoveAll(item => m_rmScorpionList.Contains(item));

            // clear old lists
            m_rmPlayerList.Clear();
            m_rmShroomsList.Clear();
            m_rmLazerList.Clear();
            m_rmFleaList.Clear();
            m_rmSpiderList.Clear();
            m_rmScorpionList.Clear();
        }
        /// <summary>
        /// Currently only Lazer uses this so its safe to use for points tracking. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool animatedSpriteCollisionAndDeath(Objects.AnimatedSprite model)
        {
            foreach (Objects.Flea ee in m_fleaList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                {
                    m_rmFleaList.Add(ee);
                    m_score += 300;
                    return true;
                }
            }

            foreach (Objects.Spider ee in m_spiderList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                {
                    m_rmSpiderList.Add(ee);
                    m_score += 25;
                    return true;
                }
            }

            foreach (Objects.Scorpion ee in m_scorpionList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                {
                    m_rmScorpionList.Add(ee);
                    m_score += 900;
                    return true;
                }
            }

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
        public void triggerGmover()
        {
            m_gamePlayView.m_gmover = true;
        }
    }
}
