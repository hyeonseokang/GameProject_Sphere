using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_AudioManager : MonoBehaviour {
    public AudioSource Boss1BackgroundMusic;

    private void Start()
    {
        if (PlayerPrefs.GetInt("BGM") == 0)
            Boss1BackgroundMusic.Stop();
        else
            Boss1BackgroundMusic.Play();

        OHSBackGroundSound.instance.BGMsrc.Stop();
    }

    public void BackgroundStart()
    {
        Boss1BackgroundMusic.Play();

    }
    public void BackgroundStop()
    {
        Boss1BackgroundMusic.Stop();
    }

}
