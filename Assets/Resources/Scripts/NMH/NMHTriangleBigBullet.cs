using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHTriangleBigBullet : NMHBossBullet
{
    public int nHP = 4;

	void Start ()
    {
        InitializeObjs();
    }
	
	void Update ()
    {
        MoveBossBullet();

        CheckHP();
    }

    void CheckHP()
    {
        int nHPCnt = 0;

        foreach(Transform child in transform)
        {
            nHPCnt++;
        }

        nHP = nHPCnt;

        if(nHP == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
