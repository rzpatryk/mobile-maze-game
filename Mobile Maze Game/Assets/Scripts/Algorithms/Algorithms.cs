using Assets.Scripts.MazeParts.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
    private IMazeAlgorithm mazeAlgorithm;

    public void SetAlgorithm(int min, int max)
    {
        int rand = Random.Range(min, max);

        switch (rand)
        {
            case 0:
                mazeAlgorithm = new BinaryTree();
                Debug.Log("BinaryTree");
                break;
            case 1:
                mazeAlgorithm = new Sidewinder();
                Debug.Log("Sidewinder");
                break;
            case 2:
                mazeAlgorithm = new Wilson();
                Debug.Log("Wilson");
                break;
        }
    }

    public void ExecuteAlgorithm(MazeGrid mazeGrid)
    {
        mazeAlgorithm.CreateMaze(mazeGrid);
    }
}

