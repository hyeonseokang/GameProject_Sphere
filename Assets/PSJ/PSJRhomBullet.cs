using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJRhomBullet : NMHBossBullet {

    int x = 0;
    int y = 0;

    float fRotZ = 0f;

	void Start ()
    {

    }
	
	void Update ()
    {
        // Debug.Log(GetComponent<NMHBossBullet>().TargetVec2.x);

        fRotZ += 4;

        GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, fRotZ);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {

        if (collision.CompareTag("XWall"))
        {
            Debug.Log("adad");
            //(GetComponent<NMHBossBullet>().TargetVec2.x) = -(GetComponent<NMHBossBullet>().TargetVec2.x);
            //(GetComponent<NMHBossBullet>().TargetVec2.y) = 2.5f * (GetComponent<NMHBossBullet>().TargetVec2.y);
            
            GetComponent<NMHBossBullet>().TargetNormalVec3.x *= -1;

        }
        if(collision.CompareTag("YWall") && transform.position.y > 0)
        {
            //if (GetComponent<NMHBossBullet>().TargetVec2.y < -6)
            //{
            //    Destroy(this.gameObject);
            //}

            //(GetComponent<NMHBossBullet>().TargetVec2.y) = -(GetComponent<NMHBossBullet>().TargetVec2.y);
            //(GetComponent<NMHBossBullet>().TargetVec2.x) = 2.5f * (GetComponent<NMHBossBullet>().TargetVec2.x);

              GetComponent<NMHBossBullet>().TargetNormalVec3.y *= -1;
        }

    }


}
