using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHRhombusBigBullet : NMHBossBullet
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
            GetComponentInChildren<Animator>().SetTrigger("Explode");

           // DestroyObj();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pbullet") )
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);
            nHP--;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Lazer"))
        {

            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);
            nHP--;
        }
    }

    void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
