using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CS5410
{
    public class LazerAnimator
    {
        private Texture2D m_spriteSheet;

        private int m_subImageIndex;
        private int m_subImageWidth;
        private int m_spriteLvlId;
        private readonly int m_subImageHeight = 8;

        public LazerAnimator(Texture2D spriteSheet, int spriteWidth, int spriteLvlId)
        {
            this.m_spriteSheet = spriteSheet;
            this.m_subImageWidth = spriteWidth;
            this.m_spriteLvlId = spriteLvlId;

        }

        public void update(GameTime gameTime)
        {
        }

        public void draw(SpriteBatch spriteBatch, Objects.InanimateSprite model)
        {
            spriteBatch.Draw(
                m_spriteSheet,
                new Rectangle((model.Center).ToPoint(), new Point(gameBoard.CellWidth / 8, gameBoard.CellHeight - 5)),//new Rectangle((model.Center - (model.Size / 2)).ToPoint(), model.Size.ToPoint()),
                new Rectangle(7, 119, 1, 6),//new Rectangle(m_subImageIndex /** model.Damage*/, m_spriteLvlId * (m_subImageHeight + 1), m_subImageWidth, m_subImageHeight),
                Color.White);
        }
    }
}
