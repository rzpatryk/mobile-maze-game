using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Image musicImage;
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    private bool muted = false;
    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 1);
            Load();
        }else
        {
            Load();
        }
        UpdateIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateIcon();
    }


    private void UpdateIcon()
    {
        if (muted == false)
        {
            musicImage.GetComponent<Image>().sprite = musicOnImage;
        }
        else
        {
            musicImage.GetComponent<Image>().sprite = musicOffImage;
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
