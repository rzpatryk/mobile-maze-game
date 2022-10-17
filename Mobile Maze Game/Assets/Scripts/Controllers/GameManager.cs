using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BackToShapeButton;
    public GameObject SaveButton;
    public GameObject BackToLevelSelection;
    public GameObject LevelSelection;
    public GameObject Images;
    public GameObject FinishScreen;
    public GameObject Background;
    public GameObject SaveMessage;
    public GameObject MusicButton;

    public void StartGame()
    {
        BackToLevelSelection.SetActive(true);
        BackToShapeButton.SetActive(false);
        LevelSelection.SetActive(false);
        Images.SetActive(true);
        SaveButton.SetActive(true);
    }

    public void BackToLevelSelect()
    {
        BackToLevelSelection.SetActive(false);
        BackToShapeButton.SetActive(true);
        LevelSelection.SetActive(true);
        Images.SetActive(false);
        SaveButton.SetActive(false);
    }

    public void DisplayFinishScreen()
    {
        FinishScreen.SetActive(true);
    }
    public void ConfigUIForSave(bool option)
    {
        BackToLevelSelection.SetActive(option);
        SaveButton.SetActive(option);
        Background.SetActive(option);
        SaveButton.SetActive(option);
    }

    public void Save()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        yield return null;
        ConfigUIForSave(false);
        yield return new WaitForEndOfFrame();
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        Texture2D texture2D = ScreenCapture.CaptureScreenshotAsTexture();
        NativeGallery.SaveImageToGallery(texture2D, "maze", fileName);
        //File.Move(Application.persistentDataPath + "/" + fileName, path + "/folder/" + fileName);
        //yield return new WaitForEndOfFrame();
        ConfigUIForSave(true);
        SaveMessage.SetActive(true);
       
    }



}
