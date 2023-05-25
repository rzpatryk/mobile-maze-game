using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ExportMazeToPdfTest
{
    [TestCase("SquareMaze", ExpectedResult = null)]
    [TestCase("HexGrid", ExpectedResult = null)]
    [TestCase("TriangleGrid", ExpectedResult = null)]
    [TestCase("HexShape", ExpectedResult = null)]
    [TestCase("TriangleShape", ExpectedResult = null)]
    [TestCase("CircleMaze", ExpectedResult = null)]
    public IEnumerator ExportSquareMazeToPdfTest(string sceneName)
    {
        int index = 0;
        SceneManager.LoadScene(sceneName);
        yield return null;
        GameObject levelSelectionButton = GameObject.Find("LevelSelectButtons");
        foreach (Transform child in levelSelectionButton.transform)
        {
            index++;
            child.GetComponent<Button>().onClick.Invoke();
            yield return null;
            GameObject saveButton = GameObject.Find("SaveButton");
            saveButton.GetComponent<Button>().onClick.Invoke();
            yield return null;
            GameObject saveScreen = GameObject.Find("SaveScreen");
            Assert.AreEqual(true, saveScreen.activeInHierarchy);
            yield return new WaitForSeconds(0.5f);
            GameObject okButton = saveScreen.transform.GetChild(0).GetChild(1).gameObject;
            okButton.GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual(false, saveScreen.activeInHierarchy);
            GameObject backToShapeSelection = GameObject.Find("BackToLevelSelection");
            backToShapeSelection.GetComponent<Button>().onClick.Invoke();
            yield return null;
            
        }

        Assert.AreEqual(12, index);
    }
}
