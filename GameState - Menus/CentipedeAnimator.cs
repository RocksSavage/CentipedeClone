using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CS5410
{
    public class CentipedeAnimator
    {
        private Texture2D m_spriteSheet;
        private int[] m_spriteTime;

        private TimeSpan m_animationTime;
        private int m_subImageIndex;
        private int m_subImageWidth;
        private int m_subImageHeight;
        private int m_spriteLvlId;

        public CentipedeAnimator(Texture2D spriteSheet, int[] spriteTime, int spriteLvlId)
        {
            this.m_spriteSheet = spriteSheet;
            this.m_spriteTime = spriteTime;
            m_spriteLvlId = spriteLvlId;

            m_subImageWidth = 15;
            m_subImageHeight = 8;
        }

        public void update(GameTime gameTime)
        {
            m_animationTime += gameTime.ElapsedGameTime;
            if (m_animationTime.TotalMilliseconds >= m_spriteTime[m_subImageIndex])
            {
                m_animationTime -= TimeSpan.FromMilliseconds(m_spriteTime[m_subImageIndex]);
                m_subImageIndex++;
                m_subImageIndex = m_subImageIndex % m_spriteTime.Length;
            }
        }

        public void draw(SpriteBatch spriteBatch, Objects.Centipede model)
        {
            if (model.isHead)
            {
                spriteBatch.Draw(
                m_spriteSheet,
                new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                new Rectangle(m_subImageIndex * m_subImageWidth, (m_spriteLvlId - 2 + (model.west ? 1 : 0)) * 9, m_subImageWidth, m_subImageHeight), // Source sub-texture
                Color.White);
            }
            else
            {
                spriteBatch.Draw(
                    m_spriteSheet,
                    new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                    new Rectangle(m_subImageIndex * m_subImageWidth, (m_spriteLvlId + (model.west ? -1 : 0))* 9, m_subImageWidth, m_subImageHeight), // Source sub-texture
                    Color.White);
            }
        }
    }
}
