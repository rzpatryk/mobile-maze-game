using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class SquareGrid : MazeGrid
    {
        public SquareGrid(int row, int column) : base(row, column)
        {

        }

        public override void PrepareGrid()
        {
            Grid = new Cell[Row][];
            for (int r = 0; r < Row; r++)
            {
                Grid[r] = new Cell[Column];
                for (int c = 0; c < Column; c++)
                {
                    Grid[r][c] = new Cell(r, c);
                }
            }
        }

        public override void ConfigureNeighbours()
        {
            for (int r = 0; r < Row; r++)
            {
                for (int c = 0; c < Column; c++)
                {
                    Cell cell = Grid[r][c];
                    if (IsOnGrid(r - 1, c))
                    {
                        cell.Neighbours.Add("South", Grid[r - 1][c]);
                    }
                    if (IsOnGrid(r + 1, c))
                    {
                        cell.Neighbours.Add("North", Grid[r + 1][c]);
                    }
                    if (IsOnGrid(r, c + 1))
                    {
                        cell.Neighbours.Add("East", Grid[r][c + 1]);
                    }
                    if (IsOnGrid(r, c - 1))
                    {
                        cell.Neighbours.Add("West", Grid[r][c - 1]);
                    }
                }
            }
        }
    }
}
