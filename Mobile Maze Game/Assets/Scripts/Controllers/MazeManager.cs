using Assets.Scripts.MazeParts.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    private MazeGrid mazeGrid;
    private int row;
    private int column;

    public DisplaySquareMaze display;

    public MazeGrid MazeGrid { get => mazeGrid; set => mazeGrid = value; }
    public int Row { get => row; set => row = value; }
    public int Column { get => column; set => column = value; }

    public void createSquareMaze()
    {
        mazeGrid = new SquareGrid(row, column);
        display.DisplayMaze(mazeGrid);
    }
}
