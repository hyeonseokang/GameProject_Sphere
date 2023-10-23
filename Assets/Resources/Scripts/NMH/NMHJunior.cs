using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHJunior : NMHUnit
{
    /////////////////////////////
    ///↓여기 아래부터 private///
    ///↓!!!건드리지 말 것!!!!///
    /////////////////////////////

    Vector2 TargetJuniorVec2;

    bool bIsAutoMoving = false;
    bool bIsMoving = false;

    ////////////////////////////
    ///↓여기 아래부터 public///
    ////////////////////////////

    public GameObject HeroObj;

    public GameObject JuniorBulletPrefab;
    public GameObject JuniorBulletParent;

    public Vector2 LeftUpVec2;
    public Vector2 RightDownVec2;
    public Vector2 CurJuniorVec2;

    public float fJuniorAutoMoveSpeed;
    public float fJuniorMoveSpeed;

    public bool bIsRunningPattern = false;
    public bool bCallRunPattern = false;

    //////////////////////////
    ///↓여기 아래부터 함수///
    //////////////////////////

    protected void Start()
    {
        InitializeObjs();
        InitializePos();
    }

    protected void Update()
    {
        CheckJuniorPos();

        UpdateJuniorMove();
        UpdateJuniorCurVec2();
    }

    void InitializeObjs()
    {
        InitializeHP();
    }

    void CheckJuniorPos()
    {
        CurJuniorVec2 = new Vector2(transform.position.x, transform.position.y);
    }

    void UpdateJuniorMove()
    {
        if (bIsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetJuniorVec2, fJuniorMoveSpeed);

            if ((transform.position.x == TargetJuniorVec2.x && transform.position.y == TargetJuniorVec2.y))
            {
                bIsMoving = false;
                bIsAutoMoving = false;
            }
        }
    }

    void UpdateJuniorCurVec2()
    {
        CurJuniorVec2 = new Vector2(transform.position.x, transform.position.y);
    }

    public IEnumerator CallJuniorAutoPattern(float _fLeastDelay, float _fBiggestDelay)
    {
        if (!bIsRunningPattern)
        {
            float nDelayToDelay = Random.Range(0, _fBiggestDelay - _fLeastDelay);

            yield return new WaitForSeconds(_fLeastDelay);
            yield return new WaitForSeconds(nDelayToDelay);

            bCallRunPattern = true;

            StartCoroutine(CallJuniorAutoPattern(_fLeastDelay, _fBiggestDelay));
        }
        else
        {
            yield return new WaitForSeconds(2.0f);

            bCallRunPattern = true;

            StartCoroutine(CallJuniorAutoPattern(_fLeastDelay, _fBiggestDelay));
        }
    }

    public IEnumerator CallJuniorCreateBullet(Vector2 _BulletVec2, Vector2 _TargetVec2, float _fBulletSpeed, float _fBulletDelayTime, float _fCreateDelayTIme)
    {
        yield return new WaitForSeconds(_fCreateDelayTIme);

        GameObject CloneBossBullet = Instantiate(JuniorBulletPrefab, JuniorBulletParent.transform);
        CloneBossBullet.transform.localPosition = _BulletVec2;

        CloneBossBullet.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(_TargetVec2.x - _BulletVec2.x, _TargetVec2.y - _BulletVec2.y, 0));
       
        CloneBossBullet.GetComponent<NMHBossBullet>().fBulletSpeed = _fBulletSpeed;
        CloneBossBullet.GetComponent<NMHBossBullet>().fDelayTime = _fBulletDelayTime;
    }

    public void CallJuniorMoveToVec2(Vector2 _TargetVec2, float _fSpeed)
    {
        fJuniorMoveSpeed = _fSpeed;

        bIsMoving = true;
        TargetJuniorVec2 = _TargetVec2;
    }

    public void CallJuniorPlaceToVec2(Vector2 _TargetVec2)
    {
        transform.position = new Vector3(_TargetVec2.x, _TargetVec2.y, 0);
    }

    public void CallStopJuniorMove()
    {
        bIsMoving = false;
    }

    public void CallResumeJuniorMove()
    {
        bIsMoving = true;
    }

    void InitializeHP()
    {
        nCurHP = nMaxHP;
    }

    void InitializePos()
    {
        transform.position = new Vector3(Random.Range(-3f, 3f), 10f, 0);

        CheckJuniorPos();
    }

    void DamageToJunior(int _nDamage)
    {
        nCurHP -= _nDamage;

        if(nCurHP <= 0)
        {
            nCurHP = 0;

            Dead();
        }
    }
    public int JuniorNum;
    public void DeadEffect()
    {
        if(JuniorNum==1)
        {
            Instantiate(KHS_Objectmanager.instance.Junior[0], gameObject.transform.position, Quaternion.identity);
        }
        else if (JuniorNum == 2)
        {
            Instantiate(KHS_Objectmanager.instance.Junior[1], gameObject.transform.position, Quaternion.identity);
        }
        else if (JuniorNum == 3)
        {
            Instantiate(KHS_Objectmanager.instance.Junior[2], gameObject.transform.position, Quaternion.identity);
        }
    }
    void Dead()
    {
        NMHInfiniteModeMng.instance.SetCurLeftJunior(NMHInfiniteModeMng.instance.GetCurLeftJunior() - 1);
        DeadEffect();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pbullet"))
        {
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.gameObject.transform.position, Quaternion.identity);
            DamageToJunior(1);
            Destroy(collision.gameObject);
          
        }
        else if(collision.CompareTag("Lazer"))
        {
            DamageToJunior(1);
        }
    }
}
