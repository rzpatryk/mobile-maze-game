using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class SquareGrid : Grid
    {
        public SquareGrid(int row, int column) : base(row, column)
        {

        }

        public override void PrepareGrid()
        {
            MazeGrid = new SquareCell[Row][];
            for (int r = 0; r < Row; r++)
            {
                MazeGrid[r] = new SquareCell[Column];
                for (int c = 0; c < Column; c++)
                {
                    MazeGrid[r][c] = new SquareCell(r, c);
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
                    SquareCell cell = (SquareCell)MazeGrid[r][c];
                    if (isOnGrid(r - 1, c) && MazeGrid[r - 1][c].IsOn)
                    {
                        cell.Neighbours.Add("South", MazeGrid[r - 1][c]);
                    }
                    if (isOnGrid(r + 1, c) && MazeGrid[r + 1][c].IsOn)
                    {
                        cell.Neighbours.Add("North", MazeGrid[r + 1][c]);
                    }
                    if (isOnGrid(r, c + 1) && MazeGrid[r][c].IsOn)
                    {
                        cell.Neighbours.Add("East", MazeGrid[r][c + 1]);
                    }
                    if (isOnGrid(r, c - 1) && MazeGrid[r][c - 1].IsOn)
                    {
                        cell.Neighbours.Add("West", MazeGrid[r][c - 1]);
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
            if (!MazeGrid[row][col].IsOn)
                return false;
            return true;
        }
    }
}
