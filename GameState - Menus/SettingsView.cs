using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class SettingsView : GameStateView
    {
        private const string MESSAGE = "Settings (Press Enter to customize your controls)";
        private const string BTPROMPT = "<<Press Desired Button>>";

        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private ControllerStateEnum m_currentSelection = ControllerStateEnum.MoveLeft;
        private bool m_waitForKeyRelease = false;
        //private bool m_awaitingInput = false;

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
                // check and fill any outstanding key assignments
                // if check is true and key assignment filled, skip the rest of this... 
                //return GameStateEnum.Settings


                // Arrow keys to navigate the menu
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && m_currentSelection != ControllerStateEnum.Fire)
                {
                    m_currentSelection = m_currentSelection + 1;
                    m_waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && m_currentSelection != ControllerStateEnum.MoveLeft)
                {
                    m_currentSelection = m_currentSelection - 1;
                    m_waitForKeyRelease = true;
                }


                // If enter is pressed, mark that state as needing a new key
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerStateEnum.MoveLeft)
                {
                    ControllerState.MoveLeft = Keys.None;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerStateEnum.MoveRight)
                {
                    ControllerState.MoveRight = Keys.None;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerStateEnum.MoveDown)
                {
                    ControllerState.MoveDown = Keys.None;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerStateEnum.MoveUp)
                {
                    ControllerState.MoveUp = Keys.None;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == ControllerStateEnum.Fire)
                {
                    ControllerState.Fire = Keys.None;
                }
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
            m_spriteBatch.DrawString(
                m_fontMenu,
                MESSAGE,
                new Vector2(
                    m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,
                    m_graphics.PreferredBackBufferHeight / 3 - stringSize.Y
                    ),
                Color.Blue
                );


            // I split the first one's parameters on separate lines to help you see them better
            float bottom = drawMenuItem(
                m_currentSelection == ControllerStateEnum.MoveLeft ? m_fontMenuSelect : m_fontMenu,
                "Move Left:",
                ControllerState.MoveLeft.ToString(),
                m_graphics.PreferredBackBufferHeight / 3, /// 50 is arbitrary, for looks.
                m_currentSelection == ControllerStateEnum.MoveLeft ? Color.Yellow : Color.Blue
                );
            bottom = drawMenuItem(m_currentSelection == ControllerStateEnum.MoveRight? m_fontMenuSelect : m_fontMenu, "Move Right:", ControllerState.MoveRight.ToString(), bottom, m_currentSelection == ControllerStateEnum.MoveRight ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == ControllerStateEnum.MoveDown ? m_fontMenuSelect : m_fontMenu, "Move Down:", ControllerState.MoveDown.ToString(), bottom, m_currentSelection == ControllerStateEnum.MoveDown ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == ControllerStateEnum.MoveUp ? m_fontMenuSelect : m_fontMenu, "Move Up:", ControllerState.MoveDown.ToString(), bottom, m_currentSelection == ControllerStateEnum.MoveUp ? Color.Yellow : Color.Blue);
                     drawMenuItem(m_currentSelection == ControllerStateEnum.Fire ? m_fontMenuSelect : m_fontMenu, "To Fire:", ControllerState.MoveUp.ToString(), bottom, m_currentSelection == ControllerStateEnum.Fire ? Color.Yellow : Color.Blue);

            m_spriteBatch.End();
        }

        private float drawMenuItem(SpriteFont font, string text, string buttonText, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(
                font,
                text+" "+buttonText,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X, y),
                color);

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
