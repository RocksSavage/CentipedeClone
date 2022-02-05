using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public partial class Assignment : Game
    {
        Texture2D m_ballTexture;
        Vector2 m_ballPosition;
        float m_ballSpeed;

        private TileState[,,] maze;

        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;

        public Assignment()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            Rectangle myBox = new Rectangle(5, 5, 400, 400);
            
            Texture2D _texture;

            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });

            m_spriteBatch.Draw(_texture, myBox, Color.White);



            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
