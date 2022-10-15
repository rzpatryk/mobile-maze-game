using Assets.Scripts.MazeParts.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    private MazeGrid mazeGrid;
    private int row;
    private int column;
    private Algorithms algorithms;


    public DisplaySquareMaze display;
    public GameObject GameManager;



    public MazeGrid MazeGrid { get => mazeGrid; set => mazeGrid = value; }
    public int Row { get => row; set => row = value; }
    public int Column { get => column; set => column = value; }

    void Start()
    {
        algorithms = new Algorithms();
    }

    public void CreateSquareMaze()
    {
        mazeGrid = new SquareGrid(row, column);
        algorithms.SetAlgorithm(0, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.DisplayMaze(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void DeleteMaze()
    {
        GameObject maze = GameObject.FindGameObjectWithTag("Maze");
        foreach (Transform child in maze.transform)
        {
            if (child.name.Equals("WallPrefab(Clone)"))
                Destroy(child.gameObject);
        }
        GameManager.GetComponent<GameManager>().BackToLevelSelect();

    }
}
