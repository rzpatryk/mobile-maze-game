using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using Assets.Scripts.MazeParts.Path;
using iText.Kernel.Pdf.Canvas;


namespace Assets.Scripts.ExportToPdf
{
    public class HexShapeMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            cellHeight = pageHeight * 0.7f / mazeGrid.Row;
            cellWidth = (pageWidth * 0.7f / mazeGrid.Column);
            float size = cellHeight + (cellHeight / 3);
            float cx, cy;
            float a_size = size / 4;
            float b_size = size / 2;
            float width = b_size * 2;
            Distance distance = mazeGrid.Grid[0][0].Distances();
            Cell maxCell = distance.Max();
            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    Cell cell = mazeGrid.Grid[i][j];
                    cx = (b_size + cell.Column * width) - ((mazeGrid.Grid[i].Length / 2.0f) * width) + (pageWidth / 2);
                    cy = size / 2 + 3 * cell.Row * a_size + ((pageHeight - (pageHeight * 0.7f)) / 2);

                    float x_west_north = cx - b_size;
                    float y_west_north = cy + a_size;

                    float x_west_south = cx - b_size;
                    float y_west_south = cy - a_size;

                    float x_east_north = cx + b_size;
                    float y_east_north = cy + a_size;

                    float x_east_south = cx + b_size;
                    float y_east_south = cy - a_size;

                    float y_mid_north = cy + size / 2;
                    float y_mid_south = cy - size / 2;

                    if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                    {
                        CreateEndImage(cx - cellWidth / 2, cy - cellHeight / 2);
                    }

                    if ((cell.Neighbours.ContainsKey("West") && !(cell.Linked(cell.Neighbours["West"]))) || !cell.Neighbours.ContainsKey("West"))
                    {
                        if (i == 0 && j == 0)
                        {
                            CreatePlayerImage(cx - cellWidth / 4, cy - cellHeight / 2);
                            CreateStartImage(cx - cellWidth * 1.5f, cy - cellHeight / 2);
                        }
                        else
                        {
                            canvas.MoveTo(x_west_north, y_west_north);
                            canvas.LineTo(x_west_south, y_west_south);
                        }
                    }
                    if ((!cell.Neighbours.ContainsKey("East")))
                    {
                        canvas.MoveTo(x_east_north, y_east_north);
                        canvas.LineTo(x_east_south, y_east_south);
                    }
                    if ((!cell.Neighbours.ContainsKey("NorthWest")))
                    {
                        canvas.MoveTo(x_west_north, y_west_north);
                        canvas.LineTo(cx, y_mid_north);
                    }
                    if ((!cell.Neighbours.ContainsKey("NorthEast")))
                    {
                        canvas.MoveTo(x_east_north, y_east_north);
                        canvas.LineTo(cx, y_mid_north);
                    }
                    if ((cell.Neighbours.ContainsKey("SouthWest") && !(cell.Linked(cell.Neighbours["SouthWest"]))) || !cell.Neighbours.ContainsKey("SouthWest"))
                    {
                        canvas.MoveTo(x_west_south, y_west_south);
                        canvas.LineTo(cx, y_mid_south);

                    }
                    if ((cell.Neighbours.ContainsKey("SouthEast") && !(cell.Linked(cell.Neighbours["SouthEast"]))) || !cell.Neighbours.ContainsKey("SouthEast"))
                    {
                        canvas.MoveTo(x_east_south, y_east_south);
                        canvas.LineTo(cx, y_mid_south);

                    }
                }
            }
        }
    }
}
