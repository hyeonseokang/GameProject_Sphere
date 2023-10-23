using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PSJRhombusBoss : NMHBossBasement
{
    public GameObject RhombusBullet;

    public Animator RhombusAnimator;

    enum BossPatternType
    {
        Reflect_Shot,
        Repeat_Reflect_Shot,
        Turning_Shot
    }

    enum BossPatternPhase2
    {
        Moving_Reflect_Shot,
        Dividing_Shot,
        Big_Shot
    }

    enum BossBulletType
    {
        RhombusBullet,
        RepeatReflectBullet,
        DividingBullet,
        BigBullet
    }

    new void Start ()
    {
        base.Start();

        InitializeObjs();
    }

	new void Update ()
    {
        base.Update();

        UpdateBossAutoMove();
        UpdateBossChaseHero();
        UpdateBossCurVec2();
        UpdateBossMove();

        DebugPattern();

        RunBossPattern(nCallRunPatternNum);
    }

    void InitializeObjs()
    {
        HeroObj = GameObject.Find("Player");
        BossBulletParent = GameObject.Find("BossBullet");

        LeftUpVec2 = new Vector2(-2, 5);
        RightDownVec2 = new Vector2(2, 1);
        
        fBossAutoMoveSpeed = 0.035f;
        fBossMoveSpeed = 0.4f;

        StartCoroutine(CallBossAutoPattern(4f, 7f, 3));
    }

    void DebugPattern()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RunBossPattern(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RunBossPattern(1);
        }
    }

    void RunBossPattern(int _nType)
    {
        if (bCallRunPattern && !bIsRunningPattern)
        {
            bCallRunPattern = false;
            bIsRunningPattern = true;

            switch (nPhaseNum)
            {
                case 1:
                    switch (_nType)
                    {
                        case (int)BossPatternType.Reflect_Shot:
                            StartCoroutine(ReflectShot());
                            break;

                        case (int)BossPatternType.Repeat_Reflect_Shot:
                            StartCoroutine(RepeatReflectShot());
                            break;

                        case (int)BossPatternType.Turning_Shot:
                            StartCoroutine(TurningShot());
                            break;
                    }
                    break;
                case 2:
                    Debug.Log("2");

                    switch (_nType)
                    {
                        case (int)BossPatternPhase2.Moving_Reflect_Shot:
                            StartCoroutine(MovingReflectShot());
                            break;

                        case (int)BossPatternPhase2.Dividing_Shot:
                            StartCoroutine(DividingShot());
                            break;

                        case (int)BossPatternPhase2.Big_Shot:
                            StartCoroutine(BigShot());
                            break;
                    }
                    break;
            }
        }
    }

    IEnumerator ReflectShot()
    {
        for (int i = 0; i < 3; i++)
        {
            RhombusAnimator.SetTrigger("Turning");

            StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(-5, -1 - i * 1.2f), 3f, 0f, 0f));
            StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(5, -1 - i * 1.2f), 3f, 0f, 0f));

            yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(3f);

        bIsRunningPattern = false;
    }

    IEnumerator RepeatReflectShot()
    {
        CallBossMoveToVec2(new Vector2(0, 3.5f), fBossMoveSpeed);

        yield return new WaitForSeconds(1f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RepeatReflectBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0.5f, 0.5f));

        yield return new WaitForSeconds(1f);

        while(GameObject.Find("RepeatReflectBullet(Clone)").GetComponent<NMHRhombusRepeatReflectBullet>().nHP > 1)
        {
            StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(0, 0), new Vector2(-5, -1), 3f, 0.5f, 0.8f));
            StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(0, 0), new Vector2(5, -1), 3f, 0.5f, 0.8f));

            GameObject.Find("RepeatReflectBullet(Clone)").GetComponent<Animator>().SetTrigger("Repeat");
            yield return new WaitForSeconds(1.5f);

            if(GameObject.Find("RepeatReflectBullet(Clone)") == null)
            {
                break;
            }
        }

        yield return new WaitForSeconds(0f);

        bIsRunningPattern = false;
    }

    IEnumerator TurningShot()
    {
        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(-2, 5), 3f, 0f, 0f));
        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(-1, 5), 3f, 0f, 0f));
        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(1, 5), 3f, 0f, 0f));
        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(2, 5), 3f, 0f, 0f));

        RhombusAnimator.SetTrigger("Turning");

        yield return new WaitForSeconds(2f);
        
        bIsRunningPattern = false;
    }

    IEnumerator MovingReflectShot()
    {
        CallBossMoveToVec2(new Vector2(0, 3.5f), fBossMoveSpeed);
        GetComponent<Animator>().SetBool("Shiled", true);
        yield return new WaitForSeconds(1f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0f, 0f));

        CallBossMoveToVec2(new Vector2(-2.5f, 0f), fBossMoveSpeed * 1.5f);

        yield return new WaitForSeconds(2f / 1.5f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0f, 0f));

        CallBossMoveToVec2(new Vector2(0, -5f), fBossMoveSpeed * 1.5f);

        yield return new WaitForSeconds(1.3f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0f, 0f));

        CallBossMoveToVec2(new Vector2(2.5f, 0f), fBossMoveSpeed * 1.5f);

        yield return new WaitForSeconds(0.7f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.RhombusBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0f, 0f));

        CallBossMoveToVec2(new Vector2(0, 3.5f), fBossMoveSpeed * 1.5f);

        yield return new WaitForSeconds(0.75f);
        GetComponent<Animator>().SetBool("Shiled", false);
        bIsRunningPattern = false;
    }

    IEnumerator DividingShot()
    {
        Debug.Log("Dividing!");

        CallBossMoveToVec2(new Vector2(0, 3.5f), fBossMoveSpeed);

        yield return new WaitForSeconds(1f);

        StartCoroutine(CallBossCreateBullet(((int)BossBulletType.DividingBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 3f, 0f, 0f));

        yield return new WaitForSeconds(1f);

        bIsRunningPattern = false;
    }

    IEnumerator BigShot()
    {
        CallBossMoveToVec2(new Vector2(0, 3.5f), fBossMoveSpeed);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        { 
            if (i == 0 || i == 2)
            {
                RhombusAnimator.SetTrigger("Turning");

                StartCoroutine(CallBossCreateBullet(((int)BossBulletType.BigBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(-2.5f, 2.5f), 3f, 0f, 0f));
            }

            if(i == 1)
            {
                RhombusAnimator.SetTrigger("ReverseTurning");

                StartCoroutine(CallBossCreateBullet(((int)BossBulletType.BigBullet), new Vector2(transform.position.x, transform.position.y), new Vector2(2.5f, 2.5f), 3f, 0f, 0f));
            }

            yield return new WaitForSeconds(1.1f);
        }

        bIsRunningPattern = false;
    }
}
