using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using UnityEngine;

public class DisplayHexMazeGrid : DisplayMaze
{
    public override void Display(MazeGrid mazeGrid)
    {
        WallScale = 0.08f;
        SetCellSize(mazeGrid.Row, mazeGrid.Column);
        CellHeight -= (CellHeight / 2) / mazeGrid.Row;
        Debug.Log("CellWidth: " + CellWidth);
        CellWidth -= CellWidth / 3;
        Debug.Log("CellWidth2: " + CellWidth);
        float cx, cy;
        float x_fw, x_nw, x_ne, x_fe;
        float y_n, y_m, y_s;

        float a_size = CellWidth / 2;
        float b_size = CellHeight / 2;
        float height = b_size * 2;
        Vector3 positionA;
        Vector3 positionB;
        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                Cell cell = mazeGrid.Grid[i][j];
                cx = (CellWidth + 3 * cell.Column * a_size) - (((CellWidth * 3 * mazeGrid.Column) / 2 - a_size) / 2) - (a_size);
                cy = (b_size + cell.Row * height) - (b_size * mazeGrid.Row) - (b_size / 2);
                if (cell.Column % 2 != 0)
                    cy += b_size;

                x_fw = cx - CellWidth;
                x_nw = cx - a_size;
                x_ne = cx + a_size;
                x_fe = cx + CellWidth;

                y_n = cy - b_size;
                y_m = cy;
                y_s = cy + b_size;
                if (i == 0 && j == 0)
                {
                    CreateStartImage((cx - CellWidth * 2f), (cy));
                    CreatePlayer(new Vector3(cx, cy, -2));
                }

                if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                {
                    CreateEndImage((cx + CellWidth * 2), (cy));
                }

                if ((cell.Neighbours.ContainsKey("NorthWest") && !(cell.Linked(cell.Neighbours["NorthWest"]))) || !cell.Neighbours.ContainsKey("NorthWest"))
                {
                    positionA = new Vector3(x_fw, y_m, -1);
                    positionB = new Vector3(x_nw, y_s, -1);
                    CreateWall(positionA, positionB);
                }

                if ((cell.Neighbours.ContainsKey("SouthWest") && !(cell.Linked(cell.Neighbours["SouthWest"]))) || !cell.Neighbours.ContainsKey("SouthWest"))
                {
                    positionA = new Vector3(x_fw, y_m, -1);
                    positionB = new Vector3(x_nw, y_n, -1);
                    if (i == 0 && j == 0)
                    {
                        CreateWall(positionA, positionB, "Start");
                    }
                    else
                        CreateWall(positionA, positionB);
                }


                if ((cell.Neighbours.ContainsKey("South") && !(cell.Linked(cell.Neighbours["South"]))) || !cell.Neighbours.ContainsKey("South"))
                {
                    positionA = new Vector3(x_nw, y_n, -1);
                    positionB = new Vector3(x_ne, y_n, -1);
                    CreateWall(positionA, positionB);

                }
                if ((cell.Neighbours.ContainsKey("SouthEast") && !(cell.Linked(cell.Neighbours["SouthEast"]))) || !cell.Neighbours.ContainsKey("SouthEast"))
                {
                    positionA = new Vector3(x_ne, y_n, -1);
                    positionB = new Vector3(x_fe, y_m, -1);
                    CreateWall(positionA, positionB);
                }
                if ((cell.Neighbours.ContainsKey("NorthEast") && !(cell.Linked(cell.Neighbours["NorthEast"]))) || !cell.Neighbours.ContainsKey("NorthEast"))
                {
                    positionA = new Vector3(x_fe, y_m, -1);
                    positionB = new Vector3(x_ne, y_s, -1);
                    if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                    {
                        CreateWall(positionA, positionB, "End");
                    }
                    else
                        CreateWall(positionA, positionB);

                }
                if ((cell.Neighbours.ContainsKey("North") && !(cell.Linked(cell.Neighbours["North"]))) || !cell.Neighbours.ContainsKey("North"))
                {
                    positionA = new Vector3(x_ne, y_s, -1);
                    positionB = new Vector3(x_nw, y_s, -1);
                    CreateWall(positionA, positionB);

                }

            }
        }
    }
}
