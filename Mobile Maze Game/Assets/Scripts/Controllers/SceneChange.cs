using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void squareMaze()
    {
        SceneManager.LoadScene("SquareMaze");
    }

    public void selectShape()
    {
        SceneManager.LoadScene("SelectShape");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Main");
    }
    
}
