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

    private List<Cell> visitedNeighbours;
    private List<Cell> unvisitedNeighbours;
    private int number;
    public void CreateMaze(MazeGrid grid)
    {
        visitedNeighbours = new List<Cell>();
        unvisitedNeighbours = new List<Cell>();

        current = grid.GetRandomCell();
        while (current != null)
        {
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

                for (int r = 0; r < grid.Grid.Length; r++)
                {
                    for (int c = 0; c < grid.Grid[r].Length; c++)
                    {
                        if (grid.Grid[r][c].IsOn)
                        {
                            visitedNeighbours = grid.Grid[r][c].GetVisitedNeighbours();
                            if (grid.Grid[r][c].Links.Count() == 0 && visitedNeighbours.Count() > 0)
                            {
                                current = grid.Grid[r][c];
                                number = grid.GetRandomNumber(0, visitedNeighbours.Count());
                                neighbour = visitedNeighbours[number];
                                current.Link(neighbour, true);
                            }

                            visitedNeighbours.Clear();
                        }
                    }
                }
            }
            unvisitedNeighbours.Clear();
        }
    }
}

