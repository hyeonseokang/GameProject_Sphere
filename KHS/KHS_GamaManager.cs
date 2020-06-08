using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_GamaManager : MonoBehaviour {
    public static KHS_GamaManager instance = null;

    private int _bossnumber;//어떤 보스인지 
    public int BossNumber
    {
        get
        {
            return _bossnumber;
        }
        set
        {
            _bossnumber = value;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void BackMenu()
    {
        Application.LoadLevel(0);
        KHS_EffectSoundManager.instance.ButtonClickOn();
    }

}
