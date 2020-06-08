using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_Objectmanager : MonoBehaviour {
    public static KHS_Objectmanager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject Player;

    public Image ResultWindow;
    public Button GiftButton;
    public GameObject HitEffect;
    public GameObject PB;//플레이어 총알 
    public GameObject PdownHitEffect;
    public Text GiftItemText;
    private int _Gold;
    public int Gold
    {
        get
        {
            return _Gold;
        }
        set
        {
            _Gold = value;
        }
    }

    public GameObject Clear;
    public GameObject Fail;
    public GameObject Box;
    public GameObject Box2;
    public GameObject Boxfail;

    public Sprite[] Icon;

    public Image pauseImage;

    public GameObject PlayerDeadEffect;
    bool _pause = false;
    public void pause()
    {
        if(!_pause)
        {
            pauseImage.gameObject.SetActive(true);
            _pause = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseImage.gameObject.SetActive(false);
            _pause = false;
            Time.timeScale = 1.0f;
        }
    }

    public GameObject B_ChangeEffect;

    public GameObject[] Junior;
}
