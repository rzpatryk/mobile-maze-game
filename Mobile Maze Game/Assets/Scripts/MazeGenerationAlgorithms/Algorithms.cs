using Assets.Scripts.MazeParts.Grids;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class Algorithms
    {
        private IMazeAlgorithm mazeAlgorithm;
        private List<IMazeAlgorithm> algorithms;

        public Algorithms()
        {
            algorithms = new List<IMazeAlgorithm>();
            algorithms.Add(new BinaryTree());
            algorithms.Add(new Sidewinder());
            algorithms.Add(new RecursiveDivision());
            algorithms.Add(new HuntAndKill());
            algorithms.Add(new RecursiveBacktracker());
            algorithms.Add(new AldousBroder());
            algorithms.Add(new Wilson());
        }

        public void SetAlgorithm(string name)
        {
            System.Random random = new System.Random();
            int index;

            if (name.Equals("SquareMaze"))
            {
                index = random.Next(algorithms.Count);
                mazeAlgorithm = algorithms[index];
            }
            else
            {
                index = random.Next(3, algorithms.Count);
                mazeAlgorithm = algorithms[index];
            }
        }

        public void ExecuteAlgorithm(MazeGrid mazeGrid)
        {
            mazeAlgorithm.CreateMaze(mazeGrid);
        }
    }
}

