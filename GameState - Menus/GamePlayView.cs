using CS5410.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CS5410
{
    public class GamePlayView : GameStateView
    {
        public bool m_gmover = false;
        private SpriteFont m_font;
        private const string GMOVER = "GAME OVER";

        GameAgents m_gameAgents;
        private ShroomAnimator m_shroomAnimator;
        private InanimatedSprite m_playerAnimator;
        private ScoreAnimator m_scoreAnimator;
        private InanimatedSprite m_lazerAnimator;
        private AnimatedSprite m_fleaAnimator;
        private AnimatedSprite m_spiderAnimator;
        private AnimatedSprite m_scorpionAnimator;

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


        public void initialize()
        {
            // Initalize is called after loadContent. What is the point of this function? 
        }
        public override void loadContent(ContentManager contentManager)
        {
            // define how large the game board is
            // GRADER ↓
            int m_gameBoardWidth = 896;
            int m_gameBoardHeight = 606;

            // It's too late to refactor this out. 
            m_gameBoardCellWidth = m_gameBoardWidth / m_cellQuanityX;
            m_gameBoardCenterX = (m_graphics.PreferredBackBufferWidth / 2);
            m_gameBoardOriginX = m_gameBoardCenterX - (m_gameBoardWidth / 2);
            m_gameBoardCellHeight = m_gameBoardHeight / m_cellQuanityY;
            m_gameBoardOriginY = m_gameBoardCellHeight;
            m_gameBoardCellWidth2 = (int)(m_gameBoardCellWidth * 1.8);

            // But if I did refactor, it'd be with this. 
            gameBoard.Left = m_gameBoardOriginX;
            gameBoard.Right = m_gameBoardOriginX + m_gameBoardWidth;
            gameBoard.Top = m_gameBoardOriginY;
            gameBoard.Bottom = m_gameBoardOriginY + m_gameBoardHeight;
            gameBoard.CellHeight = m_gameBoardCellHeight;
            gameBoard.CellWidth = m_gameBoardCellWidth;
            gameBoard.Width = m_gameBoardWidth;
            gameBoard.Height = m_gameBoardHeight;
            gameBoard.PlayerBarrier = 2 * (m_gameBoardHeight / 3);
            gameBoard.Columns = gameBoard.Width / gameBoard.CellWidth;
            gameBoard.HalfCellWidth = gameBoard.CellWidth / 2;
            gameBoard.HalfCellHeight = gameBoard.CellHeight / 2;
            gameBoard.ShroomRows = (m_cellQuanityY - 2);
            gameBoard.ShroomRowSpace = gameBoard.ShroomRows * gameBoard.CellHeight;

            m_gameAgents = new GameAgents(this);
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");

            //create and place mushrooms
            Random rmd = new Random();
            for (int i = 0; i < m_cellQuanityX; i++)
            {
                for (int j = 0; j < gameBoard.ShroomRows; j++) // do not let shrooms spawn on bottom row for player
                {
                    if (rmd.Next(35) == 1) // 1/35 chance of mushroom on each square. 
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

            // create flea animator
            m_fleaAnimator = new AnimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                new int[] { 30, 30, 30, 30 },
                7);

            // create spider animator
            m_spiderAnimator = new AnimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                new int[] { 20, 20, 20, 20 },
                6);

            // create scorpion animator
            m_scorpionAnimator = new AnimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                new int[] { 30, 30, 30},
                8);

            // create and place player
            m_gameAgents.m_player = new Objects.Player(
                new Vector2(m_gameBoardCellWidth2, m_gameBoardCellHeight),
                new Vector2(m_gameBoardCenterX, m_gameBoardHeight),
                this.m_gameAgents,
                150f
                );
            m_gameAgents.m_playerList.Add(m_gameAgents.m_player);

            m_playerAnimator = new InanimatedSprite(
                contentManager.Load<Texture2D>("spritesheet-general"),
                15,
                1);

            m_scoreAnimator = new ScoreAnimator(
                contentManager.Load<Texture2D>("spritesheet-general"),
                m_gameBoardOriginX,
                m_gameBoardHeight);

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
            // updating the controls like a fool. (or a wizard? all depends on if you can change controls midway)
            updateControls();
            m_inputKeyboard.Update(gameTime);

            m_gameAgents.unregisterAnimatedSprites();

            // determine what mobs should spawn
            Random rmd = new Random();

            if (m_gameAgents.m_fleaList.Count < 1)
            {
                m_gameAgents.m_fleaList.Add(
                    new Objects.Flea(
                        new Vector2(gameBoard.CellWidth, gameBoard.CellHeight),
                        new Vector2(gameBoard.Left + gameBoard.HalfCellWidth + rmd.Next(m_cellQuanityX) * gameBoard.CellWidth, 0 - gameBoard.CellHeight),
                        m_gameAgents,
                        100f
                        )
                    );
            }

            // Spawning Spiders, Spawning Spiders, Spawning Spiders
            if (m_gameAgents.m_spiderList.Count < 1)
            {
                bool goingWest = rmd.Next(1) == 0;
                var spawnZoneFloor = gameBoard.ShroomRows * gameBoard.CellHeight;
                var spawnY = spawnZoneFloor + rmd.Next(gameBoard.Height - spawnZoneFloor);

                m_gameAgents.m_spiderList.Add(
                    new Objects.Spider(
                        new Vector2(gameBoard.CellWidth, gameBoard.CellHeight),
                        new Vector2(gameBoard.Left, gameBoard.Height - 100),//new Vector2(goingWest? gameBoard.Left : gameBoard.Right, 550),
                        m_gameAgents,
                        100f,
                        false,//goingWest,
                        false/*rmd.Next(1) == 0*/
                        )
                    );
            }

            // Spawning Scorpions!
            if (m_gameAgents.m_scorpionList.Count < 1)
            {
                bool goingWest = rmd.Next(1) == 0;
                var spawnZoneFloor = gameBoard.CellHeight;

                var spawnY = gameBoard.HalfCellHeight + gameBoard.CellHeight * rmd.Next(gameBoard.ShroomRows);

                m_gameAgents.m_scorpionList.Add(
                    new Objects.Scorpion(
                        new Vector2(m_gameBoardCellWidth2, gameBoard.CellHeight),
                        new Vector2(gameBoard.Right - gameBoard.CellWidth, spawnY),//new Vector2(goingWest? gameBoard.Left : gameBoard.Right, 550),
                        m_gameAgents,
                        280f,
                        false
                        )
                    );
            }

            // let moving things get a chance to move
            foreach (Objects.Lazer lazer in m_gameAgents.m_lazerList)
            {
                lazer.update(gameTime);
            }
            foreach (Objects.Flea flea in m_gameAgents.m_fleaList)
            {
                flea.update(gameTime);
            }
            foreach (Objects.Spider spider in m_gameAgents.m_spiderList)
            {
                spider.update(gameTime);
            }
            foreach (Objects.Scorpion scorpion in m_gameAgents.m_scorpionList)
            {
                scorpion.update(gameTime);
            }
            m_fleaAnimator.update(gameTime);
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

            foreach (Objects.Flea flea in m_gameAgents.m_fleaList)
            {
                m_fleaAnimator.draw(m_spriteBatch, flea);
            }

            foreach (Objects.Spider spider in m_gameAgents.m_spiderList)
            {
                m_spiderAnimator.draw(m_spriteBatch, spider);
            }

            foreach (Objects.Scorpion scorpion in m_gameAgents.m_scorpionList)
            {
                m_scorpionAnimator.draw(m_spriteBatch, scorpion);
            }

            foreach (Objects.Player player in m_gameAgents.m_playerList)
            {
                m_playerAnimator.draw(m_spriteBatch, player);
            }

            m_scoreAnimator.draw(m_spriteBatch, m_graphics, m_font, m_gameAgents.m_player.Lives, m_gameAgents.m_score);

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
