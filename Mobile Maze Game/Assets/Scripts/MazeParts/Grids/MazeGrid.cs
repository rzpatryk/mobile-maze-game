using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Grids
{
    public abstract class MazeGrid
    {
        private int row;
        private int column;
        private Cell[][] grid;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public Cell[][] Grid { get => grid; set => grid = value; }

        public MazeGrid(int row, int column)
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
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    if (Grid[i][j].Visited == false)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public virtual List<Cell> GetUnvisitedCells()
        {
            List<Cell> unvisitedCells = new List<Cell>();
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    if (Grid[i][j].Visited == false)
                    {
                        unvisitedCells.Add(Grid[i][j]);
                    }
                }
            }
            return unvisitedCells;
        }
        public virtual Cell GetRandomCell()
        {
            int r, c;
            r = GetRandomNumber(0, Row);
            c = GetRandomNumber(0, Grid[r].Length);


            return Grid[r][c];
        }

        public bool IsOnGrid(int row, int col)
        {
            if (row < 0 || row > Row - 1)
                return false;
            if (col < 0 || col > Grid[row].Length - 1)
                return false;
            return true;
        }

        private List<Cell> GetDeadEnds()
        {
            List<Cell> deadEnds = new List<Cell>();
            for(int i = 0; i < Grid.Length; i++)
            {
                for(int j = 0; j < Grid[i].Length; j++)
                {
                    if(Grid[i][j].Links.Count == 1)
                    {
                        deadEnds.Add(Grid[i][j]);
                    }
                }
            }

            return deadEnds;
        }

        public void RemoveDeadEnds()
        {
            List<Cell> deadEnds = GetDeadEnds();
            List<Cell> neighbours;
            int randomNumber;
            foreach(Cell cell in deadEnds)
            {
                randomNumber = GetRandomNumber(0, 3);
                if(randomNumber == 0)
                {
                    neighbours = cell.Neighbours.Values.Where(p => !p.Linked(cell)).ToList();
                    if(neighbours.Count > 0)
                    {
                        Cell neighbour = null;
                        foreach (Cell cellNeigbour in neighbours)
                        {
                            if (cellNeigbour.Links.Count == 1)
                            {
                                neighbour = cellNeigbour;
                            }
                        }
                        if (neighbour == null)
                        {
                            neighbour = neighbours[GetRandomNumber(0, neighbours.Count)];
                        }
                        cell.Link(neighbour, true);
                    }
                }
            }
            
        }
    }

}
