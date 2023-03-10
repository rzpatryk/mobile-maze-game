using Assets.Scripts.MazeParts.Cells;
using Assets.Scripts.MazeParts.Grids;
using Assets.Scripts.MazeParts.Path;
using iText.Kernel.Pdf.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ExportToPdf
{
    public class CircleMazeToPdf : ExportMazeToPdf
    {
        public override void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth)
        {
            float theta, inner_radius, outer_radius, theta_ccw, theta_cw;
            float ax, ay, bx, by, cx, cy, dx, dy;

            cellHeight = pageHeight * 0.7f / (mazeGrid.Row * 2);
            cellWidth = pageWidth * 0.7f / (mazeGrid.Column * 2);


            float centerX = pageWidth / 2;
            float centerY = pageHeight / 2;

            Distance distance = mazeGrid.Grid[1][0].Distances();
            Cell maxCell = distance.Max();

            for (int i = 0; i < mazeGrid.Grid.Length; i++)
            {
                for (int j = 0; j < mazeGrid.Grid[i].Length; j++)
                {
                    Cell cell = mazeGrid.Grid[i][j];


                    int row1 = cell.Row;
                    theta = (float)(2 * Math.PI / mazeGrid.Grid[row1].Length);
                    inner_radius = row1 * (cellHeight);
                    outer_radius = ((row1 + 1) * cellHeight);


                    theta_ccw = cell.Column * theta;
                    theta_cw = (cell.Column + 1) * theta;

                    ax = centerX + (float)(inner_radius * Math.Cos(theta_ccw));
                    ay = centerY + (float)(inner_radius * Math.Sin(theta_ccw));
                    bx = centerX + (float)(outer_radius * Math.Cos(theta_ccw));
                    by = centerY + (float)(outer_radius * Math.Sin(theta_ccw));
                    cx = centerX + (float)(inner_radius * Math.Cos(theta_cw));
                    cy = centerY + (float)(inner_radius * Math.Sin(theta_cw));
                    dx = centerX + (float)(outer_radius * Math.Cos(theta_cw));
                    dy = centerY + (float)(outer_radius * Math.Sin(theta_cw));


                    if (i == 0)
                    {
                        CreateStartImage((pageWidth / 2) - cellHeight / 2, (pageHeight / 2) - cellHeight / 2);
                    }
                    if (cell.Row == maxCell.Row && cell.Column == maxCell.Column)
                    {
                        CreateEndImage(((bx + cx) / 2f) - cellHeight / 2, (by + cy) / 2f - cellHeight / 2);
                    }
                    if (i == 1 && j == 0)
                    {
                        CreatePlayerImage((bx + cx) / 2f - cellHeight / 2, (by + cy) / 2f - cellHeight / 2);
                        
                    }

                    if (cell.Row > 0)
                    {

                        if (i == mazeGrid.Grid.Length - 1)
                        {
                            canvas.MoveTo(bx, by);
                            canvas.LineTo(dx, dy);
                        }
                        if (cell.Neighbours.ContainsKey("Inward") && !cell.Linked(cell.Neighbours["Inward"]) || (i == 1 && j > 0))
                        {
                            canvas.MoveTo(ax, ay);
                            canvas.LineTo(cx, cy);

                        }

                        if ((cell.Neighbours.ContainsKey("Cw") && !(cell.Linked(cell.Neighbours["Cw"]))) || !cell.Neighbours.ContainsKey("Cw"))
                        {
                            canvas.MoveTo(cx, cy);
                            canvas.LineTo(dx, dy);

                        }

                    }
                }
            }
        }
    }
}
