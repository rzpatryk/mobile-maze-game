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
                    int row = cell.Row;
                    int col = cell.Column;

                    if (col > 0 )
                        cell.Neighbours.Add("West", Grid[row][col - 1]);
                    if (col < Column - 1 )
                        cell.Neighbours.Add("East", Grid[row][col + 1]);
                    if (!cell.Upright() && row < Row - 1)
                        cell.Neighbours.Add("North", Grid[row + 1][col]);
                    else if (row > 0 )
                        cell.Neighbours.Add("South", Grid[row - 1][col]);
                }
            }
        }
    }
}
