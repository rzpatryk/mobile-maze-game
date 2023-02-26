using Assets.Scripts.MazeParts.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
    private IMazeAlgorithm mazeAlgorithm;
    private List<IMazeAlgorithm> algorithms;

    public Algorithms()
    {
        algorithms = new List<IMazeAlgorithm>();
        algorithms.Add(new BinaryTree());
        algorithms.Add(new Sidewinder());
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
            Debug.Log(mazeAlgorithm.GetType().Name);
        }
        else
        {
            index = random.Next(2, algorithms.Count);
            mazeAlgorithm = algorithms[index];
            Debug.Log(mazeAlgorithm.GetType().Name);
        }
    }

        public void ExecuteAlgorithm(MazeGrid mazeGrid)
    {
        mazeAlgorithm.CreateMaze(mazeGrid);
    }
}

