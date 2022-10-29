using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Sidewinder : IMazeAlgorithm
{
    private List<Cell> cellsInRun = new List<Cell>();
    private bool shouldCloseOut;
    private bool at_eastern_boundary;
    private bool at_northern_boundary;
    private int randomNbr;
    private int cellsToCount;
    public void CreateMaze(MazeGrid grid)
    {
        for (int r = 0; r < grid.Grid.Length; r++)
        {
            cellsInRun.Clear();
            for (int c = 0; c < grid.Grid[r].Length; c++)
            {
                Cell cell = grid.Grid[r][c];
                cell.Visited = true;
                at_eastern_boundary = !cell.Neighbours.ContainsKey("East");
                at_northern_boundary = !cell.Neighbours.ContainsKey("North");
                cellsInRun.Add(cell);
                cellsToCount = grid.GetRandomNumber(0, 2);


                shouldCloseOut = at_eastern_boundary || (!at_northern_boundary && cellsToCount == 0);
                if (shouldCloseOut)
                {
                    randomNbr = grid.GetRandomNumber(0, cellsInRun.Count);
                    Cell member = cellsInRun[randomNbr];
                    if (member.Neighbours.ContainsKey("North"))
                    {
                        member.Link(member.Neighbours["North"], true);
                        cellsInRun.Clear();
                    }
                }
                else
                {
                    cell.Link(cell.Neighbours["East"], true);
                }
            }
        }
    }
}

