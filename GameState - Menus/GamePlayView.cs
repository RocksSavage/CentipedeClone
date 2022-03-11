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
        private InanimatedSprite m_shroomAnimator;

        int m_cellQuanityX = 30;
        int m_cellQuanityY = 20;
        int m_gameBoardOriginY;
        int m_gameBoardCellWidth;
        int m_gameBoardOriginX;
        int m_gameBoardCellHeight;
        


        public GamePlayView(GraphicsDeviceManager graphics)
        {
            // define how large the game board is
            int m_gameBoardWidth = 896;
            int m_gameBoardHeight = 606;

            this.m_graphics = graphics;
            m_gameBoardCellWidth = m_gameBoardWidth / m_cellQuanityX;
            m_gameBoardOriginX = (m_graphics.PreferredBackBufferWidth / 2) - (m_gameBoardWidth / 2);
            m_gameBoardCellHeight = m_gameBoardHeight / m_cellQuanityY;
            m_gameBoardOriginY = m_gameBoardCellHeight;
        }
        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");

            //create and place mushrooms
            Random rmd = new Random();
            for (int i = 0; i < m_cellQuanityX; i++)
            {
                for (int j = 0; j < m_cellQuanityY; j++)
                {
                    if (rmd.Next(35) == 1 ) // 1/35 chance of mushroom on each square. 
                    {
                        m_shroomsList.Add(
                            new Objects.Shrooms(
                                new Vector2(m_gameBoardCellWidth, m_gameBoardCellHeight), //size
                                new Vector2(m_gameBoardOriginX + (m_gameBoardCellWidth / 2) + (m_gameBoardCellWidth * i), m_gameBoardOriginY + (m_gameBoardCellHeight / 2) + (m_gameBoardCellHeight * j))  //location
                                )
                            );

                    }
                }

            }


            //m_shroomsList.Add(
            //    new Objects.Shrooms(
            //        new Vector2(m_gameBoardCellWidth, m_gameBoardCellWidth),
            //        new Vector2(m_gameBoardOriginX + 16, m_gameBoardOriginY + 16)
            //        )
            //    );

            // numbers pertain to the subtextures in the spritesheet
            m_shroomAnimator = new InanimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                8,
                14
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
