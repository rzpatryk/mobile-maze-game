using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class TriangleMaze : MazeGrid
    {
        public TriangleMaze(int row, int column) : base(row, column)
        {
        }

        public override void PrepareGrid()
        {
            int count = CalculateCol();
            Row = Column - count;
            Grid = new TriangleCell[Column - count][];
            for (int i = 0; i < Column - count; i++)
            {
                Grid[i] = new TriangleCell[Column - i - i];
                for (int j = 0; j < Column - i - i; j++)
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
                    if(IsOnGrid(i, j + 1))
                    {
                        cell.Neighbours.Add("East", Grid[i][j + 1]);
                    }
                    if(cell.Updown() && IsOnGrid(i+1, j - 1))
                    {
                        cell.Neighbours.Add("North", Grid[i + 1][j - 1]);
                    }
                    else if(!cell.Updown() && IsOnGrid(i-1, j + 1))
                    {
                        cell.Neighbours.Add("South", Grid[i - 1][j + 1]);
                    }

                }
            }
        }

        private int CalculateCol()
        {
            int tempCol = Column;
            int count = 0;
            while (tempCol > 1)
            {
                tempCol -= 2;
                count++;
            }
            return count;
        }

    }
}
