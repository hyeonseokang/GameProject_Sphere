using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSJShopMng : MonoBehaviour {


    void Start()
    {
        for (int i = 0; i < SkillList.Length; i++)
        {//초기 스킬리스트 좌표정리
            SkillList[i].transform.localPosition = new Vector2(i * 720, 0);
        }
        ToggleList = GameObject.Find("Toggle1").GetComponentsInChildren<Toggle>();

        SkillEquipImage[0].sprite =
           SkillList[PlayerPrefs.GetInt("SKILL1") - 1].transform.FindChild("Name2").GetComponent<Image>().sprite;
        SkillEquipImage[1].sprite =
        SkillList[PlayerPrefs.GetInt("SKILL2") - 1].transform.FindChild("Name2").GetComponent<Image>().sprite;

        for (int i = 0; i < 2; i++)
            CShop[i].SetActive(false);

        if (PlayerPrefs.GetInt("SKILLITEM1")== 10)//구매가 안되있다면 
        {
            SkillList[0].transform.FindChild("Image (1)").gameObject.SetActive(false);

        }
        if (PlayerPrefs.GetInt("SKILLITEM2") == 10)//구매가 안되있다면 
        {
            SkillList[1].transform.FindChild("Image (1)").gameObject.SetActive(false);

        }
        if (PlayerPrefs.GetInt("SKILLITEM3") == 10)//구매가 안되있다면 
        {
            SkillList[2].transform.FindChild("Image (1)").gameObject.SetActive(false);

        }
        if (PlayerPrefs.GetInt("SKILLITEM4") == 10)//구매가 안되있다면 
        {
            SkillList[3].transform.FindChild("Image (1)").gameObject.SetActive(false);

        }
        money.text = PlayerPrefs.GetInt("GOLD").ToString();
    }
    private void Update()
    {
        Skillparent.transform.localPosition = Vector2.MoveTowards(Skillparent.transform.localPosition,
            new Vector2(-1 * ((NowSkill-1) * 720), 0),Time.deltaTime*2000);
    }

    public GameObject[] CShop;

    public GameObject Skillparent;
    public GameObject[] SkillList; //스킬목록
    private int NowSkill = 1;//보고있는 스킬 번호 
    private Toggle[] ToggleList;

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
        if (_num == 1)
        {
            if (NowSkill < SkillList.Length)
            {
                NowSkill++;
                ToggleList[NowSkill - 2].isOn = false;
                ToggleList[NowSkill - 1].isOn = true;
            }
        }
    }
    public GameObject SkillChoice;
    public void CloseSkillChoice()
    {
        SkillChoice.SetActive(false);
    }
    public void prevButton_Skill(int _num)
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        if (_num == 1)
        {
            if (NowSkill != 1)
            {
                NowSkill--;
                ToggleList[NowSkill].isOn = false;
                ToggleList[NowSkill - 1].isOn = true;
            }
        }
    }

    /////////////////////////////////////////////////
    public GameObject SkillChange;
    public Image SkillChangeImage;
    public Text SkillChangeText;
    public Image SkillChangename;
    public Image[] SkillEquipImage;
    public int[] price;
    public Text money;
   // public Sprite[] SkillSprite;
    public void buyButton()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        if (PlayerPrefs.GetInt("SKILLITEM" + NowSkill.ToString()) != 10)//구매가 안되있다면 
        {
            if (price[NowSkill - 1] <= PlayerPrefs.GetInt("GOLD"))
            {
                SkillList[NowSkill - 1].transform.FindChild("Image (1)").gameObject.SetActive(false);
                PlayerPrefs.SetInt("SKILLITEM" + NowSkill.ToString(), 10);
                PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") - price[NowSkill - 1]);
                money.text = PlayerPrefs.GetInt("GOLD").ToString();
            }
        }
        else
        {
            //switch(SkillList[NowSkill - 1].transform.FindChild("Name").GetComponent<Text>().text)
            //{
            //    case "LAZER":
            //        SkillChangename.sprite = SkillSprite[0];
            //        break;
            //    case "SHIELD":
            //        SkillChangename.sprite = SkillSprite[1];
            //        break;
            //}
            SkillChangeImage.sprite = SkillList[NowSkill - 1].transform.FindChild("Image").GetComponent<Image>().sprite;
            SkillChangename.sprite = SkillList[NowSkill - 1].transform.FindChild("Name").GetComponent<Image>().sprite;
            //SkillChangeText.text = "SKILL" + NowSkill;
            SkillChange.SetActive(true);
        }

    }
    public void CloseChnage()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        SkillChange.SetActive(false);
    }
    int SkillTogglenum = 1;// 스킬 장착해제할때 1과 2선택 

    public void SkillToggle(int i)
    {
        SkillTogglenum = i;
    }

    public void SkillEquip()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        PlayerPrefs.SetInt("SKILL" + SkillTogglenum, NowSkill);
        SkillEquipImage[SkillTogglenum - 1].sprite = 
            SkillList[NowSkill - 1].transform.FindChild("Name2").GetComponent<Image>().sprite;
    }

    /////////////////////////////////////////////////////
    public void Goskinshop()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        CShop[0].SetActive(true);
        CShop[1].SetActive(false);
    }
    public void Goskillshop()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        CShop[0].SetActive(false);
        CShop[1].SetActive(true);
    }

    public void BackButton()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        for (int i = 0; i < 2; i++)
        {
            CShop[i].SetActive(false);
        }
        Debug.Log("efefef");
    }
    public void BackMenu()
    {
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.CLICK);
        Application.LoadLevel(0);
        Debug.Log("efefef");
    }
}