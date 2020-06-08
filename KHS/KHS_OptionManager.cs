using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_OptionManager : MonoBehaviour {
    public Button[] ONui;
    public Button[] Offui;

    public GameObject Optionlayer;

    bool BGMbool = true;
    bool EFFECTbool = true;
    private void Start()
    {
        if (PlayerPrefs.GetInt("BGM") == 0)
        {
            OHSBackGroundSound.instance.BGMsrc.Pause();
            BGMbool = false;
            ONui[0].gameObject.SetActive(false);
            Offui[0].gameObject.SetActive(true);
        }
        else
        {
            OHSBackGroundSound.instance.BGMsrc.UnPause();
            ONui[0].gameObject.SetActive(true);
            Offui[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("EFFECT") == 0)
        {
            // OHSBackGroundSound.instance.BGMsrc.Pause();
            EFFECTbool = false;
            ONui[1].gameObject.SetActive(false);
            Offui[1].gameObject.SetActive(true);
        }
        else
        {
           // OHSBackGroundSound.instance.BGMsrc.UnPause();
            ONui[1].gameObject.SetActive(true);
            Offui[1].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
            InitGame();
    }
    public void BGMonoff()
    {
        if(BGMbool)
        {
            BGMbool = false;
            ONui[0].gameObject.SetActive(false);
            Offui[0].gameObject.SetActive(true);
            PlayerPrefs.SetInt("BGM", 0);
            OHSBackGroundSound.instance.BGMsrc.Pause();
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        }
        else if(!BGMbool)
        {
            BGMbool = true;
            ONui[0].gameObject.SetActive(true);
            Offui[0].gameObject.SetActive(false);
            PlayerPrefs.SetInt("BGM", 1);
            OHSBackGroundSound.instance.BGMsrc.UnPause();
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        }
    }
    public void EFFECTonoff()
    {
        if (EFFECTbool)
        {
            EFFECTbool = false;
            ONui[1].gameObject.SetActive(false);
            Offui[1].gameObject.SetActive(true);
            PlayerPrefs.SetInt("EFFECT", 0);
            // OHSBackGroundSound.instance.BGMsrc.Pause();
            NMHEffectSoundManager.instance.effectonoff = false;
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        }
        else if (!EFFECTbool)
        {
            EFFECTbool = true;
            ONui[1].gameObject.SetActive(true);
            Offui[1].gameObject.SetActive(false);
            PlayerPrefs.SetInt("EFFECT", 1);
            // OHSBackGroundSound.instance.BGMsrc.UnPause();
            NMHEffectSoundManager.instance.effectonoff = true;
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        }
    }
    public void CloseOption()
    {
        Optionlayer.SetActive(false);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }
    public void OptionGo()
    {
        Optionlayer.SetActive(true);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }

    public GameObject CreditLayer;
    public void CreditLayerOpen()
    {
        CreditLayer.SetActive(true);
    }
    public void CreditLayerCloseOption()
    {
        CreditLayer.SetActive(false);
    }

    public void InitGame()
    {
        for (int i = 1; i < 7; i++)
        {
            PlayerPrefs.SetInt("TIME" + i, 3255);
            PlayerPrefs.SetInt("SCORE" + i, 0);
            PlayerPrefs.SetInt("TRY" + i, 0);
            PlayerPrefs.SetInt("CLEAR" + i, 0);
        }
        PlayerPrefs.SetInt("CLEAR" + 0, 1);
        PlayerPrefs.SetInt("CLEAR" + 1, 0);
        PlayerPrefs.SetInt("CLEAR" + 2, 0);


        PlayerPrefs.SetInt("SKIN1", 1);
        PlayerPrefs.SetInt("SKIN2", 0);
        PlayerPrefs.SetInt("SKIN3", 0);
        PlayerPrefs.SetInt("SKIN4", 0);

        PlayerPrefs.SetInt("!SKIN1", 1);
        PlayerPrefs.SetInt("!SKIN2", 0);
        PlayerPrefs.SetInt("!SKIN3", 0);
        PlayerPrefs.SetInt("!SKIN4", 0);


        PlayerPrefs.SetInt("SKILLITEM1", 15);
        PlayerPrefs.SetInt("SKILLITEM2", 15);
        PlayerPrefs.SetInt("SKILLITEM3", 15);
        PlayerPrefs.SetInt("SKILLITEM4", 15);

        PlayerPrefs.SetInt("SKILL1", 4);
        PlayerPrefs.SetInt("SKILL2", 4);

        PlayerPrefs.SetInt("GOLD", 100000);
    }
}
