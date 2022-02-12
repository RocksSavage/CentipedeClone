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
            public bool[,] pathMap;
            public bool[,,] breadcrumbs;
            public int n;
            public Maze(int n)
            {
                this.n = n;
                pathMap = new bool[n, n];
                breadcrumbs = new bool[n, n, 2];


                // maze generation
                var random = new Random();
                var discovered = new List<(int, int)>();
                var frontier = new List<(int, int)>();
                (int x, int y) target = (random.Next(this.n-1), random.Next(this.n-1));

                discovered.Add(target);
                frontier = frontier.Union(getNeighbors(target)).ToList();

                while (frontier.Count > 0)
                {
                    target = frontier[random.Next(frontier.Count)];
                    // get neighbors of target in discovered and choose one randomly
                    List<(int,int)> candidatesList = getNeighbors(target).Intersect(discovered).ToList();
                    (int,int) targetee = candidatesList[ random.Next(candidatesList.Count()) ];
                    
                    // erase the wall between the randomly chosen discovered tile and the target
                    (int, int) wallBetweenCoords = getWallCoords(target, targetee);
                    pathMap[wallBetweenCoords.Item1, wallBetweenCoords.Item2] = true;

                    // put the target into discovered set
                    discovered.Add(target);
                    frontier.Remove(target);

                    // throw new neighbors into frontier
                    frontier = frontier.Union(getNeighbors(target).Except(candidatesList)).ToList();
                }
            }
            /// <summary>
            /// gives the corrdinates for the wall location, given two
            /// adjacent cells. Warning: no input checks! No saftey!
            /// 
            /// </summary>
            /// <returns></returns>
            public (int,int) getWallCoords((int,int) A, (int,int) B)
            {
                if (A.Item1 == B.Item1) // case of same column
                {
                    int lateralDiff = (A.Item2 - B.Item2);
                    if      (lateralDiff >0) // case where A is south of B
                        return (B.Item1, B.Item2+1);
                    else if (lateralDiff < 0)
                        return (A.Item1, A.Item2+1);
                }
                else if (A.Item2 == B.Item2) // case of same row
                {
                    int lateralDiff = (A.Item1 - B.Item1);
                    if      (lateralDiff > 0) // case where A is east of B
                        return B;
                    else if (lateralDiff < 0)
                        return A;
                }
                // case of same cell. 
                throw new ArgumentException("tiles must be distinct");
            }

            /// <summary>
            /// Returns list of ordered pair cordinates
            /// that are neighbors to the target coordinates passed in. 
            /// Checks are made to ensure tiles off-the-map aren't
            /// included. 
            /// </summary>
            /// <param name="coords"></param>
            /// <returns></returns>
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

        } //end Maze

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
