using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using iText.Kernel.Pdf.Canvas;


namespace Assets.Scripts.ExportToPdf
{
    public class SquareMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            cellHeight = pageHeight * 0.7f / mazeGrid.Row;
            cellWidth = pageWidth * 0.7f / mazeGrid.Column;

            float x1, x2, y1, y2;
            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    Cell cell = mazeGrid.Grid[i][j];
                    x1 = cell.Column * cellWidth + ((pageWidth - (pageWidth * 0.7f)) / 2);
                    y1 = cell.Row * cellHeight + ((pageHeight - (pageHeight * 0.7f)) / 2);
                    x2 = (cell.Column + 1) * cellWidth + ((pageWidth - (pageWidth * 0.7f)) / 2);
                    y2 = (cell.Row + 1) * cellHeight + ((pageHeight - (pageHeight * 0.7f)) / 2);

                    if (!cell.Neighbours.ContainsKey("South") || (cell.Neighbours.ContainsKey("South") && !(cell.Linked(cell.Neighbours["South"]))))
                    {
                        canvas.MoveTo(x1, y1);
                        canvas.LineTo(x2, y1);
                    }
                    if (!cell.Neighbours.ContainsKey("West"))
                    {
                        if (i == 0 && j == 0)
                        {
                            CreateStartImage(x1 - cellWidth * 1.5f, y1);
                            CreatePlayerImage(x1, y1);
                        }
                        else
                        {
                            canvas.MoveTo(x1, y1);
                            canvas.LineTo(x1, y2);
                        }
                    }
                    if (!cell.Neighbours.ContainsKey("East") || (cell.Neighbours.ContainsKey("East") && !(cell.Linked(cell.Neighbours["East"]))))
                    {
                        if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                        {
                            CreateEndImage(x2, y2 - cellHeight);
                        }
                        else
                        {
                            canvas.MoveTo(x2, y1);
                            canvas.LineTo(x2, y2);
                        }
                    }

                    if (!(cell.Neighbours.ContainsKey("North")))
                    {
                        canvas.MoveTo(x1, y2);
                        canvas.LineTo(x2, y2);
                    }
                }

            }
        }
    }
}
