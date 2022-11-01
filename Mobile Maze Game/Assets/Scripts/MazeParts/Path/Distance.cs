using Assets.Scripts.MazeParts.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Path
{
    public class Distance
    {
        private Cell root;
        private Dictionary<Cell, int> Cells;

        public Dictionary<Cell, int> Cells1 { get => Cells; set => Cells = value; }

        public Distance(Cell root)
        {
            this.root = root;
            Cells = new Dictionary<Cell, int>();
            Cells.Add(root, 0);
        }

        public void AddCell(Cell cell, int distance)
        {
            Cells.Add(cell, distance);
        }
        public int GetValue(Cell cell)
        {
            return Cells[cell];
        }
        public bool CellIn(Cell cell)
        {
            if (Cells.ContainsKey(cell))
            {
                return true;
            }
            return false;
        }

        public Cell Max()
        {
            int maxDistance = 0;
            Cell maxCell = root;

            foreach (Cell key in Cells.Keys)
            {
                if (Cells[key] > maxDistance)
                {
                    maxCell = key;
                    maxDistance = Cells[key];
                }
            }

            //Cell maxCell = Cells.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            return maxCell;
        }

    }
}
