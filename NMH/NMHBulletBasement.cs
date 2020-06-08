using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHBulletBasement : MonoBehaviour
{
    //public SpriteRenderer BulletSprR;

    //public Vector3 TargetNormalVec3;

    //public bool bIsMoving = false;
    //public float fBulletSpeed;
    //public float fDelayTime;

    //public float fAngle = 0;
    //public float fRotSpeed = 10f;

    //void Start()
    //{
    //    InitializeObjs();
    //}

    //void Update()
    //{
    //    MoveBossBullet();
    //}

    //void InitializeObjs()
    //{
    //    Invoke("ChangeMovingTrue", fDelayTime);

    //    fAngle = (Mathf.Atan2(TargetNormalVec3.y, TargetNormalVec3.x) * Mathf.Rad2Deg) + 90;

    //    BulletSprR.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
    //}

    //void MoveBossBullet()
    //{
    //    if (bIsMoving)
    //    {
    //        transform.Translate(TargetNormalVec3 * fBulletSpeed * Time.deltaTime);
    //    }

    //    fAngle = (Mathf.Atan2(TargetNormalVec3.y, TargetNormalVec3.x) * Mathf.Rad2Deg) + 90;

    //    BulletSprR.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
    //}

    //void ChangeMovingTrue()
    //{
    //    bIsMoving = true;
    //}

    public void RunAnimation(string _strAnim)
    {
        GetComponent<Animation>().Play(_strAnim);
    }
}
