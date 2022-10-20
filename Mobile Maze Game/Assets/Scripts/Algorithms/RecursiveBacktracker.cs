using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RecursiveBacktracker : IMazeAlgorithm
{
    private Stack<Cell> cellStack;
    private List<Cell> unvisitedNeighbours;
    private Cell cell;
    private Cell neighbour;
    public void CreateMaze(MazeGrid grid)
    {
        unvisitedNeighbours = new List<Cell>();
        cellStack = new Stack<Cell>();
        /*cellStack.Push(grid.GetRandomCell());*/
        cellStack.Push(grid.Grid[0][0]);

        while (cellStack.Count > 0 && cellStack.Peek() != null)
        {
            cell = cellStack.Peek();

            unvisitedNeighbours = cell.GetUnvisitedNeighbours();
            if (unvisitedNeighbours.Count == 0)
            {
                cellStack.Pop();
            }
            else
            {
                neighbour = unvisitedNeighbours[grid.GetRandomNumber(0, unvisitedNeighbours.Count)];
                cell.Link(neighbour, true);
                cellStack.Push(neighbour);
            }
            unvisitedNeighbours.Clear();
        }
    }
}

