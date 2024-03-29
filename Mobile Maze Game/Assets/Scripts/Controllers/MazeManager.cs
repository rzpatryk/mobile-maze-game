using Assets.Scripts.Factory;
using Assets.Scripts.MazeGenerationAlgorithms;
using Assets.Scripts.MazeParts.Grids;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    [SerializeField]
    private DisplayMaze Display;
    [SerializeField]
    private GameObject GuiManager;
    [SerializeField]
    private ExportMazeToPdf ExportMazeToPdf;
   
    private MazeGrid mazeGrid;
    private int row;
    private int column;
    private Algorithms algorithms;
    private string sceneName;



    public MazeGrid MazeGrid { get => mazeGrid; set => mazeGrid = value; }
    public int Row { get => row; set => row = value; }
    public int Column { get => column; set => column = value; }
    public bool IsBraid { get; set; }

    void Start()
    {
        algorithms = new Algorithms();
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void CreateMaze()
    {
        mazeGrid = GridFactory.CreateGrid(sceneName, row, column);
        algorithms.SetAlgorithm(sceneName);
        algorithms.ExecuteAlgorithm(mazeGrid);
        if (IsBraid)
        {
            mazeGrid.RemoveDeadEnds();
            IsBraid = false;
        }
        Display.Display(mazeGrid.Grid, mazeGrid.Row, mazeGrid.Column);
        GuiManager.GetComponent<GuiManager>().StartGame();
    }
   
    public void ExportToPdf()
    {
        string message = ExportMazeToPdf.SavePdf(sceneName, mazeGrid);
        GuiManager.GetComponent<GuiManager>().SaveMessage(message);
    }

    public void DeleteMaze()
    {
        GameObject maze = GameObject.FindGameObjectWithTag("Maze");
        foreach (Transform child in maze.transform)
        {
            if (child.name.Equals("WallPrefab(Clone)") || child.name.Equals("Player(Clone)") || child.name.Equals("GameObject(Clone)"))
                Destroy(child.gameObject);
        }
        GuiManager.GetComponent<GuiManager>().BackToLevelSelect();

    }
}
