using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_SkinPlayer : MonoBehaviour {
    public Sprite[] PlayerImage;
	// Use this for initialization
	void Start () {
		for(int i=0;i<4;i++)
        {
            if(PlayerPrefs.GetInt("!SKIN"+(i+1))==1)
            {
                GetComponent<SpriteRenderer>().sprite = PlayerImage[i];
                break;
            }
        }
	}
}
