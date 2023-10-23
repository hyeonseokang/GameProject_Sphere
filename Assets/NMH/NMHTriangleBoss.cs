using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHTriangleBoss : NMHBossBasement
{
    private Animator animator;
    public Vector2 BossUpPatternVec2;
    public Vector2 BossPatternVec2;

    public float fBossQuadSpeed;
    public float fBossFadeShotSpeed = 7f;

    enum Phase1Pattern
    {
        TRIPLE_SHOT,
        BIG_SHOT,
        QUAD_SHOT,
    }

    enum Phase2Pattern
    {
        FADE_SHOT,
        RAPID_SHOT,
        HUGE_SHOT
    }

    enum BossBulletType
    {
        NORMAL,
        BIG,
        HUGE
    }

    new void Start ()
    {
        base.Start();
        InitializeBossObjs();
        animator = GetComponent<Animator>();
    }
	
	new void Update ()
    {
        base.Update();

        DebugPattern();

        UpdateBossMove();
        UpdateBossAutoMove();
        UpdateBossCurVec2();
        UpdateBossChaseHero();

        UpdateRunBossPattern(nCallRunPatternNum);
    }
    
    void InitializeBossObjs()
    {
        StartCoroutine(CallBossAutoPattern(5f, 7f, 3));

        HeroObj = GameObject.Find("Player");
        BossBulletParent = GameObject.Find("BossBullet");

        LeftUpVec2 = new Vector2(-2f, 5f);
        RightDownVec2 = new Vector2(2f, -0.5f);
        BossPatternVec2 = new Vector2(0, 3.5f);
        BossUpPatternVec2 = new Vector2(0, 9.5f);

        fBossDefaultSpeed = 0.25f;
        fBossAutoMoveSpeed = 0.05f; 
        fBossMoveSpeed = 0.5f;

        fBossQuadSpeed = 0.25f;

        fBossBulletSpeed = new float[3];
        fBossBulletSpeed[(int)BossBulletType.NORMAL] = 7.5f;
        fBossBulletSpeed[(int)BossBulletType.BIG] = 2f;
        fBossBulletSpeed[(int)BossBulletType.HUGE] = 1f;
    }

    void UpdateRunBossPattern(int _nType)
    {
        if (bCallRunPattern && !bIsRunningPattern)
        {
            bCallRunPattern = false;
            bIsRunningPattern = true;

            switch(nPhaseNum)
            {
                case 1:
                    int tmpPhase1Pattern = Random.Range(0, 3);

                    switch (tmpPhase1Pattern)
                    {
                        case (int)Phase1Pattern.TRIPLE_SHOT:
                            StartCoroutine(BossTripleShot());
                            break;

                        case (int)Phase1Pattern.BIG_SHOT:
                            StartCoroutine(BossBigShot());
                            break;

                        case (int)Phase1Pattern.QUAD_SHOT:
                            StartCoroutine(BossQuadShot());
                            break;
                    }
                    break;
                case 2:
                    Debug.Log("2");

                    int tmpPhase2Pattern = Random.Range(0, 3);

                    switch (tmpPhase2Pattern)
                    {
                        case (int)Phase2Pattern.FADE_SHOT:
                            StartCoroutine(BossFadeShot());
                            break;

                        case (int)Phase2Pattern.RAPID_SHOT:
                            StartCoroutine(BossRapidShot());
                            break;

                        case (int)Phase2Pattern.HUGE_SHOT:
                            StartCoroutine(BossHugeShot());
                            break;
                    }
                    break;
            }
        }
    }

    IEnumerator BossTripleShot()
    {
        CallStopBossMove();

        StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, new Vector2(CurBossVec2.x + 0f, CurBossVec2.y - 1.7f), new Vector2(0, -10), fBossBulletSpeed[(int)BossBulletType.NORMAL], 0.6f, 0.2f));
        StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, new Vector2(CurBossVec2.x + 1.7f, CurBossVec2.y + 1.7f), new Vector2(0, -10), fBossBulletSpeed[(int)BossBulletType.NORMAL], 0.8f, 0.35f));
        StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, new Vector2(CurBossVec2.x - 1.7f, CurBossVec2.y + 1.7f), new Vector2(0, -10), fBossBulletSpeed[(int)BossBulletType.NORMAL], 0.85f, 0.5f));

        yield return new WaitForSeconds(0.6f);

        RunAnimation("TripleShotAnim");

        yield return new WaitForSeconds(1f);

        CallResumeBossMove();

        bIsRunningPattern = false;
    }

    IEnumerator BossBigShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);
        StartCoroutine(CallBossCreateBullet((int)BossBulletType.BIG, new Vector2(BossPatternVec2.x, BossPatternVec2.y), new Vector2(BossPatternVec2.x, -10), fBossBulletSpeed[(int)BossBulletType.BIG], 0.5f, 0.25f));

        yield return new WaitForSeconds(1f);

        bIsRunningPattern = false;
    }

    IEnumerator BossQuadShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossQuadSpeed);

        yield return new WaitForSeconds(0.75f);

        CallBossMoveToVec2(BossUpPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(0.75f);

        CallBossPlaceToVec2(new Vector2(5.0f, 4.5f));

        yield return new WaitForSeconds(0.3f);

        CallBossMoveToVec2(new Vector2(-5.0f, 4.5f), fBossDefaultSpeed);

        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, new Vector2(3.6f - 1.44f * (i + 1), 4.5f), new Vector2(3.6f - 1.44f * (i + 1), -10), fBossBulletSpeed[(int)BossBulletType.NORMAL], 0.75f,  0.2f * i));
        }

        yield return new WaitForSeconds(0.75f);

        CallBossPlaceToVec2(BossUpPatternVec2);

        yield return new WaitForSeconds(0.75f);

        CallBossMoveToVec2(BossPatternVec2, fBossQuadSpeed);

        yield return new WaitForSeconds(1.25f);

        bIsRunningPattern = false;
    }

    IEnumerator BossFadeShot()
    {
        CallStopBossMove();

        FadeOut(0.25f, 0f);

        yield return new WaitForSeconds(0.25f);

        CallBossPlaceToVec2(new Vector2(Random.Range(LeftUpVec2.x, RightDownVec2.x), Random.Range(LeftUpVec2.y, RightDownVec2.y)));

        for (int i = 0; i < 5; i++)
        {
            FadeIn(0.25f, 1f);
            animator.SetTrigger("Fade");
            yield return new WaitForSeconds(0.25f);

            StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, new Vector2(CurBossVec2.x, CurBossVec2.y - 1.5f), new Vector2(CurBossVec2.x, -10), fBossFadeShotSpeed, 0f, 0f));

            yield return new WaitForSeconds(0.75f);

            FadeOut(0.25f, 0f);
            animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(0.25f);

            CallBossPlaceToVec2(new Vector2(Random.Range(LeftUpVec2.x, RightDownVec2.x), Random.Range(LeftUpVec2.y, RightDownVec2.y)));
        }

        FadeIn(0.25f, 1f);

        yield return new WaitForSeconds(0f);

        bIsRunningPattern = false;

        CallResumeBossMove();
    }

    IEnumerator BossRapidShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.5f);

            Vector2 RapidVec2;

            while(true)
            {
                RapidVec2 = new Vector2(CurBossVec2.x + Random.Range(-2.0f, 2.0f), CurBossVec2.y + Random.Range(-2.0f, 2.0f));

                if (RapidVec2.x <= -1.5f || RapidVec2.x >= 1.5f && RapidVec2.y <= -1.5f || RapidVec2.y >= 1.5f)
                {
                    break;
                }
            }

            StartCoroutine(CallBossCreateBullet((int)BossBulletType.NORMAL, RapidVec2, new Vector2(HeroObj.transform.position.x, HeroObj.transform.position.y), fBossBulletSpeed[(int)BossBulletType.NORMAL], 0.5f, 0f));
        }

        yield return new WaitForSeconds(5f);

        bIsRunningPattern = false;
    }

    IEnumerator BossHugeShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 3; i++)
        {
            float tmpX = 0;
            GetComponent<Animator>().SetTrigger("Huge");
            do
            {
                tmpX = Random.Range(-1.75f, 1.75f);
            }
            while (Mathf.Abs(CurBossVec2.x - tmpX) < 0.5f);

            CallBossMoveToVec2(new Vector2(tmpX, BossPatternVec2.y), fBossDefaultSpeed);

            yield return new WaitForSeconds(0.75f);

            StartCoroutine(CallBossCreateBullet((int)BossBulletType.HUGE, new Vector2(CurBossVec2.x, CurBossVec2.y - 1.5f), new Vector2(CurBossVec2.x, CurBossVec2.y - 10f), fBossBulletSpeed[(int)BossBulletType.HUGE], 0.5f, 0f));

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0f);

        bIsRunningPattern = false;
    }

    void DebugPattern()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(BossHugeShot()); 
        }
    }
}
