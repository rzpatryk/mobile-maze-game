using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Wilson : IMazeAlgorithm
{

    private Cell first;
    private Cell cell;
    private int position;
    private int random;
    private List<Cell> unvisited = new List<Cell>();
    private List<Cell> path = new List<Cell>();
    private List<Cell> neighbors = new List<Cell>();
    public void CreateMaze(MazeGrid grid)
    { 
        for (int i = 0; i < grid.Grid.Length; i++)
        {
            for (int j = 0; j < grid.Grid[i].Length; j++)
            {
                if (grid.Grid[i][j].IsOn)
                    unvisited.Add(grid.Grid[i][j]);
            }
        }

        first = unvisited[grid.GetRandomNumber(0, unvisited.Count())];
        unvisited.Remove(first);

        while (unvisited.Count() > 0)
        {
            cell = unvisited[grid.GetRandomNumber(0, unvisited.Count())];
            path.Add(cell);
            while (unvisited.Contains(cell))
            {
                neighbors = cell.GetNeighbours();
                random = grid.GetRandomNumber(0, neighbors.Count);
                cell = neighbors[random];
                if (path.Contains(cell))
                {
                    position = path.IndexOf(cell);
                    path = path.GetRange(0, position + 1);
                }
                else
                {
                    path.Add(cell);
                }
            }
            for (int i = 0; i < path.Count() - 1; i++)
            {
                path[i].Link(path[i + 1], true);
                unvisited.Remove(path[i]);
            }
            path.Clear();
        }
    }
}

