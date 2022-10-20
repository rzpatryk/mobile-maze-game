using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public class PolarGrid : MazeGrid
    {
        public PolarGrid(int row, int column) : base(row, column)
        {
        }

        public override void PrepareGrid()
        {
            Grid = new PolarCell[Row + 1][];
            float radius;
            float circumference;
            float previous_count;
            float estimated_cell_width;
            float ratio;
            float row_height = 1.0f / Row;
            Grid[0] = new PolarCell[1];
            Grid[0][0] = new PolarCell(0, 0);
            for (int i = 1; i <= Row; i++)
            {

                radius = (float)i / Row;
                circumference = ((float)(2 * Math.PI * radius));

                previous_count = Grid[i - 1].Length;
                estimated_cell_width = circumference / previous_count;
                ratio = (float)Math.Round((estimated_cell_width / row_height));
                int t = (int)(previous_count * ratio);
                Grid[i] = new PolarCell[t];
                for (int j = 0; j < t; j++)
                {
                    Grid[i][j] = new PolarCell(i, j);
                }

            }
        }

        public override void ConfigureNeighbours()
        {
            int row, col, ratio, v;
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    PolarCell cell = (PolarCell)Grid[i][j];
                    row = cell.Row;
                    col = cell.Column;

                    if (row > 0)
                    {
                        if (col < Grid[row].Length - 1)
                        {
                            cell.Neighbours.Add("Cw", Grid[row][col + 1]);
                        }


                        if (col > 0)
                        {
                            cell.Neighbours.Add("Ccw", Grid[row][col - 1]);
                        }
                        else
                        {
                            cell.Neighbours.Add("Ccw", Grid[row][col]);
                        }

                        ratio = Grid[row].Length / Grid[row - 1].Length;
                        v = (col / ratio);
                        PolarCell parent = (PolarCell)Grid[row - 1][v];
                        //parent.Neighbours.Add("Outward", cell);
                        parent.Outward.Add(cell);
                        //cell.Neighbours.Add("Inward", parent);
                        cell.Neighbours.Add("Inward", parent);
                        cell.Inward = parent;
                    }
                }
            }
            
        }

        public override Cell GetRandomCell()
        {
            int r, c;
            do
            {
                r = GetRandomNumber(1, Row + 1);
                c = GetRandomNumber(0, Grid[r].Length);
            } while (!Grid[r][c].IsOn);

            return Grid[r][c];
        }
    }
}
