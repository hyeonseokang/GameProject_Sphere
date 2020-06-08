using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PSJShop : MonoBehaviour {

    int I_Num = 0;
    public GameObject[] Item;
    public Toggle[] ItemNumber;
    public GameObject CantNext;
    bool equip;

	// Use this for initialization
	void Start () {
        I_Num = PlayerPrefs.GetInt("ItemNum");
        Item_Active(I_Num);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(PlayerPrefs.GetInt("ItemNum"));
        Lock_Item();

    }

    public void NextButton(Button BT)
    {       
        if (I_Num < 4)
        {
            I_Num+=1;
            Item_Active(I_Num);
        }
    }

    public void PrecedentButton(Button BT)
    {
        if (I_Num > 0)
        {
            I_Num-=1;
            Item_Active(I_Num);
        }       
    }

    public void Back_Button(Button BT)
    {
        SceneManager.LoadScene("0_MainMenu");
    }

    void Item_Active(int i)
    {
        for (int j = 0; j < 5; j++)
        {
            Item[j].SetActive(false);
        }
        Item[i].SetActive(true);
        ItemNumber[i].isOn = true;
    }

    void Lock_Item()
    {
        if (I_Num > 2)
        {
            CantNext.SetActive(true);
            equip = false;
        }
        else
        {
            CantNext.SetActive(false);
            equip = true;
        }
    }

    public void Equipment(Button BT)
    {
        if(equip)
        PlayerPrefs.SetInt("ItemNum",I_Num);
    }
}
