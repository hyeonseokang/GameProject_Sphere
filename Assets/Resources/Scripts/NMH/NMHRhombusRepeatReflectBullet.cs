using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHRhombusRepeatReflectBullet : NMHBossBullet
{
    public int nHP = 10;

    float fRotZ = 0;

    void Start()
    {
        InitializeObjs();
    }

    void Update()
    {
        MoveBossBullet();

        CheckPosition();
        CheckHP();
    }

    void CheckHP()
    {
        if (nHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CheckPosition()
    {
        if(transform.position.y <= 0)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pbullet") || collision.gameObject.CompareTag("Lazer"))
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);
            if (collision.gameObject.CompareTag("Pbullet"))
                Destroy(collision.gameObject);
            nHP--;
        }
    }
}
