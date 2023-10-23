using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSquareNormalBullet : NMHBossBullet
{
    public int nHP = 1;

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

        if (nHP == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
