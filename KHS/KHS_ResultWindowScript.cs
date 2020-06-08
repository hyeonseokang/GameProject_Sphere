using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_ResultWindowScript : MonoBehaviour {
    public Text TimeText;
    public Text GoldText;
    public Text ScoreText;
    public Text GiftText;
    public GameObject GiftObjet;
    public void setResult(int _Time, int _Gold, int _Score)
    {
        PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD")+_Gold);
        TimeText.text =  _Time.ToString();
        GoldText.text = _Gold.ToString();
        ScoreText.text = _Score.ToString();
        TimeText.text = "0";
        GoldText.text = "0";
        ScoreText.text = "0";
        StartCoroutine(RenewalResult(new Text[3] { ScoreText , TimeText , GoldText },
            new int[3] { _Score, _Time, _Gold }));

        if(PlayerPrefs.GetInt("TIME"+(KHS_GamaManager.instance.BossNumber+1))>_Time)
        {
            PlayerPrefs.SetInt("TIME" +( KHS_GamaManager.instance.BossNumber + 1), _Time);
        }
        if (PlayerPrefs.GetInt("SCORE" + (KHS_GamaManager.instance.BossNumber + 1)) < _Score)
        {
            PlayerPrefs.SetInt("SCORE" +(KHS_GamaManager.instance.BossNumber + 1), _Score);
        }
    }
    IEnumerator RenewalResult(Text[] _text,int[] _goal)
    {
        yield return null;
        for(int i=0;i<_text.Length;i++)
        {
            int number = 0;
            int num = 0;
            while (true)
            {
                yield return new WaitForSeconds(0.02f);
                number++;
                num = Random.Range(0, _goal[i]);
                _text[i].text = num.ToString();
                if (number == 50)
                {
                    _text[i].text = _goal[i].ToString();
                    NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.BREAK);
                    break;
                }
            }
        }
        yield return new WaitForSeconds(1.3f);
        if(GiftOpenbool)
        {
            for (int i = 0; i < Giftsetactive.Length; i++)
            {
                Giftsetactive[i].SetActive(true);
                Debug.Log("나타났다");
            }
            GiftObjet.SetActive(true);
        }
    }
    bool GiftOpenbool = false;
    public void GiftObjectOn()
    {
        GiftOpenbool = true;
    }
    public bool checkbool2=false;
   // public GameObject checkAnimation;
    IEnumerator GiftClick2()
    {
        KHS_Objectmanager.instance.GiftButton.GetComponent<Animator>().SetTrigger("Click");
        GiftObjet.GetComponent<Animator>().SetTrigger("Click");
       
        KHS_Objectmanager.instance.GiftButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        //while(true)
        //{
        //    if (GiftObjet.GetComponent<Animator>().GetBool("Check") == true)
        //        break;
        //}
       
    }
    bool check2 = true;
    private void Update()
    {
        if (GiftObjet.GetComponent<Animator>().GetBool("Check") == true && check2==true)
        {
            check2 = false;
           // GiftObjet.SetActive(false);
            StartCoroutine("GiftClick3");
        }
    }
    public GameObject[] Giftsetactive;
    public Image Giftmoney;
    public Text GiftText2;
    public GameObject ResultImsi;
    public void GiftOff()
    {
        for(int i=0;i<Giftsetactive.Length;i++)
        {
            Giftsetactive[i].SetActive(false);
            Debug.Log("사라진다");
        }
    }
    IEnumerator GiftClick3()
    {
        yield return new WaitForSeconds(0.0f);
       // GiftObjet.SetActive(false);
        int RandomNum = Random.Range(0, 81);
        for (int i = 0; i < Giftsetactive.Length; i++)
        {
            Giftsetactive[i].gameObject.SetActive(false);
            Debug.Log("사라진다");
        }
        ResultImsi.SetActive(true);
        if (RandomNum <= 60)
        {
            GiftOff();
            GiftText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + 200);
            GiftText.text = "200";
            Giftmoney.gameObject.SetActive(true);
            GiftText2.gameObject.SetActive(true);
            KHS_Objectmanager.instance.Boxfail.SetActive(true);
            GiftText2.text = "200";
        }
        else if (RandomNum <= 80)
        {
            GiftOff();
            GiftText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + 400);
            GiftText.text = "400";
            Giftmoney.gameObject.SetActive(true);
            GiftText2.gameObject.SetActive(true);
            KHS_Objectmanager.instance.Boxfail.SetActive(true);
            GiftText2.text = "400";
        }
        else
        {
            GiftText.text = "";
            KHS_Objectmanager.instance.GiftButton.gameObject.SetActive(false);
            KHS_Objectmanager.instance.Boxfail.SetActive(true);
        }
        GiftClickbool = true;
      
    }
    bool GiftClickbool = false;
    public void GiftClick()
    {
        if (!GiftClickbool)
            StartCoroutine(GiftClick2());
        else
            GiftObjet.SetActive(false);
    }
}
