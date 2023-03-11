using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Path;
using UnityEngine;

public class DisplayTriangleShapeMaze : DisplayMaze
{
    public override void Display(Cell[][] mazeGrid, int row, int column)
    {
        WallScale = 0.1f;
        SetCellSize(row, column);
        float width = CellWidth;
        float height = CellHeight;
        float halfHeight = height / 2;

        float cx, cy;
        float westX, midX, eastX;
        float apexY, baseY;
        Vector3 positionA;
        Vector3 positionB;

        Distance distance = mazeGrid[0][0].Distances();
        Cell maxCell = distance.Max();
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

                if (cell.Updown())
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
                    CreatePlayer(new Vector3(midX, baseY + halfHeight, -2));
                    CreateStartImage(midX - width * 2f, baseY + CellHeight);
                }

                if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                {
                    if (cell.Updown())
                    {
                        CreateEndImage(midX + (width * i), baseY - halfHeight, true);
                    }
                    else
                    {
                        CreateEndImage(midX + (width * i), baseY + halfHeight, true);
                    }
                }

                if (!cell.Neighbours.ContainsKey("West"))
                {
                    if (i > 0)
                    {
                        positionA = new Vector3(westX + (width * i), baseY, -1);
                        positionB = new Vector3(midX + (width * i), apexY, -1);
                    }
                    else
                    {
                        positionA = new Vector3(westX, baseY, -1);
                        positionB = new Vector3(midX, apexY, -1);
                    }

                    CreateWall(positionA, positionB);

                }
                if ((cell.Neighbours.ContainsKey("East") && !cell.Linked(cell.Neighbours["East"])) || !cell.Neighbours.ContainsKey("East"))
                {
                    positionA = new Vector3(eastX + (width * i), baseY, -1);
                    positionB = new Vector3(midX + (width * i), apexY, -1);

                    CreateWall(positionA, positionB);

                }

                bool no_north = cell.Updown() && !cell.Neighbours.ContainsKey("North");
                bool not_linked = !cell.Updown() && ((cell.Neighbours.ContainsKey("South") && !cell.Linked(cell.Neighbours["South"])) || !cell.Neighbours.ContainsKey("South"));

                if (no_north || not_linked)
                {
                    positionA = new Vector3(eastX + (width * i), baseY, -1);
                    positionB = new Vector3(westX + (width * i), baseY, -1);
                    CreateWall(positionA, positionB);

                }

            }
        }
    }
}
