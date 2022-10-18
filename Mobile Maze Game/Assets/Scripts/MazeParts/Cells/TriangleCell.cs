using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MazeParts.Cells
{
    public class TriangleCell : SquareCell
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
        public override List<Cell> GetNeighbours()
        {
            List<Cell> negbours = new List<Cell>();
            if (Neighbours.ContainsKey("West") )
                negbours.Add(Neighbours["West"]);
            if (Neighbours.ContainsKey("East") )
                negbours.Add(Neighbours["East"]);
            if (!Upright() && Neighbours.ContainsKey("North"))
                negbours.Add(Neighbours["North"]);
            if (Upright() && Neighbours.ContainsKey("South"))
                negbours.Add(Neighbours["South"]);
            return negbours;

        }
    }
}
