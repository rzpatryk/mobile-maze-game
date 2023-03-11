using Assets.Scripts.MazeParts.Cells;
using UnityEngine;

public abstract class DisplayMaze : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject startImage;
    [SerializeField]
    private GameObject endImage;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]

    private Canvas canvas;
    private GameObject wall;
    private GameObject player;

    private float cellHeight;
    private float cellWidth;
    protected float wallScale;


    public float CellHeight { get => cellHeight; set => cellHeight = value; }
    public float CellWidth { get => cellWidth; set => cellWidth = value; }
    public float WallScale { get => wallScale; set => wallScale = value; }

    public abstract void Display(Cell[][] mazeGrid, int row, int column);

    protected void SetCellSize(int row, int col)
    {
        float backgroundHeight = canvas.GetComponent<RectTransform>().rect.height;
        float backgroundWidth = canvas.GetComponent<RectTransform>().rect.width;
        CellHeight = ((backgroundHeight * 0.9f) / row * canvas.GetComponent<RectTransform>().localScale.y);
        CellWidth = ((backgroundWidth * 0.75f) / col * canvas.GetComponent<RectTransform>().localScale.x);
    }

    protected void CreateWall(Vector3 startPosition, Vector3 endPosition, string startOrEnd = null)
    {
        Vector3 notTar = (endPosition - startPosition).normalized;
        float angle = Mathf.Atan2(notTar.y, notTar.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        float distance = Vector3.Distance(startPosition, endPosition);
        wall = Instantiate(wallPrefab, startPosition, Quaternion.identity);
        wall.transform.position = (startPosition + endPosition) / 2;
        wall.transform.rotation = rotation;
        wall.transform.SetParent(GameObject.FindGameObjectWithTag("Maze").transform, false);
        wall.transform.localScale = new Vector3(CellHeight * wallScale, distance + (CellHeight * wallScale) / 2, 0);


        if (startOrEnd != null)
        {
            wall.GetComponent<SpriteRenderer>().color = new Color(wall.GetComponent<SpriteRenderer>().color.r, wall.GetComponent<SpriteRenderer>().color.g, wall.GetComponent<SpriteRenderer>().color.b, 0);
            if (startOrEnd.Equals("End"))
                wall.tag = "End";
        }
    }

    public virtual void CreateStartImage(float x, float y)
    {
        startImage.transform.localPosition = new Vector3(x, y, -1f);
        startImage.transform.localScale = new Vector2(CellHeight , CellHeight);
    }
    public virtual void CreateEndImage(float x, float y, bool end = false)
    {
        endImage.transform.localPosition = new Vector3(x, y, -1f);
        endImage.transform.localScale = new Vector2(CellHeight, CellHeight);
        if (end)
        {
            endImage.tag = "End";
        }
    }

    public virtual void CreatePlayer(Vector3 position)
    {
        player = Instantiate(playerPrefab, position, Quaternion.identity) as GameObject;
        player.transform.SetParent(GameObject.FindGameObjectWithTag("Maze").transform, false);
        player.transform.localScale = new Vector3(CellHeight, CellHeight, 0);

    }
}
