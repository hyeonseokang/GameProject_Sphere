using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_BossCreate : MonoBehaviour {
    public GameObject[] Boss;

    private void Awake()
    {
        Instantiate(Boss[KHS_GamaManager.instance.BossNumber], new Vector2(0, 4.0f), Quaternion.identity);
    }
}
