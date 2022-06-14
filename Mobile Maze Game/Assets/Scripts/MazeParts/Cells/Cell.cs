using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Cells
{
    public abstract class Cell
    {
        private int row;
        private int column;
        private List<Cell> links;
        private bool isOn;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public List<Cell> Links { get => links; set => links = value; }
        public bool IsOn { get => isOn; set => isOn = value; }

        protected Cell(int row, int column)
        {
            Row = row;
            Column = column;
            IsOn = true;
            Links = new List<Cell>();
        }

        public abstract List<Cell> GetNeighbours();

        public void Link(Cell cell, bool bidirectional)
        {
            Links.Add(cell);
            if (bidirectional) cell.Link(this, false);
        }

        public void Unlink(Cell cell, bool bidirectional)
        {
            Links.Remove(cell);
            if (bidirectional) cell.Unlink(this, false);
        }
        public bool Linked(Cell cell)
        {
            if (Links.Contains(cell))
            {
                return true;
            }
            return false;
        }
        public List<Cell> GetUnvisitedNeighbours()
        {
            List<Cell> unvisitedNeighbours = new List<Cell>();
            List<Cell> neighbours = GetNeighbours();
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].Links.Count == 0)
                {
                    unvisitedNeighbours.Add(neighbours[i]);
                }
            }
            return unvisitedNeighbours;
        }
        public List<Cell> GetVisitedNeighbours()
        {
            List<Cell> VisitedNeighbours = new List<Cell>();
            List<Cell> neighbours = GetNeighbours();
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].Links.Count != 0)
                {
                    VisitedNeighbours.Add(neighbours[i]);
                }
            }
            return VisitedNeighbours;
        }
    }
}
