using Assets.Scripts.MazeParts.Cells;
using UnityEngine;

public class DisplaySquareMaze : DisplayMaze
{

    public override void Display(Cell[][] mazeGrid, int row, int column)
    {
        wallScale = 0.08f;
        SetCellSize(row, column);
        float x1, x2, y1, y2;
        for (int i = 0; i < mazeGrid.Length; i++)
        {
            for (int j = 0; j < mazeGrid[i].Length; j++)
            {
                Cell cell = mazeGrid[i][j];
                x1 = (cell.Column * CellWidth) - ((mazeGrid[0].Length / 2.0f) * CellWidth);
                y1 = (cell.Row * CellHeight) - ((mazeGrid.Length / 2.0f) * CellHeight);
                x2 = ((cell.Column + 1) * CellWidth) - ((mazeGrid[0].Length / 2.0f) * CellWidth);
                y2 = ((cell.Row + 1) * CellHeight) - ((mazeGrid.Length / 2.0f) * CellHeight);

                if (i == 0 && j == 0)
                {
                    CreateStartImage(x1 - CellWidth / 1.5f, y1 + CellHeight / 2);
                }
                if (i == row - 1 && j == column - 1)
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
                    if (i == row - 1 && j == column - 1)
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
