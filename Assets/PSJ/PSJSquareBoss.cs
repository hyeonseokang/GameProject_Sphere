using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJSquareBoss : NMHBossBasement
{
    public GameObject ZoneSafe;

    public Vector2 BossPatternVec2 = new Vector2(0, 3.5f);

    enum BossPatternType
    {
        Razer_Shot,
        Line_Shot,
        Moving_Shot
    }

    enum BossPatternPhase2Type
    {
        Super_Razer_Shot,
        Meteo_Shot,
        Random_Razer_Shot
    }

    enum BossBulletType
    {
        Electricity,
        Moving,
        Super_Razer,
        Normal,
        Random,
        Elec2
    }

    new void Start ()
    {
        base.Start();

        InitializeBossObjs();
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

    void InitializeBossObjs()
    {
        StartCoroutine(CallBossAutoPattern(5f, 7f, 3));

        HeroObj = GameObject.Find("Player");
        BossBulletParent = GameObject.Find("BossBullet");

        LeftUpVec2 = new Vector2(-2f, 5f);
        RightDownVec2 = new Vector2(2f, -0.5f);

        fBossDefaultSpeed = 0.25f;
        fBossAutoMoveSpeed = 0.05f;
        fBossMoveSpeed = 0.5f;
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
                        case (int)BossPatternType.Razer_Shot:
                            StartCoroutine(RazerShot());
                            break;

                        case (int)BossPatternType.Line_Shot:
                            StartCoroutine(LineShot());
                            break;
                        case (int)BossPatternType.Moving_Shot:
                            StartCoroutine(MovingShot());
                            break;
                    }
                    
                    break;
                case 2:
                    Debug.Log("2");

                    switch (_nType)
                    {
                        case (int)BossPatternPhase2Type.Super_Razer_Shot:
                            StartCoroutine(SuperRazerShot());
                            break;

                        case (int)BossPatternPhase2Type.Meteo_Shot:
                            StartCoroutine(MeteoShot());
                            break;
                        case (int)BossPatternPhase2Type.Random_Razer_Shot:
                            StartCoroutine(RandomRazerShot());
                            break;
                    }
                    
                    break;
            }
        }
    }

    IEnumerator RazerShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);
        SafeZone();
        StartCoroutine(CallBossCreateBullet((int)BossBulletType.Elec2, new Vector2(transform.position.x, transform.position.y), new Vector2(0, -8), 5f, 0.6f, 0.2f));

        yield return new WaitForSeconds(1.5f);

        bIsRunningPattern = false;
    }

    IEnumerator LineShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);
        for (int i = 0; i < 5; i++)
        {
            float X = Random.Range(-2.5f, 2.5f);
            StartCoroutine(CallBossCreateBullet((int)BossBulletType.Electricity, new Vector2(X, transform.position.y), new Vector2(X, -8), 1f, 0.3f, 0.5f + 1.0f * i));
        }

        yield return new WaitForSeconds(5.2f);

        bIsRunningPattern = false;
    }

    IEnumerator MovingShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);
        StartCoroutine(CallBossCreateBullet((int)BossBulletType.Moving, new Vector2(transform.position.x, transform.position.y), new Vector2(0, -8), 2.5f, 0.6f, 0.2f));

        yield return new WaitForSeconds(1f);

        bIsRunningPattern = false;
    }

    IEnumerator SuperRazerShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);
        for (int i = 0; i < 3; i++)
        {
            float X = Random.Range(-2.5f, 2.5f);
            StartCoroutine(CallBossCreateBullet((int)BossBulletType.Super_Razer, new Vector2(X, transform.position.y), new Vector2(X, -8), 2.5f, 0.3f, 0.5f + 1.0f * i));
        }

        yield return new WaitForSeconds(5.2f);

        bIsRunningPattern = false;
    }
    public GameObject Meteo;
    private IEnumerator sex(float posX,float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(Meteo, new Vector2(posX, 0), Quaternion.identity);
    }
    IEnumerator MeteoShot()
    {
        CallBossMoveToVec2(BossPatternVec2, fBossDefaultSpeed);

        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);
        for (int i = 0; i < 15; i++)
        {
            float X = Random.Range(-2.5f, 2.5f);
            StartCoroutine(CallBossCreateBullet((int)BossBulletType.Normal, new Vector2(X, transform.position.y), new Vector2(X, -8), 2.5f, 0.3f, 0.5f + 0.25f * i));
            StartCoroutine(sex(X,0.5f + 0.25f * i));
        }

        yield return new WaitForSeconds(10f);

        bIsRunningPattern = false;
    }

    IEnumerator RandomRazerShot()
    {
        FadeOut(0.75f, 0f);
        GetComponent<Animator>().SetBool("disapear", true);
        yield return new WaitForSeconds(1f);
        NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.ELECTRONIC);   
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(CallBossCreateBullet((int)BossBulletType.Random, new Vector2(0, 4.6f), new Vector2(0, -8), 2.5f, 0.3f, 0.5f + 1.0f * i));
        }

        yield return new WaitForSeconds(5.2f);

        FadeIn(0.75f, 1f);
        GetComponent<Animator>().SetBool("disapear", false);
        yield return new WaitForSeconds(1f);

        bIsRunningPattern = false;
    }

    IEnumerator LeftRightLazer()
    {
        yield return new WaitForSeconds(0f);


    }

    void SafeZone()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject Zone = Instantiate(ZoneSafe, new Vector2(-1.3f + i * 2.6f, -3), Quaternion.identity);
            StartCoroutine(SafeZoneDelete(Zone));
        }
    }

    IEnumerator SafeZoneDelete(GameObject GetZone)
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(GetZone);
    }


}
