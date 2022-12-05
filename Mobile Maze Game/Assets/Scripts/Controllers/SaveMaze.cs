using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SaveMaze : MonoBehaviour
{
    private const string rootFolderName = "Maze";
    private string mazeType;
    [SerializeField]
    private GameObject Perm;
    [SerializeField]
    private GameManager GuiManager;
    public void Save(string mazeType)
    {
        this.mazeType = mazeType;
        StartCoroutine("CaptureIt");

    }


    IEnumerator CaptureIt()
    {
        GuiManager.ConfigUIForSave(false);
        yield return new WaitForEndOfFrame();
        string timeStamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".pdf";
        string pathToSave = fileName;
        Texture2D texture2D = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] image = texture2D.EncodeToPNG();

        if (Perm.GetComponent<PermisionController>().PermissionGranted())
        {

            CreatePdf(fileName, image);
            GuiManager.SaveMessage();
        }

        GuiManager.ConfigUIForSave(true);
    }

    private void CreatePdf(string fileName, byte[] image)
    {
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Environment");
        string path = androidJavaClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
        string directoryPath = path + "/" + rootFolderName;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string mazeTypeFolderPath = directoryPath + "/" + mazeType;
        if (!Directory.Exists(mazeTypeFolderPath))
        {
            Directory.CreateDirectory(mazeTypeFolderPath);
        }
        string filePath = mazeTypeFolderPath + "/" + fileName;
        PdfWriter pdfWriter = new PdfWriter(filePath);
        PdfDocument pdfDocument = new PdfDocument(pdfWriter);
        Document document = new Document(pdfDocument, PageSize.A4.Rotate());
        PageSize pageSize = document.GetPdfDocument().GetDefaultPageSize();
        ImageData data = ImageDataFactory.Create(image);
        iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
        if (mazeType.Equals("CircleMaze") || mazeType.Equals("HexShape"))
        {
            img.ScaleToFit(PageSize.A4.Rotate().GetWidth() * 1.5f, PageSize.A4.Rotate().GetHeight()*1.5f);
            img.SetFixedPosition((PageSize.A4.Rotate().GetWidth() - img.GetImageScaledWidth()) / 2, (PageSize.A4.Rotate().GetHeight() - img.GetImageScaledHeight())/2);
        }
        else
        {
            img.SetWidth(PageSize.A4.Rotate().GetWidth());
            img.SetHeight(PageSize.A4.Rotate().GetHeight());
            img.SetFixedPosition(0, 0);

        }
        Debug.Log("PageSize.A4.Rotate().GetWidth()" + PageSize.A4.Rotate().GetWidth());
        Debug.Log("PageSize.A4.Rotate().GetHeight()" + PageSize.A4.Rotate().GetHeight());
        Debug.Log("img.GetImageWidth()" + img.GetImageWidth());
        Debug.Log("img.GetImageHeight()" + img.GetImageHeight());
        Debug.Log("img.GetImageScaledWidth()" + img.GetImageScaledWidth());
        Debug.Log("img.GetImageScaledHeight()" + img.GetImageScaledHeight());
        document.Add(img);
        document.Close();
    }
}
