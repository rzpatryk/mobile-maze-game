using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using UnityEngine;

public class DisplaySquareMaze : DisplayMaze
{

    public override void Display(MazeGrid mazeGrid)
    {
        wallScale = 0.08f;
        SetCellSize(mazeGrid.Row, mazeGrid.Column);
        float x1, x2, y1, y2;
        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                Cell cell = mazeGrid.Grid[i][j];
                x1 = (cell.Column * CellWidth) - ((mazeGrid.Grid[0].Length / 2.0f) * CellWidth);
                y1 = (cell.Row * CellHeight) - ((mazeGrid.Grid.Length / 2.0f) * CellHeight);
                x2 = ((cell.Column + 1) * CellWidth) - ((mazeGrid.Grid[0].Length / 2.0f) * CellWidth);
                y2 = ((cell.Row + 1) * CellHeight) - ((mazeGrid.Grid.Length / 2.0f) * CellHeight);

                if (i == 0 && j == 0)
                {
                    CreateStartImage(x1 - CellWidth / 1.5f, y1 + CellHeight / 2);
                }
                if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                {
                    CreateEndImage(x2 + CellWidth / 2, y2 - CellHeight / 2);
                }



                if (!cell.Neighbours.ContainsKey("South") || (cell.Neighbours.ContainsKey("South") && !(cell.Linked(cell.Neighbours["South"]))))
                {
                    CreateWall(new Vector3(x1, y1, -1), new Vector3(x2, y1, -1));
                }
                if (!cell.Neighbours.ContainsKey("West"))
                {

                    if (i == 0 && j == 0)
                    {
                        CreateWall(new Vector3(x1, y1, -1), new Vector3(x1, y2, -1), "Start");
                        CreatePlayer(new Vector3((x1 + CellWidth / 2), (y1 + CellHeight / 2), -2));
                    }
                    else
                    {
                        CreateWall(new Vector3(x1, y1, -1), new Vector3(x1, y2, -1));
                    }

                }
                if (!cell.Neighbours.ContainsKey("East") || (cell.Neighbours.ContainsKey("East") && !(cell.Linked(cell.Neighbours["East"]))))
                {
                    if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                    {
                        CreateWall(new Vector3(x2, y1, -1), new Vector3(x2, y2, -1), "End");
                    }
                    else
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
