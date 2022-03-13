using System;
using System.Collections.Generic;

namespace CS5410
{
	public class GameAgents
	{
        public List<Objects.Shrooms> m_shroomsList = new List<Objects.Shrooms>();
		//public List<Objects.Lazer> m_lazerList = new List<Objects.Lazer>();
        public Objects.Player m_player;

		public GameAgents()
        {
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
