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
            int ratio, v;
            for (int i = 1; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    PolarCell cell = (PolarCell)Grid[i][j];
                    if (i > 0)
                    {
                        if (j < Grid[i].Length - 1)
                        {
                            cell.Neighbours.Add("Cw", Grid[i][j + 1]);
                        }


                        if (j > 0)
                        {
                            cell.Neighbours.Add("Ccw", Grid[i][j - 1]);
                        }
                        else
                        {
                            cell.Neighbours.Add("Ccw", Grid[i][j]);
                        }
                        if (i > 1)
                        {
                            ratio = Grid[i].Length / Grid[i - 1].Length;
                            v = (j / ratio);
                            PolarCell parent = (PolarCell)Grid[i - 1][v];
                            parent.Outward.Add(cell);
                            cell.Neighbours.Add("Inward", parent);
                        }
                        
                    }
                }
            }

        }

        public override List<Cell> GetUnvisitedCells()
        {
            List<Cell> unvisitedCells = base.GetUnvisitedCells();
            unvisitedCells.Remove(Grid[0][0]);
            return unvisitedCells;
        }

        public override int UnvisitedCellsCount()
        {
            int count = base.UnvisitedCellsCount();
            return count - 1;
        }

        public override Cell GetRandomCell()
        {
            int r, c;

            r = GetRandomNumber(1, Row + 1);
            c = GetRandomNumber(0, Grid[r].Length);


            return Grid[r][c];
        }
    }
}
