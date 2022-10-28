using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class HexGrid : MazeGrid
    {
        public HexGrid(int row, int column) : base(row, column)
        {

        }

        public override void PrepareGrid()
        {
            Grid = new SquareCell[Row][];
            for (int i = 0; i < Row; i++)
            {
                Grid[i] = new SquareCell[Column];
                for (int j = 0; j < Column; j++)
                {
                    Grid[i][j] = new SquareCell(i, j);
                }
            }
        }
        public override void ConfigureNeighbours()
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    SquareCell cell = (SquareCell)Grid[i][j];
                    int row = cell.Row;
                    int col = cell.Column;

                    if (col % 2 == 0)
                    {
                        if(IsOnGrid(row-1, col - 1))
                        {
                            cell.Neighbours.Add("SouthWest", Grid[row-1][col - 1]);
                        }
                        if(IsOnGrid(row-1, col + 1))
                        {
                            cell.Neighbours.Add("SouthEast", Grid[row-1][col + 1]);
                        }
                        if(IsOnGrid(row, col - 1))
                        {
                            cell.Neighbours.Add("NorthWest", Grid[row][col - 1]);
                        }
                        if(IsOnGrid(row, col+1))
                        {
                            cell.Neighbours.Add("NorthEast", Grid[row][col + 1]);
                        }

                    }
                    else
                    {
                        if(IsOnGrid(row+1, col - 1))
                        {
                            cell.Neighbours.Add("NorthWest", Grid[row+1][col - 1]);
                        }
                        if(IsOnGrid(row+1, col+1))
                        {
                            cell.Neighbours.Add("NorthEast", Grid[row+1][col + 1]);
                        }
                        if(IsOnGrid(row, col - 1))
                        {
                            cell.Neighbours.Add("SouthWest", Grid[row][col - 1]);
                        }
                        if (IsOnGrid(row, col + 1))
                        {
                            cell.Neighbours.Add("SouthEast", Grid[row][col + 1]);
                        }
                    }
                    if(IsOnGrid(row-1, col))
                    {
                        cell.Neighbours.Add("South", Grid[row - 1][col]);
                    }
                    if(IsOnGrid(row+1, col))
                    {
                        cell.Neighbours.Add("North", Grid[row + 1][col]);
                    }

                }
            }
        }
    }
}
