using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_BossCollider : MonoBehaviour {
    private int _hp;//보스 체력
    public ParticleSystem Bullet_Effect;
    public int Angle;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            KHS_ScoreManager.instance.Score += 10;
            if (_hp <= 0)  
            { 
                if (clearBool)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Instantiate(Bullet_Effect, transform.position, Quaternion.Euler(0, 0, Angle));
                        Angle += 18;
                    }
                    StartCoroutine("ClearIE");
                    clearBool = false;
                }
            }
        }
    }
    bool clearBool = true;
    IEnumerator ClearIE()
    {
        if (GameObject.Find("BossHPBar") == null) //changed------------------------------------------------------
        {
            Destroy(KHS_Objectmanager.instance.Player);
            Destroy(KHS_Objectmanager.instance.GetComponent<Inputmanager>());
            Destroy(GameObject.Find("BossBullet"));
            Text[] _text;
            Image[] _image;
            _text = KHS_Objectmanager.instance.ResultWindow.GetComponentsInChildren<Text>();
            _image = KHS_Objectmanager.instance.ResultWindow.GetComponentsInChildren<Image>();
            KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1, 0);
            for (int i = 0; i < _text.Length; i++)
            {
                _text[i].color = new Color(1, 1, 1, 0);
                _text[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < _image.Length; i++)
            {
                _image[i].color = new Color(1, 1, 1, 0);
                _image[i].gameObject.SetActive(false);
            }
            PlayerPrefs.SetInt("CLEAR" + (KHS_GamaManager.instance.BossNumber + 1), PlayerPrefs.GetInt("CLEAR" + (KHS_GamaManager.instance.BossNumber + 1)) + 1);
            GetComponent<Animator>().SetTrigger("dead");
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            yield return new WaitForSeconds(1.2f);
           

            KHS_Objectmanager.instance.Fail.SetActive(false);
        
            KHS_Objectmanager.instance.GiftButton.gameObject.SetActive(true);
           
            KHS_Objectmanager.instance.ResultWindow.gameObject.SetActive(true);
            KHS_Objectmanager.instance.ResultWindow.GetComponent<KHS_ResultWindowScript>().GiftObjectOn();
            while (true)
            {
                KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1, 
                    KHS_Objectmanager.instance.ResultWindow.color.a+0.01f);
                for (int i = 0; i < _text.Length; i++)
                {
                    _text[i].color = new Color(1, 1, 1, _text[i].color.a + 0.02f);
                }
                for (int i = 0; i < _image.Length; i++)
                {
                    _image[i].color = new Color(1, 1, 1, _image[i].color.a + 0.02f);
                }
                yield return new WaitForSeconds(0.01f);
                if (KHS_Objectmanager.instance.ResultWindow.color.a >= 1.0f)
                {
                    KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1,1);
                    for (int i = 0; i < _text.Length; i++)
                    {
                        _text[i].color = new Color(1, 1, 1, 1);
                    }
                    for (int i = 0; i < _image.Length; i++)
                    {
                        _image[i].color = new Color(1, 1, 1, 1);
                    }
                    break;
                }
            }

            KHS_Objectmanager.instance.ResultWindow.GetComponent<Animator>().SetTrigger("Clear");
            Destroy(gameObject);
            KHS_ScoreManager.instance.Score += 200;
            int timenum = GameObject.Find("Manager").GetComponent<KHS_Timer>().getTime();
            if (timenum <= 60)
            {
                KHS_ScoreManager.instance.Score += timenum * 10;
            }
            KHS_ScoreManager.instance.Score += PControl.HP * 200;
            KHS_Objectmanager.instance.ResultWindow.GetComponent<KHS_ResultWindowScript>().setResult(
                       timenum,
                       KHS_Objectmanager.instance.Gold, KHS_ScoreManager.instance.Score);
      
            //KHS_Objectmanager.instance.ResultWindow.GetComponent<Animator>().Play("ClearWindowAnimation");
            yield return new WaitForSeconds(2.0f);
            Time.timeScale = 0;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("dead");
            yield return new WaitForSeconds(0.87f);
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        PControl = GameObject.Find("Player").GetComponent<KHS_PlayerControl>();
        BossHpimage = GameObject.Find("BossHp2");
        HP = 100;
        if (GameObject.Find("BossHPBar") == null)
            BossHpRenderer = BossHpimage.GetComponent<SpriteRenderer>();
       
    }
    private void BossHit()
    {
        HP--;
        if (KHS_GamaManager.instance.BossNumber == 1)
        {
            BossHpRenderer.size = new Vector2(BossHpRenderer.size.x, BossHpRenderer.size.y - (4.449821f / 100.0f));
            BossHpimage.transform.position = new Vector2(BossHpimage.transform.position.x, BossHpimage.transform.position.y - (4.449821f / 200.0f));
        }
        else
        {
            BossHpRenderer.size = new Vector2(BossHpRenderer.size.x, BossHpRenderer.size.y - (5.197669f / 100.0f));
            BossHpimage.transform.position = new Vector2(BossHpimage.transform.position.x, BossHpimage.transform.position.y - (5.197669f / 200.0f));
        }
    }
    private KHS_PlayerControl PControl;
    private GameObject BossHpimage;
    private SpriteRenderer BossHpRenderer;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Lazer"))
        {
            StopCoroutine("LazerHit");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pbullet"))
        {
            Instantiate(Bullet_Effect, transform.position, Quaternion.Euler(0, 0, Angle));
            KHS_Objectmanager.instance.Gold++;
            Destroy(collision.gameObject);
            PControl.SkillGaugeadd();
            Instantiate(KHS_Objectmanager.instance.HitEffect, collision.gameObject.transform.position, Quaternion.identity);
            if (GameObject.Find("BossHPBar") == null)
                BossHit();
        }
        if(collision.gameObject.CompareTag("Lazer"))
        {
            StartCoroutine("LazerHit");
        }
    }

    IEnumerator LazerHit()
    {
        while(true)
        {
            BossHit();
            KHS_Objectmanager.instance.Gold++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
