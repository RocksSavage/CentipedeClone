using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace CS5410
{
    public partial class Assignment : Game
    {
        public struct TileState
        {
            
            public bool isVisited { get; set; }
            public bool isShortestPath { get; set; }
            public bool south { get; set; }
            public bool east { get; set; }
        }

        protected void ProcessInput(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var kstate = Keyboard.GetState();

            //if (kstate.IsKeyDown(Keys.Up))
            //    m_ballPosition.Y -= m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Down))
            //    m_ballPosition.Y += m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Left))
            //    m_ballPosition.X -= m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Right))
            //    m_ballPosition.X += m_ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // its best to run all meeple movements before generating a new maze so as not to 
            // create an annoying but where the player starts off a couple turns in. 

            //if (kstate.)
            //Dean said he might do this later?
        }

        /// <summary>
        /// Returns top-left node of maze. 
        /// </summary>
        /// <param name="size">the length the maze can be along each side</param>
        /// <returns></returns>
        private TileState[,] generateRandomMaze(int size)
        {
            // demo: make a simple 2x2 maze with one wall in it. 

            TileState[,] maze = new TileState[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    maze[i, j] = new TileState();

            //  Prim's algorithm


            return maze;
        }
    }
}
