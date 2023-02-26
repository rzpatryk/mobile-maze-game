using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Factory
{
    public static class GridFactory
    {
        public static MazeGrid CreateGrid(string name, int row, int column)
        {
            MazeGrid mazeGrid = null;
            if (name == "SquareMaze")
            {
                mazeGrid = new SquareGrid(row, column);
            }
            else if (name == "HexGrid")
            {
                mazeGrid = new HexGrid(row, column);
            }
            else if (name == "TriangleGrid")
            {
                mazeGrid = new TriangleGrid(row, column);
            }
            else if (name == "HexShape")
            {
                mazeGrid = new HexShape(row, column);
            }
            else if (name == "TriangleShape")
            {
                mazeGrid = new TriangleMaze(row, column);
            }
            else if (name == "CircleMaze")
            {
                mazeGrid = new PolarGrid(row, column);
            }
            return mazeGrid;
        }
    }
}
