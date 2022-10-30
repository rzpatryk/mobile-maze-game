using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HuntAndKill : IMazeAlgorithm
{
    private Cell current;
    private Cell neighbour;
    private Cell cell;
    private List<Cell> visitedNeighbours;
    private List<Cell> unvisitedNeighbours;
    private List<Cell> unvisitedCells;
    private int number;
    public void CreateMaze(MazeGrid grid)
    {
        visitedNeighbours = new List<Cell>();
        unvisitedNeighbours = new List<Cell>();
        unvisitedCells = new List<Cell>(); ;
        current = grid.GetRandomCell();
        while (current != null)
        {
            current.Visited = true;
            unvisitedNeighbours = current.GetUnvisitedNeighbours();
            if (unvisitedNeighbours.Count > 0)
            {
                number = grid.GetRandomNumber(0, unvisitedNeighbours.Count());
                neighbour = unvisitedNeighbours[number];
                current.Link(neighbour, true);
                current = neighbour;
            }
            else
            {
                current = null;
                int index = 0;
                unvisitedCells = grid.GetUnvisitedCells();
                while (current == null && index < unvisitedCells.Count())
                {
                    cell = unvisitedCells[index];
                    visitedNeighbours = cell.GetVisitedNeighbours();
                    if (cell.Visited == false  && visitedNeighbours.Count() > 0)
                    {
                        current = cell;
                    }
                    index++;
                }
                if (current!=null)
                {
                    number = grid.GetRandomNumber(0, visitedNeighbours.Count());
                    neighbour = visitedNeighbours[number];
                    current.Link(neighbour, true);
                }
                visitedNeighbours.Clear();
            }
            unvisitedNeighbours.Clear();
        }
    }
}

