using Assets.Scripts.MazeParts.Grids;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField]
    private DisplayMaze display;
    [SerializeField]
    private GameObject GameManager;


    private MazeGrid mazeGrid;
    private int row;
    private int column;
    private Algorithms algorithms;



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
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void CreateTriangleGrid()
    {
        mazeGrid = new TriangleGrid(row, column);
        algorithms.SetAlgorithm(2, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void CreateHexGrid()
    {
        mazeGrid = new HexGrid(row, column);
        algorithms.SetAlgorithm(2, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }
    public void CreateHexShape()
    {
        mazeGrid = new HexShape(row, column);
        algorithms.SetAlgorithm(2, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void CreateCircleMaze()
    {
        mazeGrid = new PolarGrid(row, 1);
        algorithms.SetAlgorithm(2, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void CreateTriangleShape()
    {
        mazeGrid = new TriangleMaze(row, column);
        algorithms.SetAlgorithm(2, 6);
        algorithms.ExecuteAlgorithm(mazeGrid);
        display.Display(mazeGrid);
        GameManager.GetComponent<GameManager>().StartGame();
    }

    public void DeleteMaze()
    {
        GameObject maze = GameObject.FindGameObjectWithTag("Maze");
        foreach (Transform child in maze.transform)
        {
            if (child.name.Equals("WallPrefab(Clone)") || child.name.Equals("Player(Clone)") || child.name.Equals("GameObject(Clone)"))
                Destroy(child.gameObject);
        }
        GameManager.GetComponent<GameManager>().BackToLevelSelect();

    }
}
