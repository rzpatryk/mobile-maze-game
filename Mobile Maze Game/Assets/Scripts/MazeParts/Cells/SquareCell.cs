using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Cells
{
    public class SquareCell : Cell
    {
        public Dictionary<string, Cell> Neighbours;

        public SquareCell(int row, int col) : base(row, col)
        {
            Neighbours = new Dictionary<string, Cell>();
            
        }

        public override List<Cell> GetNeighbours()
        {
            List<Cell> neighbours = new List<Cell>();
            foreach (Cell cell in Neighbours.Values)
            {
                neighbours.Add(cell);
            }
            return neighbours;
        }
    }
}
