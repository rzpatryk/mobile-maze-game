using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using Assets.Scripts.MazeParts.Path;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTriangleShapeMaze : DisplaySquareMaze
{

   /* public GameObject textPrefab;
    private GameObject text;*/
    public override void DisplayMaze(MazeGrid mazeGrid)
    {
        WallScale = 0.1f;
        SetCellSize(mazeGrid.Row, mazeGrid.Column, 0.9f, 0.8f);
        float width = CellWidth;
        float halfWidth = width / 2f;
        float height = CellHeight;
        float halfHeight = height / 2f;

        float cx, cy;
        float westX, midX, eastX;
        float apexY, baseY;
        Vector3 positionA;
        Vector3 positionB;

        Distance distance = mazeGrid.Grid[0][0].Distances();
        Cell maxCell = distance.Max();
        for (int i = 0; i < mazeGrid.Grid.Length; i++)
        {
            for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
            {
                TriangleCell cell = (TriangleCell)mazeGrid.Grid[i][j];

                cx = width + cell.Column * width - (mazeGrid.Column * width / 2) - width / 2;

                cy = halfHeight + cell.Row * height - ((mazeGrid.Row * halfHeight));
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

                if (distance.CellIn(cell))
                {
                    if (cell.Updown())
                    {
                        text = Instantiate(textPrefab, new Vector3(midX + (width * i), baseY - (height/2), -2), Quaternion.identity);
                    }
                    else
                    {
                        text = Instantiate(textPrefab, new Vector3(midX + (width * i), baseY  + (height/2), -2), Quaternion.identity);
                    }
                    text.transform.SetParent(GameObject.FindGameObjectWithTag("Maze").transform, false);
                    text.transform.localScale = new Vector3(CellHeight , CellHeight , CellHeight);
                    text.GetComponent<TextMeshPro>().text = distance.GetValue(cell).ToString();
                }

                if(i == 0 && j == 0)
                {
                    CreatePlayer(new Vector3(midX, baseY + halfHeight, -2));
                    CreateStartImage(midX - width*2f, baseY + CellHeight);
                }

                if(cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                {
                    if (cell.Updown())
                    {
                        CreateEndImage(midX + (width * i), baseY - halfHeight);
                    }
                    else
                    {
                        CreateEndImage(midX + (width * i), baseY + halfHeight);
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
