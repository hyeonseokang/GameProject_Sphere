using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSquareMovingBullet : NMHBossBullet
{
    public int nHP = 2;

    public GameObject MovingBulletObj;

    public int nMovingState = 0;

    void Start()
    {
        InitializeObjs();
    }

    void Update()
    {
        MoveBossBullet();

        CheckHP();

        MoveBullet();
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

    void MoveBullet()
    {
        if (MovingBulletObj != null)
        {
            if (nMovingState == 0)
            {
                MovingBulletObj.transform.Translate(-transform.right * Time.deltaTime * 3f);
            }
            else if (nMovingState == 1)
            {
                MovingBulletObj.transform.Translate(transform.right * Time.deltaTime * 3f);
            }
        }
        if(MovingBulletObj.transform.position.x >= 3f && nMovingState == 0)
        {
            nMovingState = 1;
        }
        else if(MovingBulletObj.transform.position.x <= -3f && nMovingState == 1)
        {
            nMovingState = 0;
        }
    }
}
