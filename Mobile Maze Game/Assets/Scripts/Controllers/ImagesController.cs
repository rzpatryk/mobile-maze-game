using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesController : MonoBehaviour
{
    public Sprite[] StartImages;
    public Sprite[] EndImages;
    public GameObject StartImage;
    public GameObject EndImage;

    private void OnEnable()
    {
        int index = Random.Range(0, StartImages.Length);
        StartImage.GetComponent<SpriteRenderer>().sprite = StartImages[index];
        EndImage.GetComponent<SpriteRenderer>().sprite = EndImages[index];
    }
}
