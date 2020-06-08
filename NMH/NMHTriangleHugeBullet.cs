using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHTriangleHugeBullet : NMHBossBullet
{
    public int nHP = 10;

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
        if (nHP <= 0)
        {
            DestroyObj();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pbullet") || collision.gameObject.CompareTag("Lazer"))
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);
            nHP--;
            if (collision.gameObject.CompareTag("Pbullet"))
                Destroy(collision.gameObject);
        }
    }

    void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
