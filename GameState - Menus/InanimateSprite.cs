using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CS5410
{
    public class InanimatedSprite
    {
        private Texture2D m_spriteSheet;

        private TimeSpan m_animationTime;
        private int m_subImageIndex;
        private int m_subImageWidth;
        private int m_spriteLvlId;
        private readonly int m_subImageHeight = 8;

        public InanimatedSprite(Texture2D spriteSheet, int spriteWidth, int spriteLvlId)
        {
            this.m_spriteSheet = spriteSheet;
            this.m_subImageWidth = spriteWidth;
            this.m_spriteLvlId = spriteLvlId;

        }

        public void update(GameTime gameTime)
        {
            //m_animationTime += gameTime.ElapsedGameTime;
            //if (m_animationTime.TotalMilliseconds >= m_spriteTime[m_subImageIndex])
            //{
            //    m_animationTime -= TimeSpan.FromMilliseconds(m_spriteTime[m_subImageIndex]);
            //    m_subImageIndex++;
            //    m_subImageIndex = m_subImageIndex % m_spriteTime.Length;
            //}

        }

        public void draw(SpriteBatch spriteBatch, Objects.InanimateSprite model)
        {
            spriteBatch.Draw(
                m_spriteSheet,
                new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                new Rectangle(m_subImageIndex * model.damage, m_spriteLvlId * (m_subImageHeight + 1), m_subImageWidth, m_subImageHeight),
                Color.White);
        }
    }
}
