using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSJApplication : MonoBehaviour {
    // Use this for initialization
    int Pointer = 0;
    public Sprite[] HeroImage;
    public SpriteRenderer Ang;

    void Start () {
        Ang = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Image>().sprite = HeroImage[PlayerPrefs.GetInt("ItemNum")];
        Pointer = PlayerPrefs.GetInt("CharState");
}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetItem(Button BT)
    {
        Pointer++;
        if(Pointer > 4)
        {
            Pointer = 0;
        }
        PlayerPrefs.SetInt("CharState",Pointer);
        Application.LoadLevel(0);
    }
}
