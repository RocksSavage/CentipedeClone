using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Shrooms : InanimateSprite
    {
        GameAgents m_gameAgents;
        private int m_damage;
        public bool isPoisoned;
        public Shrooms(Vector2 size, Vector2 center, GameAgents gameAgents, bool poisoned = false) : base(size, center)
        {
            m_gameAgents = gameAgents;
            isPoisoned = poisoned;
        }

        public int Damage
        {
            get { return m_damage; }
            set
            {
                m_damage = value;
                if (m_damage == 4)
                    m_gameAgents.m_rmShroomsList.Add(this);
            }
        }

    }
}
