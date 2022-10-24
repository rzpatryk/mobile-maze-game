using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHexShape : DisplaySquareMaze
{
    public Text TextPrefab;
    private Text text;
    public override void DisplayMaze(MazeGrid mazeGrid)
    {
        WallScale = 0.08f;

        
        SetCellSize(mazeGrid.Row, mazeGrid.Column, 0.9f, 0.9f);

        float size2 = CellHeight/4f;
        float size = CellHeight + 1.2f *size2;
        Debug.Log(CellHeight);

        float cx, cy;
        /*
                float a_size = size / 2;
                float b_size = size;
                float height = b_size * 2f;
                float test = (float)(size * Math.Sqrt(3) / 2);*/
        float a_size = size / 4;
        //float b_size = (float)(size/2 * Math.Sqrt(3) / 2);
        float b_size = size/2;
        float height = b_size * 2f;
        Vector3 positionA;
        Vector3 positionB;
        int index = 0;
        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                SquareCell cell = (SquareCell)mazeGrid.Grid[i][j];
                cx = (b_size + cell.Column *height)- ((mazeGrid.Grid[i].Length / 2f) * height);
                cy = size/2 + 3 *  cell.Row * a_size - ((mazeGrid.Row/2f * size) - mazeGrid.Row/2f * a_size) - a_size/2;


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

                

                if ((cell.Neighbours.ContainsKey("West") && !(cell.Linked(cell.Neighbours["West"]))) || !cell.Neighbours.ContainsKey("West"))
                {
                    positionA = new Vector3(x_west_north, y_west_north, -1);
                    positionB = new Vector3(x_west_south, y_west_south, -1);
                    CreateWall(positionA, positionB);
                  /*  text = Instantiate(TextPrefab, new Vector3(x_west_north * 100, y_west_north * 100, 0), Quaternion.identity);
                    text.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform, false);
                    text.transform.localScale = new Vector3(CellHeight * 2 * 0.20f, CellHeight * 2 * 0.20f, CellHeight * 2 * 0.20f);
                    text.text = index.ToString();
                    index++;

                    text = Instantiate(TextPrefab, new Vector3(x_west_south * 100, y_west_south * 100, 0), Quaternion.identity);
                    text.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform, false);
                    text.transform.localScale = new Vector3(CellHeight * 2 * 0.20f, CellHeight * 2 * 0.20f, CellHeight * 2 * 0.20f);
                    text.text = index.ToString();
                    index++;*/
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
