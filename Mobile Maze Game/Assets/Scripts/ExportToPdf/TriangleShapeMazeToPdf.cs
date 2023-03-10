using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using Assets.Scripts.MazeParts.Path;
using iText.Kernel.Pdf.Canvas;


namespace Assets.Scripts.ExportToPdf
{
    public class TriangleShapeMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            cellHeight = pageHeight * 0.7f / mazeGrid.Row;
            cellWidth = pageWidth * 0.7f / mazeGrid.Column;

            float width = cellWidth;
            float height = cellHeight;
            float halfHeight = height / 2;

            float cx, cy;
            float westX, midX, eastX;
            float apexY, baseY;

            Distance distance = mazeGrid.Grid[0][0].Distances();
            Cell maxCell = distance.Max();
            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    TriangleCell cell = (TriangleCell)mazeGrid.Grid[i][j];

                    cx = width + cell.Column * width + ((pageWidth - (pageWidth * 0.7f)) / 2);

                    cy = halfHeight + cell.Row * height + ((pageHeight - (pageHeight * 0.7f)) / 2);
                    westX = cx - width;
                    midX = cx;
                    eastX = cx + width;

                    if (cell.Updown())
                    {
                        apexY = cy - halfHeight;
                        baseY = cy + halfHeight;
                    }
                    else
                    {
                        apexY = cy + halfHeight;
                        baseY = cy - halfHeight;
                    }

                    if (i == 0 && j == 0)
                    {
                        CreatePlayerImage(midX, baseY);
                        CreateStartImage(westX - cellWidth, baseY);
                    }


                    if (!cell.Neighbours.ContainsKey("West"))
                    {
                        if (i > 0)
                        {
                            canvas.MoveTo(westX + (width * i), baseY);
                            canvas.LineTo(midX + (width * i), apexY);
                        }
                        else
                        {
                            canvas.MoveTo(westX, baseY);
                            canvas.LineTo(midX, apexY);

                        }
                    }

                    if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                    {
                        if (cell.Updown())
                        {
                            CreateEndImage((midX + (width * i)) - cellWidth / 2, baseY - cellHeight);
                        }
                        else
                        {
                            CreateEndImage((midX + (width * i)) - cellWidth / 2, baseY);
                        }
                    }

                    if ((cell.Neighbours.ContainsKey("East") && !cell.Linked(cell.Neighbours["East"])) || !cell.Neighbours.ContainsKey("East"))
                    {

                        canvas.MoveTo(eastX + (width * i), baseY);
                        canvas.LineTo(midX + (width * i), apexY);
                    }
                    bool no_north = cell.Updown() && !cell.Neighbours.ContainsKey("North");
                    bool not_linked = !cell.Updown() && ((cell.Neighbours.ContainsKey("South") && !cell.Linked(cell.Neighbours["South"])) || !cell.Neighbours.ContainsKey("South"));

                    if (no_north || not_linked)
                    {

                        canvas.MoveTo(eastX + (width * i), baseY);
                        canvas.LineTo(westX + (width * i), baseY);
                    }
                }
            }
        }
    }
}
