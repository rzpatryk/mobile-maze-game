using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using iText.Kernel.Pdf.Canvas;


namespace Assets.Scripts.ExportToPdf
{
    public class TriangleGridMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            cellHeight = pageHeight * 0.7f / mazeGrid.Row;
            cellWidth = (pageWidth * 0.7f / mazeGrid.Column);
            float width = cellWidth;

            float height = cellHeight;
            float halfHeight = cellHeight / 2;
            float cx, cy;
            float westX, midX, eastX;
            float apexY, baseY;

            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    TriangleCell cell = (TriangleCell)mazeGrid.Grid[i][j];
                    cx = width + cell.Column * width + ((pageHeight - (pageHeight * 0.7f)) / 2); 

                    cy = halfHeight + cell.Row * height + ((pageWidth - (pageWidth * 0.7f)) / 2);
                    westX = cx - width;
                    midX = cx;
                    eastX = cx + width;

                    if (!cell.Upright())
                    {
                        apexY = cy - halfHeight;
                        baseY = cy + halfHeight;
                    }
                    else
                    {
                        apexY = cy + halfHeight;
                        baseY = cy - halfHeight;
                    }
                    if (!cell.Neighbours.ContainsKey("West"))
                    {
                        if (i == 0 && j == 0)
                        {
                            CreateStartImage(westX - cellWidth, baseY);
                            CreatePlayerImage(westX + cellWidth / 2, baseY);
                        }
                        else
                        {
                            canvas.MoveTo(westX, baseY);
                            canvas.LineTo(midX, apexY);
                        }
                    }
                    if (!cell.Neighbours.ContainsKey("East") || (cell.Neighbours.ContainsKey("East") && !cell.Linked(cell.Neighbours["East"])))
                    {
                        if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                        {
                            if (cell.Updown())
                            {
                                CreateEndImage(eastX, apexY);
                            }
                            else
                            {
                                CreateEndImage(eastX, apexY - cellHeight / 2);
                            }
                        }
                        else
                        {
                            canvas.MoveTo(eastX, baseY);
                            canvas.LineTo(midX, apexY);
                        }
                    }
                    bool no_north = (!cell.Upright() && !cell.Neighbours.ContainsKey("North"));
                    bool not_linked = (cell.Upright() && !cell.Neighbours.ContainsKey("South")) || (cell.Neighbours.ContainsKey("South") && !cell.Linked(cell.Neighbours["South"]));
                    if (not_linked || no_north)
                    {
                        canvas.MoveTo(eastX, baseY);
                        canvas.LineTo(westX, baseY);

                    }
                }
            }
        }
    }
}
