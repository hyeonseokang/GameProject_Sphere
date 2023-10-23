using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHBossHPBar : MonoBehaviour
{
    int nBossType = 0;

    float fCurGauge = 100;

    ////////////////////////////////////////////////////////////////

    public Sprite[] BackSprArr;
    public Sprite[] GaugeSprArr;

    Sprite BackSpr;
    Sprite GaugeSpr;

    GameObject BackObj;
    SpriteRenderer BackSprR;

    GameObject GaugeObj;
    SpriteRenderer GaugeSprR;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////

    void Start ()
    {
        InitializeObj();
    }

    public float GetCurGauge()
    {
        return fCurGauge;
    }

    public void SetCurGauge(float _fGauge)
    {
        fCurGauge = _fGauge;
    }

    ////////////////////////////////////////////////////////////////

    void InitializeObj()
    {
        BackSpr = GameObject.Find("BossHPBarBack").GetComponent<SpriteRenderer>().sprite;
        GaugeSpr = GameObject.Find("BossHp2").GetComponent<SpriteRenderer>().sprite;

        GaugeObj = GameObject.Find("BossHp2");
        GaugeSprR = GaugeObj.GetComponent<SpriteRenderer>();

        BackObj = GameObject.Find("BossHPBarBack");
        BackSprR = BackObj.GetComponent<SpriteRenderer>();
    }

    public void SetHPBar(int _nType)
    {
        nBossType = _nType;

        BackSpr = BackSprArr[_nType];
        GaugeSpr = GaugeSprArr[_nType];

        BackSprR.sprite = BackSpr;
        GaugeSprR.sprite = GaugeSpr;

        if (_nType == 1)
        {
            GaugeSprR.size = new Vector2(4.408951f, 4.449821f);
            GaugeObj.transform.position = new Vector2(0.03f, 0.03f);
        }
        else
        {
            GaugeSprR.size = new Vector2(5.261393f, 5.197669f);
            GaugeObj.transform.position = new Vector2(0.03f, 0.03f);
        }
    }

    public void SetHPBarGaugeByPercent(float _nPercent)
    {
        if (nBossType == 1)
        {
            GaugeSprR.size = new Vector2(GaugeSprR.size.x, 4.449821f - (4.449821f / 100.0f) * (100 - _nPercent));
            GaugeObj.transform.position = new Vector2(0.03f, 0.03f - (4.449821f / 200.0f) * (100 - _nPercent));
        }
        else
        {
            GaugeSprR.size = new Vector2(GaugeSprR.size.x, 5.197669f - (5.197669f / 100.0f) * (100 - _nPercent));
            GaugeObj.transform.position = new Vector2(0.03f, 0.03f - (5.197669f / 200.0f) * (100 - _nPercent));
        }

        fCurGauge = _nPercent;
    }

    public IEnumerator SetHPBarFullAtFirst()
    {
        for (int i = 1; i <= 100; i++)
        {
            SetHPBarGaugeByPercent(i);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
