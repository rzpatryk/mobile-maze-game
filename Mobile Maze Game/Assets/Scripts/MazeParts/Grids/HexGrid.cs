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
            int north_diagonal, south_diagonal;
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    SquareCell cell = (SquareCell)Grid[i][j];
                    int row = cell.Row;
                    int col = cell.Column;
                    if (cell.IsOn)
                    {
                        if (col % 2 == 0)
                        {
                            north_diagonal = row;
                            south_diagonal = row - 1;
                            if (row > 0)
                            {
                                if (col > 0 && Grid[south_diagonal][col - 1].IsOn)
                                    cell.Neighbours.Add("SouthWest", Grid[south_diagonal][col - 1]);


                                if (col < Column - 1 && Grid[south_diagonal][col + 1].IsOn)
                                    cell.Neighbours.Add("SouthEast", Grid[south_diagonal][col + 1]);
                            }
                            if (col > 0 && Grid[north_diagonal][col - 1].IsOn)
                                cell.Neighbours.Add("NorthWest", Grid[north_diagonal][col - 1]);


                            if (col < Column - 1 && Grid[north_diagonal][col + 1].IsOn)
                                cell.Neighbours.Add("NorthEast", Grid[north_diagonal][col + 1]);
                        }
                        else
                        {
                            north_diagonal = row + 1;
                            south_diagonal = row;

                            if (row < Row - 1)
                            {
                                if (col > 0 && Grid[north_diagonal][col - 1].IsOn)
                                    cell.Neighbours.Add("NorthWest", Grid[north_diagonal][col - 1]);


                                if (col < Column - 1 && Grid[north_diagonal][col + 1].IsOn)
                                    cell.Neighbours.Add("NorthEast", Grid[north_diagonal][col + 1]);

                            }
                            if (col > 0 && Grid[south_diagonal][col - 1].IsOn)
                                cell.Neighbours.Add("SouthWest", Grid[south_diagonal][col - 1]);

                            if (col < Column - 1 && Grid[south_diagonal][col + 1].IsOn)
                                cell.Neighbours.Add("SouthEast", Grid[south_diagonal][col + 1]);
                        }
                        if (row > 0 && Grid[row - 1][col].IsOn)
                            cell.Neighbours.Add("South", Grid[row - 1][col]);

                        if (row < Row - 1 && Grid[row + 1][col].IsOn)
                            cell.Neighbours.Add("North", Grid[row + 1][col]);
                    }
                }
            }
        }
    }
}
