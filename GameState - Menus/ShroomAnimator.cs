using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS5410
{
    public class ShroomAnimator
    {
        private Texture2D m_spriteSheet;

        private int m_subImageIndex;
        private int m_subImageWidth;
        private int m_spriteLvlId;
        private readonly int m_subImageHeight = 8;

        public ShroomAnimator(Texture2D spriteSheet, int spriteWidth, int spriteLvlId)
        {
            this.m_spriteSheet = spriteSheet;
            this.m_subImageWidth = spriteWidth;
            this.m_spriteLvlId = spriteLvlId;

        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(SpriteBatch spriteBatch, Objects.Shrooms model)
        {
            if (model.isPoisoned)
            {
                spriteBatch.Draw(
                m_spriteSheet,
                new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                new Rectangle(m_subImageWidth * model.Damage, (m_spriteLvlId + 1) * (m_subImageHeight + 1), m_subImageWidth, m_subImageHeight),
                Color.White);
            }
            else
            {
                spriteBatch.Draw(
                    m_spriteSheet,
                    new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                    new Rectangle(m_subImageWidth * model.Damage, m_spriteLvlId * (m_subImageHeight + 1), m_subImageWidth, m_subImageHeight),
                    Color.White);
            }
        }
    }
}
