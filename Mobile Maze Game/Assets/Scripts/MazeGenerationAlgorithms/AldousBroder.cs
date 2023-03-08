using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class AldousBroder : IMazeAlgorithm
    {
        private List<Cell> neighbors;
        private Cell neighbor;
        private int random;
        private int unvisited;
        private Cell cell;
        public void CreateMaze(MazeGrid grid)
        {
            neighbors = new List<Cell>();
            cell = grid.GetRandomCell();
            cell.Visited = true;
            unvisited = grid.UnvisitedCellsCount();
            while (unvisited > 0)
            {

                neighbors = cell.GetNeighbours();
                random = grid.GetRandomNumber(0, neighbors.Count());
                neighbor = neighbors[random];
                if (neighbor.Visited == false)
                {
                    cell.Link(neighbor, true);
                    neighbor.Visited = true;
                    unvisited--;
                }
                cell = neighbor;

            }
        }
    }
}

