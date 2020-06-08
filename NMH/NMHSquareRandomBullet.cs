using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSquareRandomBullet : NMHBossBullet
{
    public int nHP = 5;

    public GameObject[] RandomBulletObj;

    void Start()
    {
        InitializeObjs();

        RandomizeBullet();
    }

    void Update()
    {
        MoveBossBullet();

        CheckHP();
    }

    void RandomizeBullet()
    {
        for(int i = 0; i < 4; i ++)
        {
            RandomBulletObj[i].transform.localPosition = new Vector3(Random.Range(-3f, 3f), 0, 0);
        }
    }

    void CheckHP()
    {
        int nHPCnt = 0;

        foreach (Transform child in transform)
        {
            nHPCnt++;
        }

        nHP = nHPCnt;

        if (nHP == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
