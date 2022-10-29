using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BinaryTree : IMazeAlgorithm
{
    private int randomNbr;
    private List<Cell> neighbors = new List<Cell>();
    public void CreateMaze(MazeGrid grid)
    {
        for (int i = 0; i < grid.Grid.Length; i++)
        {
            for (int j = 0; j < grid.Grid[i].Length; j++)
            {
                Cell cell = grid.Grid[i][j];
                cell.Visited = true;
                if (cell.Neighbours.ContainsKey("North"))
                    neighbors.Add(cell.Neighbours["North"]);

                if (cell.Neighbours.ContainsKey("East"))
                    neighbors.Add(cell.Neighbours["East"]);

                if (neighbors.Count > 0)
                {
                    randomNbr = grid.GetRandomNumber(0, neighbors.Count);
                    Cell neighbor = neighbors[randomNbr];
                    cell.Link(neighbor, true);
                }
                neighbors.Clear();
            }
        }
    }
}

