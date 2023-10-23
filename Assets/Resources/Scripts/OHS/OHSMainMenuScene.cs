using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OHSMainMenuScene : MonoBehaviour
{
    public GameObject option;
    public GameObject credit;
    public GameObject on;
    public GameObject offs;
    public GameObject soundoffs;
    int a = 0;
    // private AudioSource shootaudio;
    //  public AudioClip shootsound;

    // Use this for initialization
    void Start()
    {
        
        a++;
        moneytext.text = PlayerPrefs.GetInt("GOLD").ToString();

        //this.shootaudio = this.gameObject.AddComponent<AudioSource>();
        //this.shootaudio.clip = this.shootsound;
        //this.shootaudio.loop = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void InfiniteModeButton()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        SceneManager.LoadScene("InfiniteMode");
    }
    public void OptionButton()
    {
        option.SetActive(true);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }
    public void OptionClose()
    {
        option.SetActive(false);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }
    public void CreditOn()
    {
        credit.SetActive(true);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }
    public void CreditClose()
    {
        credit.SetActive(false);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }

    public void soundonoff()
    {
        a++;

        if (a == 1)
        {
            if (!OHSBackGroundSound.instance.BGMsrc.isPlaying)
            {
                PlayerPrefs.SetInt("BGM", 1);
                OHSBackGroundSound.instance.BGMsrc.UnPause();
                on.SetActive(true);
                offs.SetActive(false);
                soundoffs.SetActive(false);
                NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
            }
        }
        if (a == 2)
        {
            a = 0;
            PlayerPrefs.SetInt("BGM", 0);
            OHSBackGroundSound.instance.BGMsrc.Pause();
            on.SetActive(false);
            offs.SetActive(true);
            soundoffs.SetActive(true);
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        }
    }
    public void BossModeButton()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        SceneManager.LoadScene("1_BossMode");
    }
    public void goShop()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        SceneManager.LoadScene("ShopScene");
    }

    public Text moneytext;
}
