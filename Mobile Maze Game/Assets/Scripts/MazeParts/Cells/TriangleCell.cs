using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Cells
{
    public class TriangleCell : Cell
    {
        public TriangleCell(int row, int col) : base(row, col)
        {
        }

        public bool Updown()
        {
            return Column % 2 != 0;

        }

        public bool Upright()
        {
            return (Row + Column) % 2 == 0;
        }
    }
}
