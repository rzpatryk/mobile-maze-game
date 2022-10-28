using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class HexShape : HexGrid
    {
        public HexShape(int row, int column) : base(row, column)
        {
        }

        public override void PrepareGrid()
        {
            Grid = new SquareCell[Row][];
            for (int i = 0; i < (Row / 2) + 1; i++)
            {
                int test = ((Column / 2) + 1) + i;
                Grid[i] = new SquareCell[test];
                for (int j = 0; j < test; j++)
                {
                    Grid[i][j] = new SquareCell(i, j);
                }
            }
            for (int i = (Row / 2) + 1, k = 1; i < Row; i++, k++)
            {
                int test = Column - k;
                Grid[i] = new SquareCell[test];
                for (int j = 0; j < test; j++)
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

                  

                    if (IsOnGrid(row, col - 1))
                    {
                        cell.Neighbours.Add("West", Grid[row][col - 1]);
                    }
                    if (IsOnGrid(row, col + 1))
                    {
                        cell.Neighbours.Add("East", Grid[row][col + 1]);
                    }

                    if (row < Grid.Length / 2)
                    {
                        if (IsOnGrid(row + 1, col))
                        {
                            cell.Neighbours.Add("NorthWest", Grid[row + 1][col]);
                        }
                        if (IsOnGrid(row + 1, col + 1))
                        {
                            cell.Neighbours.Add("NorthEast", Grid[row + 1][col + 1]);
                        }
                        if (IsOnGrid(row - 1, col - 1))
                        {
                            cell.Neighbours.Add("SouthWest", Grid[row - 1][col - 1]);
                        }

                        if (IsOnGrid(row - 1, col))
                        {
                            cell.Neighbours.Add("SouthEast", Grid[row - 1][col]);
                        }
                    }
                    else
                    {
                        if (IsOnGrid(row + 1, col - 1))
                        {
                            cell.Neighbours.Add("NorthWest", Grid[row + 1][col - 1]);
                        }
                        if (IsOnGrid(row + 1, col))
                        {
                            cell.Neighbours.Add("NorthEast", Grid[row + 1][col]);
                        }

                        if (row == (Grid.Length / 2))
                        {
                            if (IsOnGrid(row - 1, col - 1))
                            {
                                cell.Neighbours.Add("SouthWest", Grid[row - 1][col - 1]);
                            }

                            if (IsOnGrid(row - 1, col))
                            {
                                cell.Neighbours.Add("SouthEast", Grid[row - 1][col]);
                            }
                        }
                        else
                        {
                            if (IsOnGrid(row - 1, col))
                            {
                                cell.Neighbours.Add("SouthWest", Grid[row - 1][col]);
                            }

                            if (IsOnGrid(row - 1, col + 1))
                            {
                                cell.Neighbours.Add("SouthEast", Grid[row - 1][col + 1]);
                            }
                        }
                    }

                }
            }
        }
    }
}
