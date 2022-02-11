using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CS5410
{
    public partial class Assignment : Game
    {
        Texture2D m_ballTexture;
        Texture2D m_BGTexture;
        Texture2D m_FootPrintTexture;
        Texture2D m_ShortestPathTexture;
        SpriteFont m_font;
        SpriteBatch spriteBatch;
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
            m_graphics.PreferredBackBufferHeight = m_MazeSizePx*2;
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
            //m_SpriteFont = this.Content.Load<SpriteFont>("Ariel");
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
            //int TileSizePx = m_MazeSizePx / n - 2; // subtract 2 to make space for walls

            // draw maze internals
            
            // about this trade off: we print walls first, breadcrumbs second.
            // breadcrumps can be toggled on/ off, so those need to be detatchable anyway. 
            // ****^^^^ this is the big breakthrough. 

            // 2 sets of while loops. 
            // maybe the first can incorporate drawing the character, too, since he is always drawn. 

            // so, for the walls + character loops:

            // who tracks the top-left printers coords, this function or the obj? 
            // I think that code should go here. 

            // who tracks which texture to show? 
            // Here. 

            // who tracks rectangle size? 
            // Here. 

            // I want the maze locatoin bool arrary to be encapsulated in an obj. WHy?
            // so that when I have to do move checks, I can do something like
            // Guy.move(left)
            // move(direction)
            //   check if desired direction is in the map; (track state to check this)
            //      sdf
            //   check if wall is in the way
            //      m
            //   if neither of these are bad, update guy object. 

            // let the main character be a separate object that keeps track of his own corrds. 

            // could be like this:
            Maze mazey = new Maze(5);
            int TileSizePx = (m_MazeSizePx / mazey.n) - 4; // subtract 2 to make space for walls

            int x = 2;
            int y = 2;
            
            for (int i = 0; i < mazey.walls.GetLength(0); i++) // horizontal walls
            {
                for (int j = 0; j < mazey.n; j++)
                {
                    if (mazey.walls[i, j])
                    {
                        if (i % 2 == 0) // case of even rows (vertical walls)
                            m_spriteBatch.Draw(_texture, new Rectangle(x, y, m_MazeWallSizePx, TileSizePx), Color.White);

                        else // case of odd rows (horizontal walls)
                            m_spriteBatch.Draw(_texture, new Rectangle(x, y, TileSizePx + 4, m_MazeWallSizePx), Color.White);
                    }

                    x += TileSizePx;
                }
                x = 2;
                if (i % 2 == 0)
                    y += TileSizePx;
                else
                    y += 4;
            }

            x = 2;
            y = 2;





            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
