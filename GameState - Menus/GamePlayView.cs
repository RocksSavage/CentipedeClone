using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CS5410
{
    public class GamePlayView : GameStateView
    {
        bool m_gmover = false;
        private SpriteFont m_font;
        private const string GMOVER = "GAME OVER";

        List<Objects.Shrooms> m_shroomsList = new List<Objects.Shrooms>();
        private AnimatedSprite m_shroomAnimator;

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");

            //create and place mushrooms
            Random rmd = new Random();
            m_shroomsList.Add(
                new Objects.Shrooms(
                    new Vector2(75, 75),
                    new Vector2(150, 200)
                    )
                );

            m_shroomAnimator = new AnimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                new int[] { 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 }
            );

        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.GamePlay;
        }
        public override void update(GameTime gameTime)
        {
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            foreach (Objects.Shrooms shroom in m_shroomsList)
            {
                m_shroomAnimator.draw(m_spriteBatch, shroom);
            }

            if (m_gmover)
            {
                Vector2 stringSize = m_font.MeasureString(GMOVER);
                m_spriteBatch.DrawString(
                    m_font,
                    GMOVER,
                    new Vector2(
                        m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,
                        m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y),
                    Color.Yellow
                    );
            }
            m_spriteBatch.End();
        }

    }
}
