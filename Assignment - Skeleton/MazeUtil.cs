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
        /// <param name="n">the length the maze can be along each side</param>
        /// <returns></returns>
        private TileState[,] generateRandomMaze(int n)
        {
            // "Create a graph of cells"
            TileState[,] maze = new TileState[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    maze[i, j] = new TileState();

            //  Prim's algorithm
            var random = new Random();
            var discovered = new List<(int,int)>();
            var frontier = new List<(int,int)>();

            // "Randomly pick a cell, add it to the maze"
            (int, int) target = (random.Next(n - 1), random.Next(n - 1));
            discovered.Add(target);

            // "Add its neighboring cells to the frontier"
            frontier = getNeighbors(target, n);

            while (frontier.Count > 0)
            {
                // "Randomly choose a cell in the frontier and (randomly) pick a wall that connects to
                //  a cell in the maze"
                target = frontier[random.Next(frontier.Count)];
                // get neighbors of target in discovered and choose one randomly
                List<(int, int)> candidatesList = getNeighbors(target,n).Intersect(discovered).ToList();
                (int, int) targetee = candidatesList[random.Next(candidatesList.Count())];

                // "Remove that wall"
                // establish path between the randomly chosen discovered tile and the target
                if (target.Item1 == targetee.Item1) // case of same column
                {
                    if (target.Item2 > targetee.Item2) // case where A is south of B
                        maze[targetee.Item1, targetee.Item2].south = true;
                    else
                        maze[target.Item1, target.Item2].south = true;
                }
                else if (target.Item2 == targetee.Item2) // case of same row
                {
                    if (target.Item1 > targetee.Item1) // case where A is east of B
                        maze[targetee.Item1, targetee.Item2].east = true;
                    else
                        maze[target.Item1, target.Item2].east = true;

                }
                else
                    throw new ArgumentException("Target and targetee are the same");

                // "Add that cell to the maze"
                // put the target into discovered set
                discovered.Add(target);
                frontier.Remove(target);

                // "Update the frontier"
                // throw new neighbors into frontier
                frontier = frontier.Union(getNeighbors(target,n).Except(candidatesList)).ToList();
            } // "Repeat Step 3 until no more cells in the frontier"

            return maze;
        }
        List<(int, int)> getNeighbors((int,int) target, int n)
        {
            List<(int,int)> neighbors = new List<(int,int)>();
            if (target.Item1 - 1 >= 0)
                neighbors.Add((target.Item1 - 1, target.Item2));
            if (target.Item1 + 1 < n)
                neighbors.Add((target.Item1 + 1, target.Item2));
            if (target.Item2 - 1 >= 0)
                neighbors.Add((target.Item1, target.Item2 - 1));
            if (target.Item2 + 1 < n)
                neighbors.Add((target.Item1, target.Item2 + 1));
            return neighbors;
        }
        public (int, int) getWallCoords((int, int) target, (int, int) targetee)
        {
            if (target.Item1 == targetee.Item1) // case of same column
            {
                int lateralDiff = (target.Item2 - targetee.Item2);
                if (lateralDiff > 0) // case where A is south of B
                    return (target.Item1, target.Item2 * 2 + 1);
                else if (lateralDiff < 0)
                    return (targetee.Item1, targetee.Item2 * 2 + 1);
            }
            else if (target.Item2 == targetee.Item2) // case of same row
            {
                int lateralDiff = (target.Item1 - targetee.Item1);
                if (lateralDiff > 0) // case where A is east of B
                    return targetee;
                else if (lateralDiff < 0)
                    return target;
            }
            // case of same cell. 
            throw new ArgumentException("tiles must be distinct");
        }
    }
}
