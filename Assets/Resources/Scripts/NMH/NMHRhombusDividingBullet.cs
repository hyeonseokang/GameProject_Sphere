using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHRhombusDividingBullet : NMHBossBullet
{
    public int nHP = 4;

    void Start()
    {
        InitializeObjs();
    }

    void Update()
    {
        MoveBossBullet();

        CheckPosition();
    }

    void CheckPosition()
    {
        if (transform.position.y <= 0 && nHP == 4)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pbullet") )
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);

            if (nHP != 1)
            {
                GameObject NextBulletObj0 = Instantiate(this.gameObject);
                NextBulletObj0.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(-1.5f, 5, 0) - this.gameObject.transform.position);
                NextBulletObj0.GetComponent<NMHBossBullet>().fBulletSpeed = 3f;
                NextBulletObj0.GetComponent<NMHBossBullet>().fDelayTime = 0f;
                NextBulletObj0.GetComponent<NMHRhombusDividingBullet>().nHP = nHP - 1;

                GameObject NextBulletObj1 = Instantiate(this.gameObject);
                NextBulletObj1.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(1.5f, 5, 0) - this.gameObject.transform.position);
                NextBulletObj1.GetComponent<NMHBossBullet>().fBulletSpeed = 3f;
                NextBulletObj1.GetComponent<NMHBossBullet>().fDelayTime = 0f;
                NextBulletObj1.GetComponent<NMHRhombusDividingBullet>().nHP = nHP - 1;
            }

            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Lazer"))
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.transform.position, Quaternion.identity);

            if (nHP != 1)
            {
                GameObject NextBulletObj0 = Instantiate(this.gameObject);
                NextBulletObj0.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(-1.5f, 5, 0) - this.gameObject.transform.position);
                NextBulletObj0.GetComponent<NMHBossBullet>().fBulletSpeed = 3f;
                NextBulletObj0.GetComponent<NMHBossBullet>().fDelayTime = 0f;
                NextBulletObj0.GetComponent<NMHRhombusDividingBullet>().nHP = nHP - 1;

                GameObject NextBulletObj1 = Instantiate(this.gameObject);
                NextBulletObj1.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(1.5f, 5, 0) - this.gameObject.transform.position);
                NextBulletObj1.GetComponent<NMHBossBullet>().fBulletSpeed = 3f;
                NextBulletObj1.GetComponent<NMHBossBullet>().fDelayTime = 0f;
                NextBulletObj1.GetComponent<NMHRhombusDividingBullet>().nHP = nHP - 1;
            }

            Destroy(this.gameObject);
        }
    }
}
