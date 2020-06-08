using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHBossBasement : NMHUnit
{
    /////////////////////////////
    ///↓여기 아래부터 private///
    ///↓!!!건드리지 말 것!!!!///
    /////////////////////////////

    Vector2 TargetBossVec2;

    bool bIsAutoMoving = false;
    bool bIsMoving = false;
    bool bIsChasing = false;

    ////////////////////////////
    ///↓여기 아래부터 public///
    ////////////////////////////

    public GameObject[] BossPrefab;
    public GameObject[] BossBulletPrefab;
    public GameObject BossBulletParent;

    public GameObject HeroObj;

    public Vector2 LeftUpVec2;
    public Vector2 RightDownVec2;
    public Vector2 CurBossVec2;

    public float fBossDefaultSpeed;
    public float fBossAutoMoveSpeed;
    public float fBossMoveSpeed;

    public float[] fBossBulletSpeed;

    public bool bIsRunningPattern = false;
    public bool bCallRunPattern = false;
    public int nCallRunPatternNum = 0;
    public int nBeforePatternNum = 99;

    public int nPhaseNum = 1;

    //////////////////////////
    ///↓여기 아래부터 함수///
    //////////////////////////

     protected void Start ()
    {
        InitializeHP();
        InitializeObjs();
    }
	
	protected void Update ()
    {
        CheckPhase();
        DebugBoss();
    }

    void InitializeObjs()
    {
        CallBossPlaceToVec2(new Vector2(0, 10));
        CallBossMoveToVec2(new Vector2(0, 2), 0.4f);//0.1f 
    }

    public void UpdateBossMove()
    {
        if (bIsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetBossVec2, fBossMoveSpeed);

            if ((transform.position.x == TargetBossVec2.x && transform.position.y == TargetBossVec2.y))
            {
                bIsMoving = false;
                bIsAutoMoving = false;
                if (PhaseChangebool == 1)
                    PhaseChangebool = 2;
            }
        }
    }

    public void UpdateBossChaseHero()
    {
        if (bIsChasing)
        {
            transform.position = new Vector3(HeroObj.transform.position.x, HeroObj.transform.position.y + 3f, 0);
        }
    }

    public void UpdateBossCurVec2()
    {
        CurBossVec2 = new Vector2(transform.position.x, transform.position.y);
    }

    public void UpdateBossAutoMove()
    {
        if(!bIsAutoMoving && !bIsRunningPattern)
        {
            bIsAutoMoving = true;

            float fAutoX = Random.Range(LeftUpVec2.x, RightDownVec2.x);
            float fAutoY = Random.Range(RightDownVec2.y, LeftUpVec2.y);

            Vector2 AutoVec2 = new Vector3(fAutoX, fAutoY);

            if (Time.timeScale == 0)
                AutoVec2 *= 0;
            if(PhaseChangebool==1)
            {
                AutoVec2 = new Vector2(0, 0);
            }
            else if(PhaseChangebool==2)
            {
                fBossAutoMoveSpeed = 0;
                StartCoroutine("PhaseChange2");
            }
            CallBossMoveToVec2(AutoVec2, fBossAutoMoveSpeed/40.0f);
        }
    }

    public IEnumerator CallBossAutoPattern(float _fLeastDelay, float _fBiggestDelay, int _NumberOfAllPattern)
    {
        if (!bIsRunningPattern)
        {
           // Debug.Log("Pattern");
            float nDelayToDelay = Random.Range(0, _fBiggestDelay - _fLeastDelay);

            yield return new WaitForSeconds(_fLeastDelay);
            yield return new WaitForSeconds(nDelayToDelay);

            bCallRunPattern = true;
           // Debug.Log("Run Pattern");

            while (true)
            {
                nCallRunPatternNum = Random.Range(0, _NumberOfAllPattern);

                if (nBeforePatternNum != nCallRunPatternNum)
                {
                    nBeforePatternNum = nCallRunPatternNum;
                    break;
                }
            }

            StartCoroutine(CallBossAutoPattern(_fLeastDelay, _fBiggestDelay, _NumberOfAllPattern));
        }
        else
        {
            yield return new WaitForSeconds(2.0f);

            bCallRunPattern = true;
           // Debug.Log("Run Pattern");

            while (true)
            {
                nCallRunPatternNum = Random.Range(0, _NumberOfAllPattern);

                if (nBeforePatternNum != nCallRunPatternNum)
                {
                    nBeforePatternNum = nCallRunPatternNum;
                    break;
                }
            }

            StartCoroutine(CallBossAutoPattern(_fLeastDelay, _fBiggestDelay, _NumberOfAllPattern));
        }
    }

    public IEnumerator CallBossCreateBullet(int _nType, Vector2 _BulletVec2, Vector2 _TargetVec2, float _fBulletSpeed, float _fBulletDelayTime, float _fCreateDelayTIme)
    {
        yield return new WaitForSeconds(_fCreateDelayTIme);

        GameObject CloneBossBullet = Instantiate(BossBulletPrefab[_nType], BossBulletParent.transform);
        CloneBossBullet.transform.localPosition = _BulletVec2;
        //if (_nType == 1)
        //{
        //    CloneBossBullet.GetComponent<Animator>().SetBool("Launch", true);
        //}
        CloneBossBullet.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(_TargetVec2.x - _BulletVec2.x, _TargetVec2.y - _BulletVec2.y, 0));
       // CloneBossBullet.GetComponent<NMHBossBullet>().TargetNormalVec3 = Vector3.Normalize(new Vector3(_BulletVec2.x - _TargetVec2.x, _BulletVec2.y - _TargetVec2.y, 0));
        CloneBossBullet.GetComponent<NMHBossBullet>().fBulletSpeed = _fBulletSpeed;
        CloneBossBullet.GetComponent<NMHBossBullet>().fDelayTime = _fBulletDelayTime;
    }

    public void CallBossMoveToVec2(Vector2 _TargetVec2, float _fSpeed)
    {
        fBossMoveSpeed = _fSpeed;

        bIsMoving = true;
        TargetBossVec2 = _TargetVec2;
    }

    public void CallBossPlaceToVec2(Vector2 _TargetVec2)
    {
        transform.position = new Vector3(_TargetVec2.x, _TargetVec2.y, 0);
    }

    public IEnumerator CallBossChaseHero(float _fDelay, float _fTime)
    {
        yield return new WaitForSeconds(_fDelay);

        bIsChasing = true;

        yield return new WaitForSeconds(_fTime);

        bIsChasing = false;
    }

    public void CallStopBossMove()
    {
        bIsMoving = false;
    }

    public void CallResumeBossMove()
    {
        bIsMoving = true;
    }

    public void RunAnimation(string _strAnim)
    {
        GetComponent<Animation>().Play(_strAnim);
    }
    int PhaseChangebool = 0;
    void ChangePhaseTo(int _nPhase)
    {
        if (nPhaseNum == 1)
        {
            if (GetComponent<NMHTriangleBoss>() != null)
            {
                PhaseChangebool = 1;
                bIsMoving = false;
                bIsAutoMoving = false;
            }
            else if (GetComponent<PSJRhombusBoss>() != null)
            {
                PhaseChangebool = 1;
                bIsMoving = false;
                bIsAutoMoving = false;
            }
            else if (GetComponent<PSJSquareBoss>() != null)
            {
                PhaseChangebool = 1;
                bIsMoving = false;
                bIsAutoMoving = false;
            }
        }

        nPhaseNum = _nPhase;

    }
    IEnumerator PhaseChange2()
    {
        GetComponent<Animator>().SetTrigger("PhaseChange");
        yield return new WaitForSeconds(1.0f);
        bIsMoving = false;
        bIsAutoMoving = false;
        PhaseChangebool = 0;
    }
    void InitializeHP()
    {
        Debug.Log("HP : " + GetComponent<KHS_BossCollider>().HP);
        //Debug.Log("name : " + name);
        nMaxHP = GetComponent<KHS_BossCollider>().HP;
        nCurHP = nMaxHP;
    }

    void CheckPhase()
    {
        nCurHP = GetComponent<KHS_BossCollider>().HP;

        if (nCurHP  <= nMaxHP / 2)
        {
            ChangePhaseTo(2);
        }
    }

    void DamageBoss(int _nDamage)
    {
        if(GameObject.Find("BossHPBar") != null)
        {
            GetComponent<KHS_BossCollider>().HP -= _nDamage;
            nCurHP = GetComponent<KHS_BossCollider>().HP;

            GameObject.Find("BossHPBar").GetComponent<NMHBossHPBar>().SetHPBarGaugeByPercent((float)((float)nCurHP / (float)nMaxHP * 100f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pbullet") || collision.CompareTag("Lazer"))
        {
            DamageBoss(1);
        }
    }

    void DebugBoss()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("fuckSex");
            DamageBoss(50);
        }
    }
}
