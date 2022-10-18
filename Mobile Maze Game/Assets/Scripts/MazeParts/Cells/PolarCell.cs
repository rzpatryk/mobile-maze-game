using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Cells
{
    public class PolarCell : SquareCell
    {
        private List<Cell> outward;
        private Cell inward;
        public PolarCell(int row, int col) : base(row, col)
        {
            Outward = new List<Cell>();
        }

        public List<Cell> Outward { get => outward; set => outward = value; }
        public Cell Inward { get => inward; set => inward = value; }
    }
}
