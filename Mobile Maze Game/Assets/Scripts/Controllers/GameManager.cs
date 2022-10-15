using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BackToShapeButton;
    public GameObject BackToLevelSelection;
    public GameObject LevelSelection;

    public void StartGame()
    {
        BackToLevelSelection.SetActive(true);
        BackToShapeButton.SetActive(false);
        LevelSelection.SetActive(false);
    }

    public void BackToLevelSelect()
    {
        BackToLevelSelection.SetActive(false);
        BackToShapeButton.SetActive(true);
        LevelSelection.SetActive(true);
    }



}
