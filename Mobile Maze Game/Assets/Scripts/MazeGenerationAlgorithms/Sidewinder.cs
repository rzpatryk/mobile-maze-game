using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using System.Collections.Generic;

namespace Assets.Scripts.MazeGenerationAlgorithms
{
    public class Sidewinder : IMazeAlgorithm
    {
        public void CreateMaze(MazeGrid grid)
        {
            List<Cell> cellsInRun = new List<Cell>();
            bool shouldCloseOut;
            bool at_eastern_boundary;
            bool at_northern_boundary;
            int randomNumber;
            int cellsToCount;
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
                        randomNumber = grid.GetRandomNumber(0, cellsInRun.Count);
                        Cell member = cellsInRun[randomNumber];
                        if (member.Neighbours.ContainsKey("North"))
                        {
                            member.Link(member.Neighbours["North"]);
                            cellsInRun.Clear();
                        }
                    }
                    else
                    {
                        cell.Link(cell.Neighbours["East"]);
                    }
                }
            }
        }
    }
}

