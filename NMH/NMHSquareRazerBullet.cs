using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSquareRazerBullet : NMHBossBullet
{
    public int nHP = 3;

    void Start()
    {
        InitializeObjs();

        float Xpos = Random.Range(-1.65f, 1.65f);
        gameObject.transform.position = new Vector2(Xpos, gameObject.transform.position.y);
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

        if (nHP == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
