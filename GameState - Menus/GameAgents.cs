﻿using Microsoft.Xna.Framework;
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
            // give player points
            m_score += 1 * m_rmShroomsList.Count;
            m_score += 25 * m_rmFleaList.Count;

            m_playerList.RemoveAll(item => m_rmPlayerList.Contains(item));
            m_shroomsList.RemoveAll(item => m_rmShroomsList.Contains(item));
            m_lazerList.RemoveAll(item => m_rmLazerList.Contains(item));
            m_fleaList.RemoveAll(item => m_rmFleaList.Contains(item));

            // clear old lists
            m_rmPlayerList.Clear();
            m_rmShroomsList.Clear();
            m_rmLazerList.Clear();
            m_rmFleaList.Clear();
        }
        public bool animatedSpriteCollisionAndDeath(Objects.AnimatedSprite model)
        {

            foreach (Objects.Flea ee in m_fleaList)
            {
                if (model.collide((Objects.AnimatedSprite)ee))
                {
                    m_rmFleaList.Add(ee);
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
