using Assets.Scripts.MazeParts.Cells;
using UnityEngine;

public class DisplayTriangleMazeGrid : DisplayMaze
{
    public override void Display(Cell[][] mazeGrid, int row, int column)
    {
        WallScale = 0.08f;
        SetCellSize(row, column + 1);
        float width = CellWidth;

        float height = CellHeight;
        float halfHeight = CellHeight / 2;
        float cx, cy;
        float westX, midX, eastX;
        float apexY, baseY;
        Vector3 positionA;
        Vector3 positionB;
        for (int i = 0; i < mazeGrid.Length; i++)
        {
            for (int j = 0; j < mazeGrid[i].Length; j++)
            {
                TriangleCell cell = (TriangleCell)mazeGrid[i][j];

                cx = width + cell.Column * width - (column * width / 2) - width / 2;

                cy = halfHeight + cell.Row * height - ((row * halfHeight));
                westX = cx - width;
                midX = cx;
                eastX = cx + width;

                if (!cell.Upright())
                {
                    apexY = cy - halfHeight;
                    baseY = cy + halfHeight;
                }
                else
                {
                    apexY = cy + halfHeight;
                    baseY = cy - halfHeight;
                }

                if (i == 0 && j == 0)
                {
                    CreateStartImage((westX), (apexY));
                }
                if (i == row - 1 && j == column - 1)
                {
                    CreateEndImage((eastX), (apexY));
                }


                if (!cell.Neighbours.ContainsKey("West"))
                {
                    positionA = new Vector3(westX, baseY, -1);
                    positionB = new Vector3(midX, apexY, -1);
                    if (i == 0 && j == 0)
                    {
                        CreateWall(positionA, positionB, "Start");
                        CreatePlayer(new Vector3((midX), (baseY + halfHeight), -2));
                    }

                    else
                        CreateWall(positionA, positionB);

                }
                if (!cell.Neighbours.ContainsKey("East") || (cell.Neighbours.ContainsKey("East") && !cell.Linked(cell.Neighbours["East"])))
                {
                    positionA = new Vector3(eastX, baseY, -1);
                    positionB = new Vector3(midX, apexY, -1);
                    if (i == row - 1 && j == column - 1)
                    {
                        CreateWall(positionA, positionB, "End");
                    }
                    else
                        CreateWall(positionA, positionB);
                }

                bool no_north = (!cell.Upright() && !cell.Neighbours.ContainsKey("North"));
                bool not_linked = (cell.Upright() && !cell.Neighbours.ContainsKey("South")) || (cell.Neighbours.ContainsKey("South") && !cell.Linked(cell.Neighbours["South"]));

                if (not_linked || no_north)
                {
                    positionA = new Vector3(eastX, baseY, -1);
                    positionB = new Vector3(westX, baseY, -1);
                    CreateWall(positionA, positionB);

                }


            }
        }
    }
}
