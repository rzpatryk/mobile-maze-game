using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BackToShapeButton;
    [SerializeField]
    private GameObject SaveButton;
    [SerializeField]
    private GameObject BackToLevelSelection;
    [SerializeField]
    private GameObject LevelSelection;
    [SerializeField]
    private GameObject Images;
    [SerializeField]
    private GameObject FinishScreen;
    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private GameObject MusicButton;
    [SerializeField]
    private GameObject SaveScreen;
    [SerializeField]
    private GameObject saveText;


    public void SaveMessage(string saveMessage)
    {
        SaveScreen.SetActive(true);
        saveText.GetComponent<TextMeshProUGUI>().text = "Save at :\n" + saveMessage;
        
        
    }

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
        MusicButton.SetActive(option);
    }

}
