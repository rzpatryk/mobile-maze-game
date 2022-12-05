using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using Assets.Scripts.MazeParts.Path;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHexShape : DisplaySquareMaze
{
    public override void DisplayMaze(MazeGrid mazeGrid)
    {
        WallScale = 0.08f;


        SetCellSize(mazeGrid.Row, mazeGrid.Column, 0.9f, 0.45f);


        float size = CellHeight + (CellHeight / 3);

        float cx, cy;

        float a_size = size / 4;

        float b_size = size / 2;
        float width = b_size * 2;




        Vector3 positionA;
        Vector3 positionB;
        Distance distance = mazeGrid.Grid[0][0].Distances();
        Cell maxCell = distance.Max();

        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                Cell cell = mazeGrid.Grid[i][j];
                cx = (b_size + cell.Column * width)- ((mazeGrid.Grid[i].Length / 2.0f) * width);
                cy = size / 2 + 3 * cell.Row * a_size - ((mazeGrid.Row / 2f * size) - mazeGrid.Row / 2.0f * a_size) - a_size / 2;
                //cy = CellHeight + 3 * cell.Row * a_size; // - ((mazeGrid.Row / 2f * size) - mazeGrid.Row / 2.0f * a_size) - a_size / 2;


                float x_west_north = cx - b_size;
                float y_west_north = cy + a_size;

                float x_west_south = cx - b_size;
                float y_west_south = cy - a_size;

                float x_east_north = cx + b_size;
                float y_east_north = cy + a_size;

                float x_east_south = cx + b_size;
                float y_east_south = cy - a_size;

                float y_mid_north = cy + size/2;
                float y_mid_south = cy - size/2;


                if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                {
                    CreateEndImage(cx, cy, true);
                }

                if ((cell.Neighbours.ContainsKey("West") && !(cell.Linked(cell.Neighbours["West"]))) || !cell.Neighbours.ContainsKey("West"))
                {
                    positionA = new Vector3(x_west_north, y_west_north, -1);
                    positionB = new Vector3(x_west_south, y_west_south, -1);
                    if (i == 0 && j == 0)
                    {
                        CreatePlayer(new Vector3(cx, cy, -2));
                        CreateStartImage(cx - size - a_size, cy);
                        CreateWall(positionA, positionB, "Start");
                    }
                    else
                    {
                        CreateWall(positionA, positionB);
                    }
                }
                if ((!cell.Neighbours.ContainsKey("East")))
                {
                    positionA = new Vector3(x_east_north, y_east_north, -1);
                    positionB = new Vector3(x_east_south, y_east_south, -1);
                    CreateWall(positionA, positionB);

                }
                if ((!cell.Neighbours.ContainsKey("NorthWest")))
                {
                    positionA = new Vector3(x_west_north, y_west_north, -1);
                    positionB = new Vector3(cx, y_mid_north, -1);
                   CreateWall(positionA, positionB);

                }
                if ((!cell.Neighbours.ContainsKey("NorthEast")))
                {
                    positionA = new Vector3(x_east_north, y_east_north, -1);
                    positionB = new Vector3(cx, y_mid_north, -1);
                    CreateWall(positionA, positionB);
                }
                if ((cell.Neighbours.ContainsKey("SouthWest") && !(cell.Linked(cell.Neighbours["SouthWest"]))) || !cell.Neighbours.ContainsKey("SouthWest"))
                {
                    positionA = new Vector3(x_west_south, y_west_south, -1);
                    positionB = new Vector3(cx, y_mid_south, -1);
                    CreateWall(positionA, positionB);

                }
                if ((cell.Neighbours.ContainsKey("SouthEast") && !(cell.Linked(cell.Neighbours["SouthEast"]))) || !cell.Neighbours.ContainsKey("SouthEast"))
                {
                    positionA = new Vector3(x_east_south, y_east_south, -1);
                    positionB = new Vector3(cx, y_mid_south, -1);
                    CreateWall(positionA, positionB);

                }

            }
        }
    }
}
