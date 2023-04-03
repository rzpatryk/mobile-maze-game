using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class HuntAndKill : IMazeAlgorithm
    {
        public void CreateMaze(MazeGrid grid)
        {
            List<Cell> visitedNeighbours = new List<Cell>();
            List<Cell> unvisitedNeighbours;
            List<Cell> unvisitedCells;
            Cell neighbour;
            Cell cell;
            int randomNumber;
            Cell current = grid.GetRandomCell();
            while (current != null)
            {
                current.Visited = true;
                unvisitedNeighbours = current.GetUnvisitedNeighbours();
                if (unvisitedNeighbours.Count > 0)
                {
                    randomNumber = grid.GetRandomNumber(0, unvisitedNeighbours.Count());
                    neighbour = unvisitedNeighbours[randomNumber];
                    current.Link(neighbour);
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
                        if (cell.Visited == false && visitedNeighbours.Count() > 0)
                        {
                            current = cell;
                        }
                        index++;
                    }
                    if (current != null)
                    {
                        randomNumber = grid.GetRandomNumber(0, visitedNeighbours.Count());
                        neighbour = visitedNeighbours[randomNumber];
                        current.Link(neighbour);
                    }
                    visitedNeighbours.Clear();
                }
                unvisitedNeighbours.Clear();
            }
        }
    }
}

