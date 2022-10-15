using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BackToShapeButton;
    public GameObject BackToLevelSelection;
    public GameObject LevelSelection;
    public GameObject Images;

    public void StartGame()
    {
        BackToLevelSelection.SetActive(true);
        BackToShapeButton.SetActive(false);
        LevelSelection.SetActive(false);
        Images.SetActive(true);
    }

    public void BackToLevelSelect()
    {
        BackToLevelSelection.SetActive(false);
        BackToShapeButton.SetActive(true);
        LevelSelection.SetActive(true);
        Images.SetActive(false);
    }



}
