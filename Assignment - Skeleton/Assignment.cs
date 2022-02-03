using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class Assignment : Game
    {
        Texture2D m_ballTexture;
        Vector2 m_ballPosition;
        float m_ballSpeed;

        // HEY Trent, do this here. 
        private Nodey array[,,] maze;


        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;

        public Assignment()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            maze
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_ballPosition = new Vector2(m_graphics.PreferredBackBufferWidth / 2,
                                         m_graphics.PreferredBackBufferHeight / 2);
            m_ballSpeed = 100f;

            m_graphics.IsFullScreen = false;
            m_graphics.PreferredBackBufferWidth = 640;
            m_graphics.PreferredBackBufferHeight = 480;
            m_graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_ballTexture = Content.Load<Texture2D>("ball");

        }
        protected void ProcessInput(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                m_ballPosition.Y -= m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                m_ballPosition.Y += m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                m_ballPosition.X -= m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                m_ballPosition.X += m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            ProcessInput(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(
                m_ballTexture,
                m_ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(m_ballTexture.Width / 2, m_ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }



    public struct Nodey
    {
        public Nodey()
        {
            isWall = false;
            isBreadcrumb = false;
            isShortestPath = false;
        }
        bool isWall { get; set; }
        bool isBreadcrumb { get; set; }
        bool isShortestPath { get; set; }
    }
}
