using Assets.Scripts.MazeParts.Path;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MazeParts.Cells
{
    public class Cell
    {
        private int row;
        private int column;
        private List<Cell> links;
        private Dictionary<string, Cell> neighbours;
        private bool visited;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public List<Cell> Links { get => links; set => links = value; }
        public bool Visited { get => visited; set => visited = value; }
        public Dictionary<string, Cell> Neighbours { get => neighbours; set => neighbours = value; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Visited = false;
            Links = new List<Cell>();
            Neighbours = new Dictionary<string, Cell>();
        }

        public virtual List<Cell> GetNeighbours()
        {
            List<Cell> neighbours = new List<Cell>();
            foreach (Cell cell in Neighbours.Values)
            {
                neighbours.Add(cell);
            }
            return neighbours;
        }

        public void Link(Cell cell, bool bidirectional = true)
        {
            Links.Add(cell);
            if (bidirectional) cell.Link(this, false);
        }

        public void Unlink(Cell cell, bool bidirectional = true)
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
            foreach(Cell cell in neighbours)
            {
                if (cell.Visited == false)
                {
                    unvisitedNeighbours.Add(cell);
                }
            }
            return unvisitedNeighbours;
        }
        public virtual List<Cell> GetVisitedNeighbours()
        {
            List<Cell> VisitedNeighbours = new List<Cell>();
            List<Cell> neighbours = GetNeighbours();
            foreach (Cell cell in neighbours)
            {
                if (cell.Visited)
                {
                    VisitedNeighbours.Add(cell);
                }
            }
            return VisitedNeighbours;
        }

        public Distance Distances()
        {
            Distance distance = new Distance(this);
            Dictionary<Cell, int> pending = new Dictionary<Cell, int>();
            pending.Add(this, 0);
            while (pending.Count > 0)
            {
                Cell cell = pending.OrderBy(kvp => kvp.Value).First().Key;
                pending.Remove(cell);

                foreach (Cell neighbour in cell.Links)
                {
                    int total_weight = distance.GetValue(cell) + 1;
                    if (!distance.Cells.ContainsKey(neighbour) || total_weight < distance.GetValue(cell))
                    {
                        pending[neighbour] = total_weight;
                        distance.Cells[neighbour] = total_weight;
                    }
                }
            }

            return distance;
        }
    }
}
