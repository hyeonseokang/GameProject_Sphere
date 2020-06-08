using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KHS_SceneManager : MonoBehaviour {
    public void goMainMenu()
    {
        Time.timeScale = 1.0f;
        OHSBackGroundSound.instance.BGMsrc.Play();
        SceneManager.LoadScene("0_MainMenu");
    }
    public void goInGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("inGame");
    }
    public void goInfiniteMod()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("InfiniteMode");
    }
}
