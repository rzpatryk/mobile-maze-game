using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void SquareMaze()
    {
        SceneManager.LoadScene("SquareMaze");
    }

    public void SelectShape()
    {
        SceneManager.LoadScene("SelectShape");
    }

    public void TriangleGrid()
    {
        SceneManager.LoadScene("TriangleGrid");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void HexGrid() {
        SceneManager.LoadScene("HexGrid");
    }

    public void CircleMaze()
    {
        SceneManager.LoadScene("CircleMaze");
    }

    
}
