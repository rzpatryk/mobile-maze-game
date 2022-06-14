using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public abstract class Grid
    {
        private int row;
        private int column;
        private Cell[][] mazeGrid;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public Cell[][] MazeGrid { get => mazeGrid; set => mazeGrid = value; }

        public Grid(int row, int column)
        {
            Row = row;
            Column = column;
            PrepareGrid();
            ConfigureNeighbours();

        }
        public abstract void PrepareGrid();
        public abstract void ConfigureNeighbours();

        public int GetRandomNumber(int low, int high)
        {
            return UnityEngine.Random.Range(low, high);
        }
        public virtual int UnvisitedCellsCount()
        {
            int count = 0;
            for (int i = 0; i < MazeGrid.Length; i++)
            {
                for (int j = 0; j < MazeGrid[i].Length; j++)
                {
                    if (MazeGrid[i][j].IsOn)
                    {
                        count++;
                    }
                }
            }
            return count - 1;
        }
        public virtual Cell GetRandomCell()
        {
            int r, c;
            do
            {
                r = GetRandomNumber(0, Row - 1);
                c = GetRandomNumber(0, Column - 1);
            } while (!MazeGrid[r][c].IsOn);

            return MazeGrid[r][c];
        }
    }

}
