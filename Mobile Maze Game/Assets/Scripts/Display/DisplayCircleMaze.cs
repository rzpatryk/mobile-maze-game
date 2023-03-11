using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Path;
using System;
using UnityEngine;


public class DisplayCircleMaze : DisplayMaze
{
    public override void Display(Cell[][] mazeGrid, int row, int column)
    {
        WallScale = 0.15f;
        float theta, inner_radius, outer_radius, theta_ccw, theta_cw;
        float ax, ay, bx, by, cx, cy, dx, dy;
        SetCellSize((row + 1) * 2, (row + 1) * 2);
        Distance distance = mazeGrid[1][0].Distances();
        Cell maxCell = distance.Max();
        for (int i = 0; i < mazeGrid.Length; i++)
        {
            for (int j = 0; j < mazeGrid[i].Length; j++)
            {
                PolarCell cell = (PolarCell)mazeGrid[i][j];
                int row1 = cell.Row;
                theta = (float)(2 * Math.PI / mazeGrid[row1].Length);
                inner_radius = row1 * (CellHeight);
                outer_radius = ((row1 + 1) * CellHeight);


                theta_ccw = cell.Column * theta;
                theta_cw = (cell.Column + 1) * theta;

                ax = (float)(inner_radius * Math.Cos(theta_ccw));
                ay = (float)(inner_radius * Math.Sin(theta_ccw));
                bx = (float)(outer_radius * Math.Cos(theta_ccw));
                by = (float)(outer_radius * Math.Sin(theta_ccw));
                cx = (float)(inner_radius * Math.Cos(theta_cw));
                cy = (float)(inner_radius * Math.Sin(theta_cw));
                dx = (float)(outer_radius * Math.Cos(theta_cw));
                dy = (float)(outer_radius * Math.Sin(theta_cw));

                if (i == 0)
                {
                    CreateStartImage(0, 0);
                }
                if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                {
                    CreateEndImage((bx + cx) / 2f, (by + cy) / 2f, true);
                }
                if (i == 1 && j == 0)
                {
                    CreatePlayer(new Vector3((bx + cx) / 2f, (by + cy) / 2f, -2));
                }
                if (cell.Row > 0)
                {
                    Vector3 positionA = new Vector3(ax, ay, -1);
                    Vector3 positionB = new Vector3(bx, by, -1);
                    Vector3 positionC = new Vector3(cx, cy, -1);
                    Vector3 positionD = new Vector3(dx, dy, -1);

                    if (i == mazeGrid.Length - 1)
                    {
                        CreateWall(positionB, positionD);
                    }
                    if (cell.Neighbours.ContainsKey("Inward") && !cell.Linked(cell.Neighbours["Inward"]) || (i == 1 && j > 0))
                    {
                        CreateWall(positionA, positionC);
                    }

                    if ((cell.Neighbours.ContainsKey("Cw") && !(cell.Linked(cell.Neighbours["Cw"]))) || !cell.Neighbours.ContainsKey("Cw"))
                    {
                        CreateWall(positionC, positionD);
                    }

                }

            }
        }
    }
}
