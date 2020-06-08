using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHBossBullet : NMHSprite
{
    public SpriteRenderer BulletSprR;

    public Vector3 TargetNormalVec3;

    public bool bIsMoving = false;
    public float fBulletSpeed;
    public float fDelayTime;

    public float fAngle = 0;
    public float fRotSpeed = 10f;

    public int nBulletType;
    public int nBulletHP;
    

    void Start()
    {
        InitializeObjs();
    }

    void Update()
    {
        MoveBossBullet();
    }

    public void InitializeObjs()
    {
        Invoke("ChangeMovingTrue", fDelayTime);

        fAngle = (Mathf.Atan2(TargetNormalVec3.y, TargetNormalVec3.x) * Mathf.Rad2Deg) + 90;

        if (BulletSprR != null)
        {
            BulletSprR.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
        }
    }

    public void MoveBossBullet()
    {
        if (bIsMoving)
        {
            transform.Translate(TargetNormalVec3 * fBulletSpeed * Time.deltaTime);
        }

        fAngle = (Mathf.Atan2(TargetNormalVec3.y, TargetNormalVec3.x) * Mathf.Rad2Deg) + 90;

        if (BulletSprR != null)
        {
            BulletSprR.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
        }
    }

    void ChangeMovingTrue()
    {
        bIsMoving = true;
    }
}
