using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class AldousBroder : IMazeAlgorithm
    {   
        public void CreateMaze(MazeGrid grid)
        {
            int random;
            Cell neighbor;
            Cell cell;
            List<Cell> neighbors = new List<Cell>();
            cell = grid.GetRandomCell();
            cell.Visited = true;
            int unvisited = grid.UnvisitedCellsCount();
            while (unvisited > 0)
            {

                neighbors = cell.GetNeighbours();
                random = grid.GetRandomNumber(0, neighbors.Count());
                neighbor = neighbors[random];
                if (neighbor.Visited == false)
                {
                    cell.Link(neighbor);
                    neighbor.Visited = true;
                    unvisited--;
                }
                cell = neighbor;
            }
        }
    }
}

