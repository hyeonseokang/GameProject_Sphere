using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSquareSuperRazerBullet : NMHBossBullet
{
    public int nHP = 5;

    void Start()
    {
        InitializeObjs();
    }

    void Update()
    {
        MoveBossBullet();

        CheckHP();
    }

    void CheckHP()
    {
        int nHPCnt = 0;

        foreach (Transform child in transform)
        {
            nHPCnt++;
        }

        nHP = nHPCnt;

        if (nHP == 2)
        {
            Destroy(this.gameObject);
        }
    }
}
