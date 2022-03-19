using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CS5410
{
    public class AnimatedSprite
    {
        private Texture2D m_spriteSheet;
        private int[] m_spriteTime;

        private TimeSpan m_animationTime;
        private int m_subImageIndex;
        private int m_subImageWidth;
        private int m_subImageHeight;
        private int m_spriteLvlId;

        public AnimatedSprite(Texture2D spriteSheet, int[] spriteTime, int spriteLvlId)
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

        public void draw(SpriteBatch spriteBatch, Objects.AnimatedSprite model)
        {
            //spriteBatch.Draw(
            //    m_spriteSheet,
            //    new Rectangle((int)model.Center.X - m_subImageWidth / 2, (int)model.Center.Y - m_spriteSheet.Height / 2, (int)model.Size.X, (int)model.Size.Y), // Destination rectangle
            //    new Rectangle(m_subImageIndex * m_subImageWidth, m_spriteLvlId * 9, m_subImageWidth, m_subImageHeight), // Source sub-texture
            //    Color.White,
            //    0f,//model.Rotation, // Angular rotation
            //                    //new Vector2(m_subImageWidth / 2, m_subImageHeight / 2), // Center point of rotation
            //    new Vector2(250, 250),
            //    SpriteEffects.None,
            //    0);
            spriteBatch.Draw(
                m_spriteSheet,
                new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                new Rectangle(m_subImageIndex * m_subImageWidth, m_spriteLvlId * 9, m_subImageWidth, m_subImageHeight), // Source sub-texture
                Color.White);
        }
    }
}
