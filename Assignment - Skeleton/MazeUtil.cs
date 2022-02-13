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
            TileState[,] maze = new TileState[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    maze[i, j] = new TileState();

            //  Prim's algorithm
            var random = new Random();
            var discovered = new List<(int,int)>();
            var frontier = new List<(int,int)>();

            (int, int) target = (random.Next(n - 1), random.Next(n - 1));

            discovered.Add(target);
            frontier = frontier.Union(getNeighbors(target, n)).ToList();

            while (frontier.Count > 0)
            {
                target = frontier[random.Next(frontier.Count)];
                // get neighbors of target in discovered and choose one randomly
                List<(int, int)> candidatesList = getNeighbors(target,n).Intersect(discovered).ToList();
                (int, int) targetee = candidatesList[random.Next(candidatesList.Count())];

                // erase the wall between the randomly chosen discovered tile and the target
                //(int, int) wallBetweenCoords = getWallCoords(target, targetee);

                //pathMap[wallBetweenCoords.Item1, wallBetweenCoords.Item2] = true;

                // establish path between the randomly chosen discovered tile and the target
                

                // put the target into discovered set
                discovered.Add(target);
                frontier.Remove(target);

                // throw new neighbors into frontier
                frontier = frontier.Union(getNeighbors(target,n).Except(candidatesList)).ToList();
            }

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
    }
}
