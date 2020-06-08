using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OHSBossModeScene : MonoBehaviour {
    public GameObject GameStartButton;

    public GameObject[] BossChoiceObject;
    public Button[] BossChoiceButton;

    public Text BESTTIMETEXT;
    public Text BESTSCORETEXT;
    public Text TRYTEXT;
    public Text CLEARTEXT;
    // Use this for initialization
    void Start()
    {
        GameStartButton.SetActive(false);
        InitSize();
        ChoiceButtonClick(1);
    }
    public void BackButton()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        SceneManager.LoadScene("0_MainMenu");
    }
    private void InitSize()
    {
        for(int i=0;i<6;i++)
        {
            BossChoiceObject[i].gameObject.SetActive(false);
            BossChoiceButton[i].image.color = new Color(0.7f, 0.7f, 0.7f);
            BossChoiceButton[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    public GameObject Bosslock;
    public Image LockImage;
    public Text ClearText;
    public void ChoiceButtonClick(int _num)
    {
        KHS_GamaManager.instance.BossNumber = _num - 1;
        bossImage.sprite = boss[KHS_GamaManager.instance.BossNumber];
        InitSize();
        BossChoiceObject[_num - 1].SetActive(true);
        BossChoiceButton[_num - 1].image.color = new Color(1f,1f,1f);
        BossChoiceButton[_num - 1].GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.5f, 1);
        GameStartButton.SetActive(true);
        BESTTIMETEXT.text = PlayerPrefs.GetInt("TIME" + (KHS_GamaManager.instance.BossNumber + 1)).ToString();
        BESTSCORETEXT.text = PlayerPrefs.GetInt("SCORE" + (KHS_GamaManager.instance.BossNumber + 1)).ToString();
        TRYTEXT.text = "TRY : " +PlayerPrefs.GetInt("TRY" + (KHS_GamaManager.instance.BossNumber + 1)).ToString();
        if(PlayerPrefs.GetInt("TIME" + (KHS_GamaManager.instance.BossNumber + 1)) ==3255)
        {
            BESTTIMETEXT.text = "0";
        }
        CLEARTEXT.text ="CLEAR : "+ PlayerPrefs.GetInt("CLEAR" + (KHS_GamaManager.instance.BossNumber + 1)).ToString();
        /////////////////////////////
        if(PlayerPrefs.GetInt("CLEAR"+(KHS_GamaManager.instance.BossNumber))<1)
        {
            Bosslock.SetActive(true);
            LockImage.sprite = boss[KHS_GamaManager.instance.BossNumber];
            switch(_num)
            {
                case 2:
                    ClearText.text = "TRIANGLE\n클리어 시\n해금";
                    break;
                case 3:
                    ClearText.text = "SQUARE\n클리어 시\n해금";
                    break;
                case 4:
                    ClearText.text = "RHOMBUS\n클리어 시\n해금";
                    break;
                case 5:
                    ClearText.text = "PENTAGON\n클리어 시\n해금";
                    break;
                case 6:
                    ClearText.text = "CIRCLE\n클리어 시\n해금";
                    break;
            }
        }
        else
        {
            Bosslock.SetActive(false);
        }
        //////////////////////////////////////

        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
    }
    public void GameStartClick()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        GameStartButton.GetComponent<Animator>().SetTrigger("Start");
        Invoke("GameStart", 0.55f);
    }
    void GameStart()
    {
        if (KHS_GamaManager.instance.BossNumber < 3)
            SceneManager.LoadScene("inGame");

        PlayerPrefs.SetInt("TRY" + (KHS_GamaManager.instance.BossNumber + 1), PlayerPrefs.GetInt("TRY" + (KHS_GamaManager.instance.BossNumber + 1)) +1);
    }


    public Image bossImage;
    public Sprite[] boss;
}

