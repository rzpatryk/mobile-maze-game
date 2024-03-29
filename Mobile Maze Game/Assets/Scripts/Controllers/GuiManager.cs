using TMPro;
using UnityEngine;


public class GuiManager : MonoBehaviour
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
    private GameObject SaveScreen;
    [SerializeField]
    private GameObject saveText;


    public void SaveMessage(string saveMessage)
    {
        if (saveMessage != null)
        {
            SaveScreen.SetActive(true);
            saveText.GetComponent<TextMeshProUGUI>().text = "Save at :\n" + saveMessage;
        }
    }

    public void PermissionDanedMessage()
    {
        SaveScreen.SetActive(true);
        saveText.GetComponent<TextMeshProUGUI>().text = "Failed. Permission Danied";
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
}
