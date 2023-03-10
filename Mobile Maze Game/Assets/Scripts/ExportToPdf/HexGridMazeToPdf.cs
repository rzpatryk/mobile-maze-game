using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using iText.Kernel.Pdf.Canvas;


namespace Assets.Scripts.ExportToPdf
{
    public class HexGridMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            cellHeight = pageHeight * 0.7f / mazeGrid.Row;
            cellWidth = (pageWidth * 0.7f / mazeGrid.Column);
            cellHeight -= (cellHeight / 2) / mazeGrid.Row;
            cellWidth -= cellWidth / 3;
            float cx, cy;
            float x_fw, x_nw, x_ne, x_fe;
            float y_n, y_m, y_s;
            float a_size = cellWidth / 2;
            float b_size = cellHeight / 2;
            float height = b_size * 2;
            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    Cell cell = mazeGrid.Grid[i][j];
                    cx = (cellWidth + 3 * cell.Column * a_size) + ((pageWidth - (pageWidth * 0.7f)) / 2);
                    cy = (b_size + cell.Row * height) + ((pageHeight - (pageHeight * 0.7f)) / 2);
                    if (cell.Column % 2 != 0)
                        cy += b_size;
                    x_fw = cx - cellWidth;
                    x_nw = cx - a_size;
                    x_ne = cx + a_size;
                    x_fe = cx + cellWidth;

                    y_n = cy - b_size;
                    y_m = cy;
                    y_s = cy + b_size;


                    if (!(cell.Neighbours.ContainsKey("SouthWest")))
                    {
                        if (i == 0 && j == 0)
                        {
                            CreateStartImage(x_fw - cellWidth * 2, y_n);
                            CreatePlayerImage(x_fw + a_size, y_n);
                        }
                        else
                        {
                            canvas.MoveTo(x_fw, y_m);
                            canvas.LineTo(x_nw, y_n);
                        }

                    }
                    if ((cell.Neighbours.ContainsKey("SouthEast") && !(cell.Linked(cell.Neighbours["SouthEast"]))) || !cell.Neighbours.ContainsKey("SouthEast"))
                    {
                        canvas.MoveTo(x_ne, y_n);
                        canvas.LineTo(x_fe, y_m);
                    }
                    if ((cell.Neighbours.ContainsKey("South") && !(cell.Linked(cell.Neighbours["South"]))) || !cell.Neighbours.ContainsKey("South"))
                    {
                        canvas.MoveTo(x_nw, y_n);
                        canvas.LineTo(x_ne, y_n);
                    }
                    if (!(cell.Neighbours.ContainsKey("North")))
                    {
                        canvas.MoveTo(x_ne, y_s);
                        canvas.LineTo(x_nw, y_s);
                    }
                    if (!(cell.Neighbours.ContainsKey("NorthWest")))
                    {
                        canvas.MoveTo(x_fw, y_m);
                        canvas.LineTo(x_nw, y_s);
                    }
                    if ((cell.Neighbours.ContainsKey("NorthEast") && !(cell.Linked(cell.Neighbours["NorthEast"]))) || !cell.Neighbours.ContainsKey("NorthEast"))
                    {
                        if (i == mazeGrid.Row - 1 && j == mazeGrid.Column - 1)
                        {
                            CreateEndImage(x_fe, y_m);
                        }
                        else
                        {
                            canvas.MoveTo(x_fe, y_m);
                            canvas.LineTo(x_ne, y_s);
                        }

                    }
                }
            }
        }
    }
}
