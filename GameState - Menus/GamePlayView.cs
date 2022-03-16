using CS5410.Input;
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

        GameAgents m_gameAgents;
        private ShroomAnimator m_shroomAnimator;
        private InanimatedSprite m_playerAnimator;
        private InanimatedSprite m_lazerAnimator;

        int m_cellQuanityX = 30;
        int m_cellQuanityY = 20;
        int m_gameBoardOriginY;
        int m_gameBoardCellWidth;
        int m_gameBoardOriginX;
        int m_gameBoardCellHeight;
        int m_gameBoardCenterX;
        int m_gameBoardHeight;
        int m_gameBoardCellWidth2; // for the larger sprite types

        private KeyboardInput m_inputKeyboard = new KeyboardInput();


        public GamePlayView(GraphicsDeviceManager graphics)
        {
            // define how large the game board is
            int m_gameBoardWidth = 896;
            m_gameBoardHeight = 606;

            this.m_graphics = graphics;
            m_gameBoardCellWidth = m_gameBoardWidth / m_cellQuanityX;
            m_gameBoardCenterX = (m_graphics.PreferredBackBufferWidth / 2);
            m_gameBoardOriginX = m_gameBoardCenterX - (m_gameBoardWidth / 2);
            m_gameBoardCellHeight = m_gameBoardHeight / m_cellQuanityY;
            m_gameBoardOriginY = m_gameBoardCellHeight;
            m_gameBoardCellWidth2 = (int)(m_gameBoardCellWidth * 1.8);

            m_gameAgents = new GameAgents(m_gameBoardCellWidth, m_gameBoardCellHeight);
        }
        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");

            //create and place mushrooms
            Random rmd = new Random();
            for (int i = 0; i < m_cellQuanityX; i++)
            {
                for (int j = 0; j < m_cellQuanityY - 1; j++) // do not let shrooms spawn on bottom row for player
                {
                    if (/*rmd.Next(35) == 1 */false) // 1/35 chance of mushroom on each square. 
                    {
                        m_gameAgents.m_shroomsList.Add(
                            new Objects.Shrooms(
                                new Vector2(m_gameBoardCellWidth, m_gameBoardCellHeight), //size
                                new Vector2(m_gameBoardOriginX + (m_gameBoardCellWidth / 2) + (m_gameBoardCellWidth * i), m_gameBoardOriginY + (m_gameBoardCellHeight / 2) + (m_gameBoardCellHeight * j)),  //location
                                m_gameAgents
                                )
                            );
                    }
                }
            }

            // deleteLATER
            m_gameAgents.m_shroomsList.Add(
                new Objects.Shrooms(
                    new Vector2(m_gameBoardCellWidth, m_gameBoardCellHeight), //size
                    new Vector2(m_gameBoardOriginX + (m_gameBoardCellWidth / 2) + (m_gameBoardCellWidth * 15), m_gameBoardOriginY + (m_gameBoardCellHeight / 2) + (m_gameBoardCellHeight * 15)),  //location
                    m_gameAgents
                    )
                );

            // numbers pertain to the subtextures in the spritesheet
            m_shroomAnimator = new ShroomAnimator(
                contentManager.Load<Texture2D>("spritesheet-general"),
                8,
                14);

            // create lazer animator
            m_lazerAnimator = new InanimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                15, //15
                13);//13

            // create and place player
            m_gameAgents.m_player = new Objects.Player(
                new Vector2(m_gameBoardCellWidth2,m_gameBoardCellHeight),
                new Vector2(m_gameBoardCenterX, m_gameBoardHeight - (m_gameBoardCellHeight / 2) ),
                this.m_gameAgents,
                100f
                );

            m_playerAnimator = new InanimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                15,
                1);

            // initialize controls
            updateControls(true);
        }
        public void updateControls(bool onstartup = false)
        {
            if (ControllerState.dirty == true || onstartup)
            {
                m_inputKeyboard.empty();

                // Setup input handlers
                m_inputKeyboard.registerCommand(ControllerState.MoveLeft, false, new InputDeviceHelper.CommandDelegate((gameTime, value) => { m_gameAgents.m_player.moveLeft(gameTime); }));
                m_inputKeyboard.registerCommand(ControllerState.MoveRight, false, new InputDeviceHelper.CommandDelegate((gameTime, value) => { m_gameAgents.m_player.moveRight(gameTime); }));
                m_inputKeyboard.registerCommand(ControllerState.MoveUp, false, new InputDeviceHelper.CommandDelegate((gameTime, value) => { m_gameAgents.m_player.moveUp(gameTime); }));
                m_inputKeyboard.registerCommand(ControllerState.MoveDown, false, new InputDeviceHelper.CommandDelegate((gameTime, value) => { m_gameAgents.m_player.moveDown(gameTime); }));
                m_inputKeyboard.registerCommand(ControllerState.Fire, true, new InputDeviceHelper.CommandDelegate((gameTime, value) => { m_gameAgents.m_player.fire(gameTime); }));
            }
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
            // updating the controls like a fool. 
            updateControls();
            m_inputKeyboard.Update(gameTime);

            m_gameAgents.unregisterAnimatedSprites();

            foreach (Objects.Lazer lazer in m_gameAgents.m_lazerList)
            {
                lazer.update(gameTime);
            }
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            foreach (Objects.Shrooms shroom in m_gameAgents.m_shroomsList)
            {
                m_shroomAnimator.draw(m_spriteBatch, shroom);
            }
            
            foreach (Objects.Lazer lazer in m_gameAgents.m_lazerList)
            {
                m_lazerAnimator.draw(m_spriteBatch, lazer);
            }
            
            m_playerAnimator.draw(m_spriteBatch, m_gameAgents.m_player);

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
