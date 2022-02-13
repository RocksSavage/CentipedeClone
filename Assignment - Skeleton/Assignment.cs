using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public partial class Assignment : Game
    {
        Texture2D m_ballTexture;
        Texture2D m_BGTexture;
        Texture2D m_FootPrintTexture;
        Texture2D m_ShortestPathTexture;
        static int m_MazeSizePx = 500;
        static int m_MazeWallSizePx = 4;


        private TileState[,] m_maze;

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

            m_graphics.IsFullScreen = false;
            m_graphics.PreferredBackBufferWidth = m_MazeSizePx + 200; // tack on some space for timer, etc
            m_graphics.PreferredBackBufferHeight = m_MazeSizePx;
            m_graphics.ApplyChanges();

            m_maze = generateRandomMaze(5);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_ballTexture = Content.Load<Texture2D>("ball");
            m_BGTexture = Content.Load<Texture2D>("black_grunge_bg");
        }
        

        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            ProcessInput(gameTime);

            //maze = generateRandomMaze(2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();

            // draw bg
            m_spriteBatch.Draw(m_BGTexture, new Rectangle(0, 0, m_MazeSizePx, m_graphics.PreferredBackBufferHeight), Color.White);

            // draw outline of maze
            Texture2D _texture;
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Black});

            m_spriteBatch.Draw(_texture, new Rectangle(0, 0, m_MazeWallSizePx, m_MazeSizePx), Color.White);
            m_spriteBatch.Draw(_texture, new Rectangle(0, 0, m_MazeSizePx, m_MazeWallSizePx), Color.White);
            
            m_spriteBatch.Draw(_texture, new Rectangle(0, m_MazeSizePx - 5, m_MazeSizePx, m_MazeWallSizePx), Color.White);
            m_spriteBatch.Draw(_texture, new Rectangle(m_MazeSizePx - 5, 0, m_MazeWallSizePx, m_MazeSizePx), Color.White);

            // calc sizes
            int n = m_maze.GetLength(0);
            int TileSizePx =(m_MazeSizePx / n) - 2; // subtract 2 to make space for walls


            // draw maze internals

            // 'floor' walls
            for (int x = 2; x < TileSizePx*n; x=x+TileSizePx)
                for (int y = TileSizePx + 2; y < TileSizePx*n; y=y+TileSizePx)
                {
                    m_spriteBatch.Draw(_texture, new Rectangle(x, y, TileSizePx +4, m_MazeWallSizePx), Color.White);
                }

            for (int x = TileSizePx + 2; x < TileSizePx * n; x = x + TileSizePx)
                for (int y = 2; y < TileSizePx * n; y = y + TileSizePx)
                {
                    m_spriteBatch.Draw(_texture, new Rectangle(x, y, m_MazeWallSizePx, TileSizePx + 4), Color.White);
                }


            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
