using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_BossHpInit : MonoBehaviour {
    public Sprite[] Bosshp1Sprite;
    public Sprite[] Bosshp2Sprite;

    private void Start()
    {
        GameObject Bosshp1 = GameObject.Find("BossHp1");
        GameObject Bosshp2 = GameObject.Find("BossHp2");
        Bosshp1.GetComponent<SpriteRenderer>().sprite = Bosshp1Sprite[KHS_GamaManager.instance.BossNumber];
        Bosshp2.GetComponent<SpriteRenderer>().sprite = Bosshp2Sprite[KHS_GamaManager.instance.BossNumber];
        if(KHS_GamaManager.instance.BossNumber==1)
        {
            Bosshp2.GetComponent<SpriteRenderer>().size = new Vector2(4.408951f, 4.449821f);
            Bosshp2.transform.position = new Vector2(0.03f, 0.03f);
        }
    }
}
