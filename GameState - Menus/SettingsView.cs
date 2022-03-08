using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class SettingsView : GameStateView
    {
        private const string MESSAGE = "Settings (Customize Your Controls)";

        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private enum ControllerState
        {
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            Fire
        }

        private ControllerState m_currentSelection = ControllerState.MoveLeft;
        private bool m_waitForKeyRelease = false;

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");

        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }
            // This is the technique I'm using to ensure one keypress makes one menu navigation move
            if (!m_waitForKeyRelease)
            {
                // Arrow keys to navigate the menu
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    m_currentSelection = m_currentSelection + 1;
                    m_waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    m_currentSelection = m_currentSelection - 1;
                    m_waitForKeyRelease = true;
                }

                // If enter is pressed, return the appropriate new state
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerState.NewGame)
                //{
                //    return GameStateEnum.GamePlay;
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerState.HighScores)
                //{
                //    return GameStateEnum.HighScores;
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerState.Settings)
                //{
                //    return GameStateEnum.Settings;
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerState.About)
                //{
                //    return GameStateEnum.About;
                //}
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerState.Quit)
                //{
                //    return GameStateEnum.Exit;
                //}
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                m_waitForKeyRelease = false;
            }

            return GameStateEnum.Settings;
        }

        


        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            Vector2 stringSize = m_fontMenu.MeasureString(MESSAGE);
            m_spriteBatch.DrawString(m_fontMenu, MESSAGE,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);


            // I split the first one's parameters on separate lines to help you see them better
            float bottom = drawMenuItem(
                m_currentSelection == ControllerState.MoveLeft ? m_fontMenuSelect : m_fontMenu,
                "Move Left: ",
                200,
                m_currentSelection == ControllerState.MoveLeft ? Color.Yellow : Color.Blue
                );
            bottom = drawMenuItem(m_currentSelection == ControllerState.MoveRight? m_fontMenuSelect : m_fontMenu, "Move Right: ", bottom, m_currentSelection == ControllerState.MoveRight ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == ControllerState.MoveDown ? m_fontMenuSelect : m_fontMenu, "Move Down: ", bottom, m_currentSelection == ControllerState.MoveDown ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == ControllerState.MoveUp ? m_fontMenuSelect : m_fontMenu, "Move Up: ", bottom, m_currentSelection == ControllerState.MoveUp ? Color.Yellow : Color.Blue);
            drawMenuItem(m_currentSelection == ControllerState.Fire ? m_fontMenuSelect : m_fontMenu, "To Fire: ", bottom, m_currentSelection == ControllerState.MoveDown ? Color.Yellow : Color.Blue);


            m_spriteBatch.End();
        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(
                font,
                text,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
