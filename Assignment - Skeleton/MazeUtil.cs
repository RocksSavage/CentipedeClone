using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
            public bool[,,] breadcrumbs;
            public int n;
            public Maze(int n)
            {
                this.n = n;
                walls = new bool[2*n-1, 2*n-1];
                breadcrumbs = new bool[n, n, 2];


                // maze generation
                var random = new Random();
                var discovered = new List<(int, int)>();
                var frontier = new List<(int, int)>();
                (int x, int y) target = (random.Next(this.n), random.Next(this.n));

                discovered.Add(target);
                frontier.Union(getNeighbors(target));
                
                while (frontier.Count > 0)
                {
                    target = frontier[random.Next(frontier.Count)];
                    // get neighbors of target in discovered and choose one randomly
                    List<(int,int)> candidatesList = (List<(int, int)>)getNeighbors(target).Union(discovered);
                    (int,int) targetee = candidatesList[ random.Next(candidatesList.Count()) ];
                    // chagne this later??
                    //getWallCoords(target, targetee);
                }

            }

            List<(int,int)> getNeighbors((int, int) coords)
            {
                List<(int,int)> neighbors = new List<(int, int)>();
                if (coords.Item1 - 1 >= 0)
                    neighbors.Add((coords.Item1 - 1, coords.Item2));
                if (coords.Item1 + 1 < this.n)
                    neighbors.Add((coords.Item1+1,coords.Item2));
                if (coords.Item2 - 1 >= 0)
                    neighbors.Add((coords.Item1, coords.Item2 -1));
                if (coords.Item2 + 1 < this.n)
                    neighbors.Add((coords.Item1, coords.Item2 + 1));
                return neighbors;
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
