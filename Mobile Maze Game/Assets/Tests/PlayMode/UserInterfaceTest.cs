using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceTest
{
    [UnityTest]
    public IEnumerator StartGameTest()
    {
        SceneManager.LoadScene("Main");
        yield return null;
        GameObject playButton = GameObject.Find("PlayButton");
        playButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("SelectShape", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator BackToMainFromSelectShape()
    {
        SceneManager.LoadScene("SelectShape");
        yield return null;
        GameObject playButton = GameObject.Find("BackButton");
        playButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("Main", SceneManager.GetActiveScene().name);
    }


    [TestCase("SquareGridButton", "SquareMaze", ExpectedResult = null)]
    [TestCase("HexGridButton", "HexGrid", ExpectedResult = null)]
    [TestCase("TriangleGridButton", "TriangleGrid", ExpectedResult = null)]
    [TestCase("HexMazeShapeButton", "HexShape", ExpectedResult = null)]
    [TestCase("TriangleMazeShapeButton", "TriangleShape", ExpectedResult = null)]
    [TestCase("CircleMazeShapeButton", "CircleMaze", ExpectedResult = null)]
    public IEnumerator SelectShapeButtonTest(string buttonName, string mazeSceneName)
    {
        SceneManager.LoadScene("SelectShape");
        yield return null;
        Assert.AreEqual("SelectShape", SceneManager.GetActiveScene().name);
        yield return null;
        GameObject shapeButton = GameObject.Find(buttonName);
        shapeButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual(mazeSceneName, SceneManager.GetActiveScene().name);
        GameObject backtoSelectShapeButton = GameObject.Find("BackToShapeSelection");
        backtoSelectShapeButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("SelectShape", SceneManager.GetActiveScene().name);
    }
    [TestCase("SquareMaze", ExpectedResult = null)]
    [TestCase("HexGrid", ExpectedResult = null)]
    [TestCase("TriangleGrid", ExpectedResult = null)]
    [TestCase("HexShape", ExpectedResult = null)]
    [TestCase("TriangleShape", ExpectedResult = null)]
    [TestCase("CircleMaze", ExpectedResult = null)]
    public IEnumerator BackToselectShapeButtonTest(string mazeSceneName)
    {
        SceneManager.LoadScene(mazeSceneName);
        yield return null;
        GameObject playButton = GameObject.Find("BackToShapeSelection");
        playButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("SelectShape", SceneManager.GetActiveScene().name);
    }


    [UnityTest]
    public IEnumerator MusicButtonMainSceneTest()
    {
        SceneManager.LoadScene("Main");
        yield return null;
        GameObject musicButton = GameObject.Find("MusicButton");
        GameObject image = musicButton.transform.GetChild(0).gameObject;
        Assert.AreEqual("Icon_22", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(true, AudioListener.pause);

        musicButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("Icon_23", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(false, AudioListener.pause);

        musicButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("Icon_22", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(true, AudioListener.pause);
    }

    [TestCase("SelectShape", ExpectedResult = null)]
    [TestCase("SquareMaze", ExpectedResult = null)]
    [TestCase("HexGrid", ExpectedResult = null)]
    [TestCase("TriangleGrid", ExpectedResult = null)]
    [TestCase("HexShape", ExpectedResult = null)]
    [TestCase("TriangleShape", ExpectedResult = null)]
    [TestCase("CircleMaze", ExpectedResult = null)]
    public IEnumerator MusicButtonOtherSceneTest(string sceneName)
    {
        SceneManager.LoadScene("Main");
        yield return null;

        SceneManager.LoadScene(sceneName);
        yield return null;


        GameObject musicButton = GameObject.Find("MusicButton");
        GameObject image = musicButton.transform.GetChild(0).gameObject;
        Assert.AreEqual("Icon_22", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(true, AudioListener.pause);

        musicButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("Icon_23", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(false, AudioListener.pause);

        musicButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("Icon_22", image.GetComponent<Image>().sprite.name);
        Assert.AreEqual(true, AudioListener.pause);
    }


    [TestCase("SquareMaze", ExpectedResult = null)]
    [TestCase("HexGrid", ExpectedResult = null)]
    [TestCase("TriangleGrid", ExpectedResult = null)]
    [TestCase("HexShape", ExpectedResult = null)]
    [TestCase("TriangleShape", ExpectedResult = null)]
    [TestCase("CircleMaze", ExpectedResult = null)]
    public IEnumerator SelectLevelTest(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
        GameObject levelSelectionButton = GameObject.Find("LevelSelectButtons");
        GameObject backToShapeSelection = GameObject.Find("BackToShapeSelection");
        foreach (Transform child in levelSelectionButton.transform)
        {
            child.GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual(false, levelSelectionButton.activeInHierarchy);
            GameObject backToLevelSelection = GameObject.Find("BackToLevelSelection");
            GameObject saveButton = GameObject.Find("SaveButton");
            Assert.AreEqual(true, backToLevelSelection.activeInHierarchy);
            Assert.AreEqual(false, backToShapeSelection.activeInHierarchy);
            Assert.AreEqual(true, saveButton.activeInHierarchy);

            backToLevelSelection.GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual(true, levelSelectionButton.activeInHierarchy);
            Assert.AreEqual(false, backToLevelSelection.activeInHierarchy);
            Assert.AreEqual(true, backToShapeSelection.activeInHierarchy);
            Assert.AreEqual(false, saveButton.activeInHierarchy);
        }
    }
}
