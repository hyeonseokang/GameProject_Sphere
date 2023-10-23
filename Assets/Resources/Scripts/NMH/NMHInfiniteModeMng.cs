using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHInfiniteModeMng : MonoBehaviour
{
    public static NMHInfiniteModeMng instance = null;

    int nBeforeStageType = -1;

    int nCurStageType = 0;
    int nCurStageLevel = 0;
    int nCurSpawnLevel = 4;
    int nCurLeftJunior = 0;

    int nMaxJunior = 6;
    int nCurUnSpawnedJunior = 0;

    int nBossCnt = 0;

    bool bIsBossAlive = false;

    ////////////////////////////////////////////////////////////////

    public GameObject[] TriangleJuniorObjArr;
    public GameObject[] SquareJuniorObjArr;
    public GameObject[] RhombusJuniorObjArr;

    public GameObject TriangleBoss;
    public GameObject SquareBoss;
    public GameObject RhombusBoss;

    public GameObject BossHPBar;

    public enum StageType
    {
        TRIANGLE,
        SQUARE,
        RHOMBUS
    }

    GameObject BossParent;
    GameObject JuniorParent;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////

    public int GetCurStageType()
    {
        return nCurStageType;
    }

    public int GetCurStageLevel()
    {
        return nCurStageLevel;
    }

    public int GetCurSpawnLevel()
    {
        return nCurSpawnLevel;
    }

    public int GetCurLeftJunior()
    {
        return nCurLeftJunior;
    }

    public void SetCurStageType(int _nType)
    {
        nCurStageType = _nType;
    }

    public void SetCurStageLevel(int _nLevel)
    {
        nCurStageLevel = _nLevel;
    }

    public void SetCurSpawnLevel(int _nLevel)
    {
        nCurSpawnLevel = _nLevel;
    }

    public void SetCurLeftJunior(int _nLeft)
    {
        nCurLeftJunior = _nLeft;

        if(nCurLeftJunior == 0)
        {
            SpawnBoss(nCurStageType);
        }
    }

    ////////////////////////////////////////////////////////////////

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitializeObjs();
        StartInfiniteMode();
    }

    void Update ()
    {
        CheckBossDeadOrAlive();
        CheckIfSpawnMoreJunior();
    }

    void InitializeObjs()
    {
        BossParent = GameObject.Find("Boss");
        JuniorParent = GameObject.Find("Juniors");

        BossHPBar.SetActive(false);
    }

    void StartInfiniteMode()
    {
        StartStageLevel(0);
    }

    void StartStageLevel(int _nStageLevel)
    {
        SetCurStageLevel(_nStageLevel);

        while (true)
        {
            nCurStageType = Random.Range(0, 3);

            if(nCurStageType != nBeforeStageType)
            {
                break;
            }
        }

        for (int i = 0; i < nCurSpawnLevel && i < nMaxJunior; i++)
        {
            switch (nCurStageType)
            {
                case (int)StageType.TRIANGLE:
                    SpawnJunior((int)StageType.TRIANGLE);
                    break;
                case (int)StageType.SQUARE:
                    SpawnJunior((int)StageType.SQUARE);
                    break;
                case (int)StageType.RHOMBUS:
                    SpawnJunior((int)StageType.RHOMBUS);
                    break;
            }
        }

        nCurUnSpawnedJunior = nCurSpawnLevel - GetJuniorCnt() > 0 ? nCurSpawnLevel - GetJuniorCnt() : 0;
    }

    void SpawnJunior(int _nStageType)
    {
        nCurLeftJunior++;

        switch (_nStageType)
        {
            case (int)StageType.TRIANGLE:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject TriangleJuniorCloneObj = Instantiate(RhombusJuniorObjArr[0], JuniorParent.transform);
                break;
            case (int)StageType.SQUARE:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject SquareJuniorCloneObj = Instantiate(RhombusJuniorObjArr[0], JuniorParent.transform);
                break;
            case (int)StageType.RHOMBUS:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject RhombusJuniorCloneObj = Instantiate(RhombusJuniorObjArr[0], JuniorParent.transform);
                break;
        }
    }

    void SpawnBoss(int _nStageType)
    {
        switch (_nStageType)
        {
            case (int)StageType.TRIANGLE:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject TriangleBossCloneObj = Instantiate(RhombusBoss, BossParent.transform);
                break;
            case (int)StageType.SQUARE:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject SquareBossCloneObj = Instantiate(RhombusBoss, BossParent.transform);
                break;
            case (int)StageType.RHOMBUS:
                KHS_GamaManager.instance.BossNumber = (int)StageType.RHOMBUS;
                GameObject RhombusBossCloneObj = Instantiate(RhombusBoss, BossParent.transform);
                break;
        }

        BossHPBar.SetActive(true);
        BossHPBar.GetComponent<NMHBossHPBar>().SetHPBar(2);

        StartCoroutine(BossHPBar.GetComponent<NMHBossHPBar>().SetHPBarFullAtFirst());
    }

    void CheckBossDeadOrAlive()
    {
        nBossCnt = GetBossCnt();

        if (nBossCnt == 0 && bIsBossAlive == true)
        {
            bIsBossAlive = false;

            StartCoroutine(StageLevelUp());
        }
        else if(nBossCnt == 1 && bIsBossAlive == false)
        {
            bIsBossAlive = true;
        }
    }

    void CheckIfSpawnMoreJunior()
    {
        if(nCurUnSpawnedJunior > 0 && GetJuniorCnt() < nMaxJunior)
        {
            SpawnJunior(nCurStageType);

            nCurUnSpawnedJunior--;
        }
    }

    int GetJuniorCnt()
    {
        int nTmpJuniorCnt = 0;

        foreach (Transform child in JuniorParent.transform)
        {
            nTmpJuniorCnt++;
        }

        return nTmpJuniorCnt;
    }

    int GetBossCnt()
    {
        int nTmpCnt = 0;

        foreach (Transform child in BossParent.transform)
        {
            nTmpCnt++;
        }

        return nTmpCnt;
    }

    IEnumerator StageLevelUp()
    {
        BossHPBar.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        nBeforeStageType = nCurStageType;

        nCurStageLevel++;
        nCurSpawnLevel++;

        StartStageLevel(nCurStageLevel);
    }
}
