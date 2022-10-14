using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySquareMaze : MonoBehaviour
{
    public GameObject WallPrefab;
    public GameObject BackGround;
    public Canvas canvas;
    private GameObject wall;
    private float cellHeight;
    private float cellWidth;

    public float CellHeight { get => cellHeight; set => cellHeight = value; }
    public float CellWidth { get => cellWidth; set => cellWidth = value; }

    protected virtual void SetCellSize(int row, int col)
    {
        //float backgroundHeight = BackGround.GetComponent<RectTransform>().rect.height;
       // float backgroundWidth = BackGround.GetComponent<RectTransform>().rect.width;
        float backgroundHeight = canvas.GetComponent<RectTransform>().rect.height;
        float backgroundWidth = canvas.GetComponent<RectTransform>().rect.width;
        CellHeight = ((backgroundHeight - 100) / row * canvas.GetComponent<RectTransform>().localScale.y);
        CellWidth = ((backgroundWidth - 100) / col * canvas.GetComponent<RectTransform>().localScale.x );
        //cellWidth = ((camWidth / 100) * 0.85f) / Column;
        //cellHeight = ((camHeight / 100) * 0.9f) / Row;
    }

    public virtual void DisplayMaze(MazeGrid mazeGrid)
    {
        SetCellSize(mazeGrid.Row, mazeGrid.Column);
        float x1, x2, y1, y2;
        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                SquareCell cell = (SquareCell)mazeGrid.Grid[i][j];
                x1 = (cell.Column * CellWidth) - ((mazeGrid.Grid[0].Length / 2f) * CellWidth);
                y1 = (cell.Row * CellHeight) - ((mazeGrid.Grid.Length / 2f) * CellHeight);
                x2 = ((cell.Column + 1) * CellWidth) - ((mazeGrid.Grid[0].Length / 2f) * CellWidth);
                y2 = ((cell.Row + 1) * CellHeight) - ((mazeGrid.Grid.Length / 2f) * CellHeight);
                /*if (i == 0 && j == 0)
                {
                    CreateStartImage(x1 + CellWidth, y1 + CellHeight);
                    CreateEndImage(-(x1 + CellWidth), y1 + CellHeight);
                }*/

                if (cell.IsOn)
                {

                    if (!cell.Neighbours.ContainsKey("South") || (cell.Neighbours.ContainsKey("South") && !(cell.Linked(cell.Neighbours["South"]))))
                    {
                        if (i == 2 && j == mazeGrid.Column - 1)
                            CreateWall(new Vector3(x1, y1, -1), new Vector3(x2, y1, -1), "End");
                        else
                            CreateWall(new Vector3(x1, y1, -1), new Vector3(x2, y1, -1));
                    }
                    if (!cell.Neighbours.ContainsKey("West"))
                    {

                        if (i == 0 && j == 2)
                        {
                            CreateWall(new Vector3(x1, y1, -1), new Vector3(x1, y2, -1), "Start");
                            //CreatePlayer(new Vector3((x1 + CellWidth / 2), (y1 + CellHeight / 2), -2));
                        }
                        else
                        {
                            CreateWall(new Vector3(x1, y1, -1), new Vector3(x1, y2, -1));
                        }

                    }
                    if (!cell.Neighbours.ContainsKey("East") || (cell.Neighbours.ContainsKey("East") && !(cell.Linked(cell.Neighbours["East"]))))
                    {
                        CreateWall(new Vector3(x2, y1, -1), new Vector3(x2, y2, -1));
                    }
                    if (!(cell.Neighbours.ContainsKey("North")))
                    {
                        CreateWall(new Vector3(x1, y2, -1), new Vector3(x2, y2, -1));
                    }
                }
            }
        }
    }

    protected void CreateWall(Vector3 startPosition, Vector3 endPosition, string startOrEnd = null)
    {
        Vector3 notTar = (endPosition - startPosition).normalized;
        float angle = Mathf.Atan2(notTar.y, notTar.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        float distance = Vector3.Distance(startPosition, endPosition);
        wall = Instantiate(WallPrefab, startPosition, Quaternion.identity);
        wall.transform.position = (startPosition + endPosition) / 2;
        wall.transform.rotation = rotation;
        wall.transform.SetParent(GameObject.FindGameObjectWithTag("Maze").transform, false);
        wall.transform.localScale = new Vector3(CellHeight * 0.1f, distance + CellHeight * 0.1f, /*CellHeight * 0.07f*/0);
        //wall.transform.localScale = new Vector3(0.10f, distance + 0.10f, 0.10f);
       /* if (startOrEnd != null)
        {
            wall.GetComponent<SpriteRenderer>().color = new Color(wall.GetComponent<SpriteRenderer>().color.r, wall.GetComponent<SpriteRenderer>().color.g, wall.GetComponent<SpriteRenderer>().color.b, 0);
            if (startOrEnd.Equals("End"))
                wall.tag = "End";
        }*/
    }
}
