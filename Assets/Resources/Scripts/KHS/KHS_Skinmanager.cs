using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_Skinmanager : MonoBehaviour {
    public GameObject[] SkillList; //스킬목록
    private int NowSkill = 1;//보고있는 스킬 번호 
    public Toggle[] ToggleList;
    public GameObject Skillparent;

    public Sprite[] equipsprites;
    public Image BuyImage;
    public GameObject[] PriceObject;
    public Text money;
    int equipnum;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < SkillList.Length; i++)
        {//초기 스킬리스트 좌표정리
            SkillList[i].transform.localPosition = new Vector2(i * 720, 0);
        }
     
        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("!SKIN" + i.ToString()) == 1)
            {
                equipnum = i;
                break;
            }
            if(PlayerPrefs.GetInt("SKIN"+i.ToString())==1)
            {
                PriceObject[i - 1].SetActive(false);
            }
        }
        if (equipnum == NowSkill)
        {
            BuyImage.sprite = equipsprites[1];
        }
        else
        {
            BuyImage.sprite = equipsprites[0];
        }
        //ToggleList = GameObject.Find("Toggle2").GetComponentsInChildren<Toggle>();
    }
	
	// Update is called once per frame
	void Update () {
        Skillparent.transform.localPosition = Vector2.MoveTowards(Skillparent.transform.localPosition,
           new Vector2(-1 * ((NowSkill - 1) * 720), 0), Time.deltaTime * 2000);
    }

    IEnumerator moveSkillList(int Direction)
    {
        for (int i = 0; i < 720; i++)
        {
            yield return null;
            Skillparent.transform.localPosition = new Vector2(Skillparent.transform.localPosition.x + Direction, 0);
        }

    }

    public void nextButton_Skill(int _num)
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        if (_num == 2)
        {
            if (NowSkill < SkillList.Length)
            {
                NowSkill++;
                ToggleList[NowSkill - 2].isOn = false;
                ToggleList[NowSkill - 1].isOn = true;
                if(equipnum==NowSkill)
                {
                    BuyImage.sprite = equipsprites[1];
                }
                else
                {
                    BuyImage.sprite = equipsprites[0];
                }
            }
        }
    }
    public void prevButton_Skill(int _num)
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        if (_num == 2)
        {
            if (NowSkill != 1)
            {
                NowSkill--;
                ToggleList[NowSkill].isOn = false;
                ToggleList[NowSkill - 1].isOn = true;
                if (equipnum == NowSkill)
                {
                    BuyImage.sprite = equipsprites[1];
                }
                else
                {
                    BuyImage.sprite = equipsprites[0];
                }
            }
        }
    }

    public void BuyButton()
    {
        if (PlayerPrefs.GetInt("SKIN" + NowSkill) != 1)
        {
            if (PlayerPrefs.GetInt("GOLD") >= 5000)
            {
                PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD"));
                PlayerPrefs.SetInt("SKIN" + NowSkill, 1);
                PriceObject[NowSkill - 1].SetActive(false);
                PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") - 5000);
                money.text = PlayerPrefs.GetInt("GOLD").ToString();
            }
        }
        else if (PlayerPrefs.GetInt("SKIN" + NowSkill) == 1)
        {
            PlayerPrefs.SetInt("!SKIN1", 0);
            PlayerPrefs.SetInt("!SKIN2", 0);
            PlayerPrefs.SetInt("!SKIN3", 0);
            PlayerPrefs.SetInt("!SKIN4", 0);
            PlayerPrefs.SetInt("!SKIN" + NowSkill, 1);
            BuyImage.sprite = equipsprites[1];
            equipnum = NowSkill;

        }
    }
}
