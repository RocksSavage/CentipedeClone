﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public partial class Assignment : Game
    {

        public struct TileState
        {
            public TileState(bool isWall, bool isVisited, bool isShortestPath)
            {
                this.isWall = isWall;
                this.isVisited = isVisited;
                this.isShortestPath = isShortestPath;
            }
            public bool isWall { get; set; }
            public bool isVisited { get; set; }
            public bool isShortestPath { get; set; }
        }

        class Maze
        {
            // later I can make these private after debugging. 
            public bool[,] walls;
            public bool[,] breadcrumbs;
            public int n;
            public Maze(int n)
            {
                // consider making this the new maze generator
                this.n = n;
                walls = new bool[n-1, n-1];
                breadcrumbs = new bool[n, n];
            }

        }

        // is this needed?
        public class Guy
        {
            int x;
            int y;
            Guy()
            {
                x = 0;
                y = 0;
            }
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

        private TileState[,] generateRandomMaze(int size)
        {
            // demo: make a simple 2x2 maze with one wall in it. 
            TileState bob = new TileState(true, true, true);

            return new TileState[3, 3]
            {
                { new TileState(true,false,false),
                    new TileState(true, false, false),
                    new TileState(true, false, false)
                },
                {  new TileState(true,false,false),
                 new TileState(true,false,false),
                 new TileState(true,false,false)
                },
                {  new TileState(true,false,false),
                 new TileState(true,false,false),
                 new TileState(true,false,false)
                }
            };
        }
    }
}
