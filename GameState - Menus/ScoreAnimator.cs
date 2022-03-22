using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CS5410
{
    public class ScoreAnimator
    {
        private Texture2D m_spriteSheet;

        private Tuple<int, int> origin;
        private Vector2 m_origin;
        private int m_gameBoardOriginx;
        private int m_gameBoardHeight;

        public ScoreAnimator(Texture2D spriteSheet, int gameBoardOriginX, int gameBoardHeight)
        {
            this.m_spriteSheet = spriteSheet;
            m_gameBoardHeight = gameBoardHeight;
            m_gameBoardOriginx = gameBoardOriginX;
            m_origin = new Vector2((int)gameBoardOriginX, (0));
        }

        public void update(GameTime gameTime)
        {
            //
        }

        public void draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont font, int remainingLives, int score)
        {
            // Print Title
            spriteBatch.DrawString(
                font,
                score.ToString(),
                m_origin,
                Color.Green);

            int cursor = gameBoard.Right - gameBoard.CellWidth;
            for (int i = 0; i < remainingLives; i++)
            {
                spriteBatch.Draw(
                    m_spriteSheet,
                    new Rectangle(cursor, 0, gameBoard.CellWidth, gameBoard.CellHeight),
                    new Rectangle(3, 9, 8, 8),
                    Color.White);
                cursor -= gameBoard.CellWidth + 2;
            }
        }
    }
}
