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
            Grid = new SquareCell[Row][];
            for (int r = 0; r < Row; r++)
            {
                Grid[r] = new SquareCell[Column];
                for (int c = 0; c < Column; c++)
                {
                    Grid[r][c] = new SquareCell(r, c);
                }
            }
            //MaskGrid();
        }

        public override void ConfigureNeighbours()
        {
            for (int r = 0; r < Row; r++)
            {
                for (int c = 0; c < Column; c++)
                {
                    SquareCell cell = (SquareCell)Grid[r][c];
                    if (isOnGrid(r - 1, c) && Grid[r - 1][c].IsOn)
                    {
                        cell.Neighbours.Add("South", Grid[r - 1][c]);
                    }
                    if (isOnGrid(r + 1, c) && Grid[r + 1][c].IsOn)
                    {
                        cell.Neighbours.Add("North", Grid[r + 1][c]);
                    }
                    if (isOnGrid(r, c + 1) && Grid[r][c].IsOn)
                    {
                        cell.Neighbours.Add("East", Grid[r][c + 1]);
                    }
                    if (isOnGrid(r, c - 1) && Grid[r][c - 1].IsOn)
                    {
                        cell.Neighbours.Add("West", Grid[r][c - 1]);
                    }
                }
            }
        }

        private bool isOnGrid(int row, int col)
        {
            if (row < 0 || row > Row - 1)
                return false;
            if (col < 0 || col > Column - 1)
                return false;
            if (!Grid[row][col].IsOn)
                return false;
            return true;
        }
    }
}
