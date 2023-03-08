using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class RecursiveDivision : IMazeAlgorithm
    {
        private Random random = new Random();
        private MazeGrid mazeGrid;
        public void CreateMaze(MazeGrid grid)
        {
            mazeGrid = grid;
            List<Cell> neigbours;
            for (int i = 0; i < grid.Grid.Length; i++)
            {
                for (int j = 0; j < grid.Grid[i].Length; j++)
                {
                    neigbours = mazeGrid.Grid[i][j].GetNeighbours();
                    foreach (Cell cell in neigbours)
                    {
                        grid.Grid[i][j].Link(cell, false);
                    }
                }
            }
            Divide(0, 0, grid.Row, grid.Column);
        }
        private void Divide(int row, int column, int height, int width)
        {
            if (height <= 1 || width <= 1)
                return;
            if (height > width)
                DivideHorizontally(row, column, height, width);
            else
                DivideVertically(row, column, height, width);
        }

        private void DivideHorizontally(int row, int column, int height, int width)
        {
            int divideNorthOF = random.Next(height - 1);
            int passageAt = random.Next(width);
            Cell cell;
            for (int i = 0; i < width; i++)
            {
                if (i != passageAt)
                {
                    cell = mazeGrid.Grid[row + divideNorthOF][column + i];
                    cell.Unlink(cell.Neighbours["North"], true);
                }
            }
            Divide(row, column, divideNorthOF + 1, width);
            Divide(row + divideNorthOF + 1, column, height - divideNorthOF - 1, width);

        }

        private void DivideVertically(int row, int column, int height, int width)
        {
            int divideEastOF = random.Next(width - 1);
            int passageAt = random.Next(height);
            Cell cell;
            for (int i = 0; i < height; i++)
            {
                if (i != passageAt)
                {
                    cell = mazeGrid.Grid[row + i][column + divideEastOF];
                    cell.Unlink(cell.Neighbours["East"], true);
                }
            }
            Divide(row, column, height, divideEastOF + 1);
            Divide(row, column + divideEastOF + 1, height, width - divideEastOF - 1);
        }
    }
}
