using Assets.Scripts.MazeParts.Grids;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using UnityEngine;

public abstract class ExportMazeToPdf : MonoBehaviour
{
    [SerializeField]
    private GameObject StartImage;
    [SerializeField]
    private GameObject EndImage;
    [SerializeField]
    private Sprite Player;
    [SerializeField]
    private GameObject Perm;

    private const string rootFolderName = "Maze";
    protected float cellHeight;
    protected float cellWidth; 
    private PdfWriter pdfWriter;
    private PdfDocument pdfDocument;
    private Document document;
    private PdfPage pdfPage;

    public abstract void ExportMaze(PdfCanvas canvas, MazeGrid mazeGrid, float pageHeight, float pageWidth);

    public string SavePdf(string gridType, MazeGrid maze)
    {
        string savemessage = "";
        if (Perm.GetComponent<PermisionController>().PermissionGranted())
        {
            string path = CreateRootFolder(gridType);
            savemessage = CreatePdf(path, gridType, maze);
        }
        return savemessage;
    }

    private string CreateRootFolder(string gridType)
    {
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Environment");
        string path = androidJavaClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
        string directoryPath = path + "/" + rootFolderName;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string mazeTypeFolderPath = directoryPath + "/" + gridType;
        if (!Directory.Exists(mazeTypeFolderPath))
        {
            Directory.CreateDirectory(mazeTypeFolderPath);
        }

        return mazeTypeFolderPath;
    }
    private string CreatePdf(string path, string gridType, MazeGrid mazeGrid)
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = gridType + "-"  + timeStamp + ".pdf";
        string filePath = path + "/" + fileName;
        pdfWriter = new PdfWriter(filePath);
        pdfDocument = new PdfDocument(pdfWriter);
        document = new Document(pdfDocument, PageSize.A4.Rotate());
        pdfPage = pdfDocument.AddNewPage();
        PdfCanvas canvas = new PdfCanvas(pdfPage);
        float pageHeight = PageSize.A4.Rotate().GetHeight();
        float pageWidth = PageSize.A4.Rotate().GetWidth();
        ExportMaze(canvas, mazeGrid, pageHeight, pageWidth);
        canvas.ClosePathStroke();
        document.Close();

        return rootFolderName + "/" + gridType  + "/" + fileName;
    }
    public void CreateStartImage(float x, float y)
    {

        Texture2D texture = StartImage.GetComponent<SpriteRenderer>().sprite.texture;
        byte[] startImage = texture.EncodeToPNG();
        ImageData image = ImageDataFactory.Create(startImage);
        Image img = new Image(image);
        img.SetWidth(cellHeight);
        img.SetHeight(cellHeight);
        img.SetFixedPosition(x, y);
        document.Add(img);

    }
    public void CreateEndImage(float x, float y)
    {

        Texture2D texture = EndImage.GetComponent<SpriteRenderer>().sprite.texture;
        byte[] endImage = texture.EncodeToPNG();
        ImageData image = ImageDataFactory.Create(endImage);
        Image img = new Image(image);
        img.SetWidth(cellHeight);
        img.SetHeight(cellHeight);
        img.SetFixedPosition(x, y);
        document.Add(img);

    }
    public void CreatePlayerImage(float x, float y)
    {

        Texture2D texture = Player.texture;
        byte[] playerImage = texture.EncodeToPNG();
        ImageData image = ImageDataFactory.Create(playerImage);
        Image img = new Image(image);
        img.SetWidth(cellHeight);
        img.SetHeight(cellHeight);
        img.SetFixedPosition(x, y);
        document.Add(img);

    }
}
