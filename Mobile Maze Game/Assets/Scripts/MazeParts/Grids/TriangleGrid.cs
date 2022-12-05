using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class TriangleGrid : MazeGrid
    {
        public TriangleGrid(int row, int column) : base(row, column)
        {
        }


        public override void PrepareGrid()
        {
            Grid = new TriangleCell[Row][];
            for (int i = 0; i < Row; i++)
            {
                Grid[i] = new TriangleCell[Column];
                for (int j = 0; j < Column; j++)
                {
                    Grid[i][j] = new TriangleCell(i, j);
                }
            }
           
        }


        public override void ConfigureNeighbours()
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    TriangleCell cell = (TriangleCell)Grid[i][j];

                    if(IsOnGrid(i, j - 1))
                    {
                        cell.Neighbours.Add("West", Grid[i][j - 1]);
                    }
                    if (IsOnGrid(i,j + 1))
                    {
                        cell.Neighbours.Add("East", Grid[i][j + 1]);
                    }
                    if (!cell.Upright() && IsOnGrid(i+1, j))
                        cell.Neighbours.Add("North", Grid[i + 1][j]);
                    else if(cell.Upright() && IsOnGrid(i-1, j))
                    {
                        cell.Neighbours.Add("South", Grid[i - 1][j]);
                    }
                }
            }
        }
    }
}
