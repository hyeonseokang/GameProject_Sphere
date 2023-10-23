using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OHSBackGroundSound : MonoBehaviour
{
    public static OHSBackGroundSound instance = null;

    public bool bIsOn = true;

    public AudioSource BGMsrc;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start () {

    }
}


