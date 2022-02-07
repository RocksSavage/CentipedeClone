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


        private TileState[,] maze;

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

            //DELETE LATER TRENT
            maze = generateRandomMaze(2);

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
            int n = (maze.GetLength(0) + 1) / 2;
            int TileSizePx = m_MazeSizePx / n - 2; // subtract 2 to make space for walls

            // draw maze internals
            int x = 2;
            int y = 2;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                x = 2;
                y += i % 2 == 0 ? TileSizePx : 4;

                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    if (i % 2 == 0) // even rows (rows with breadcrumbs & walls)
                    {
                        
                        
                        if (j % 2 == 0) // even cells (columns for breadcrumbs)
                        {
                            // case where we consider drawing breadcrumbs

                            if (maze[i, j].isVisited)
                            //m_spriteBatch.Draw()
                            {
                                //m_spriteBatch.Draw(new Rectangle(x, y, TileSizePx, TileSizePx));
                            }
                            if (maze[i, j].isShortestPath)
                            { }

                            x += TileSizePx;

                            
                        }
                        else // odd cells (columns for vertical walls)
                        {
                            if (maze[i, j].isWall) // case where this is a vertical wall section we are considering
                            {
                                m_spriteBatch.Draw(_texture, new Rectangle(x, y, m_MazeWallSizePx, TileSizePx), Color.White);
                            }

                            x += 4;
                        }
                    }
                    else // odd rows ( horizontal walls only)
                    {
                        if (j%2==0)
                            if (maze[i, j].isWall) // case where this is a horizontal wall section we are considering
                                m_spriteBatch.Draw(_texture, new Rectangle(x, y, TileSizePx, m_MazeWallSizePx), Color.White);

                        x += TileSizePx + 4;
                    }
                }
            }


            

            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
