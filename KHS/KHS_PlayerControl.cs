using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}
public class KHS_PlayerControl : MonoBehaviour
{
    bool failbool = true;
    private int _hp;//플레이어 체력
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp <= 0)
            { //게임 오버 
                StartCoroutine("FailCroutine");
            }
        }
    }

    IEnumerator FailCroutine()
    {
        if (failbool)
        {
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.PlayerControlClip.DEAD);
            Instantiate(KHS_Objectmanager.instance.PlayerDeadEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            HP = 5000;
            yield return new WaitForSeconds(2.0f);
            failbool = false;
            Text[] _text;
            Image[] _image;
            _text = KHS_Objectmanager.instance.ResultWindow.GetComponentsInChildren<Text>();
            _image = KHS_Objectmanager.instance.ResultWindow.GetComponentsInChildren<Image>();
            KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1, 0);
            for (int i = 0; i < _text.Length; i++)
            {
                _text[i].color = new Color(1, 1, 1, 0);
            }
            for (int i = 0; i < _image.Length; i++)
            {
                _image[i].color = new Color(1, 1, 1, 0);
            }
            KHS_Objectmanager.instance.Clear.SetActive(false);
            KHS_Objectmanager.instance.GiftItemText.gameObject.SetActive(false);
            KHS_Objectmanager.instance.Box.SetActive(false);
            KHS_Objectmanager.instance.Box2.SetActive(false);
            KHS_Objectmanager.instance.ResultWindow.gameObject.SetActive(true);

            KHS_Objectmanager.instance.ResultWindow.gameObject.SetActive(true);
            while (true)
            {
                KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1,
                    KHS_Objectmanager.instance.ResultWindow.color.a + 0.01f);
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
                    KHS_Objectmanager.instance.ResultWindow.color = new Color(1, 1, 1, 1);
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

            Destroy(KHS_Objectmanager.instance.ResultWindow.GetComponent<Animator>());
            KHS_Objectmanager.instance.ResultWindow.GetComponent<KHS_ResultWindowScript>().setResult(
              GameObject.Find("Manager").GetComponent<KHS_Timer>().getTime(),
              KHS_Objectmanager.instance.Gold,
              KHS_ScoreManager.instance.Score);

            KHS_Objectmanager.instance.Fail.GetComponent<Animator>().SetBool("Fail", true);
            //Time.timeScale = 0;
        }
    }
    public Image[] SkillIcon;
    private void Awake()
    {
        HP = 3;
        SkillType[0] = PlayerPrefs.GetInt("SKILL1");
        SkillType[1] = PlayerPrefs.GetInt("SKILL2");
        SkillIcon[0].sprite = KHS_Objectmanager.instance.Icon[SkillType[0]-1];
        SkillIcon[1].sprite = KHS_Objectmanager.instance.Icon[SkillType[1]-1];
    }
    private void Start()
    {
        tr = GameObject.Find("Main Camera").GetComponent<Transform>();
        Debug.Log(KHS_GamaManager.instance.BossNumber);
    }
    private void Update()
    {
        if (isShakeing)
        {
            tr.transform.position = originalPosition + Random.insideUnitSphere * 0.15f;
        }
    }
    public float MoveSpeed;//플레이어 움직임 스피드

    public void playerMoving(DIRECTION _direction)
    {
        if (Limitbool)
        {
            switch (_direction)
            {
                case DIRECTION.UP:
                    if (gameObject.transform.position.y < 4.92f) ;
                    gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
                    break;
                case DIRECTION.DOWN:
                    if (gameObject.transform.position.y > -5.79f)
                        gameObject.transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
                    break;
                case DIRECTION.LEFT:
                    if (gameObject.transform.position.x > -3)
                        gameObject.transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
                    break;
                case DIRECTION.RIGHT:
                    if(gameObject.transform.position.x < 2.99f)
                    gameObject.transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
                    break;
            }
        }
    }
    public void playerMovePosition(Vector2 _pos)
    {
      //플레이어 움직이기 inputmanager로부터 좌표 받아서 움직이기 
      gameObject.transform.Translate(_pos * Input.GetTouch(0).deltaTime);
      // gameObject.transform.position = new Vector2(gameObject.transform.position.x + _pos.x / Input.GetTouch(0).deltaTime, gameObject.transform.position.y + _pos.y / Input.GetTouch(0).deltaTime);
      //gameObject.transform.position = (Vector2)gameObject.transform.position + _pos;
      //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + _pos;
    }
    public float bulletCoolTimenum;
    private bool bulletCooldown = false;
    private void startBulletColl()
    {
        bulletCooldown = false;
    }//쿨다운 다시도는거
    public void launchBullet()//플레이어 총알 쏘기
    {
        if (!bulletCooldown)
        {
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.PlayerControlClip.SHOT);
            GetComponent<Animator>().SetTrigger("Shot");
            bulletCooldown = true;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().SkillOn();
            Invoke("startBulletColl", bulletCoolTimenum);
        }
    }
    public Image SkillGauge;
    private int Skillbool = 35; //스킬을 쓸수있는지 
    public Image SkillGauge2;
    public void SkillGaugeadd()
    {
        if (Skillbool != 35&& skillbool2)
        {
            //SkillGauge.rectTransform.sizeDelta = new Vector2(16f, SkillGauge.rectTransform.sizeDelta.y + 13.85f);
            // SkillGauge.rectTransform.position = new Vector2(SkillGauge.rectTransform.position.x, SkillGauge.rectTransform.position.y + 6.925f);
            SkillGauge.rectTransform.sizeDelta = new Vector2(88, SkillGauge.rectTransform.sizeDelta.y + (246.0f / 20.0f));
            SkillGauge2.rectTransform.localPosition = new Vector2(-9.6f, SkillGauge2.rectTransform.localPosition.y + (126.0f / 20.0f));
            Skillbool++;
            if (Skillbool == 20)
            {
                Skillbool = 35;
                SkillGauageOn();
                SkillGauge2.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator SkillGaugeminus()
    {
        skillbool2 = false;
        SkillGauge2.gameObject.SetActive(true);
        while (true)
        {
            //SkillGauge.rectTransform.sizeDelta = new Vector2(16f, SkillGauge.rectTransform.sizeDelta.y - 8.0f);
            //SkillGauge.rectTransform.position = new Vector2(SkillGauge.rectTransform.position.x, SkillGauge.rectTransform.position.y - 4.0f);
            SkillGauge.rectTransform.sizeDelta = new Vector2(88, SkillGauge.rectTransform.sizeDelta.y - (246.0f / 35.0f));
            SkillGauge2.rectTransform.localPosition = new Vector2(-9.6f, SkillGauge2.rectTransform.localPosition.y - (126.0f / 35.0f));
            yield return new WaitForSeconds(0.0f);
            Skillbool--;
            if (Skillbool == 0)
            {
                skillbool2 = true;
                break;
            }
        }
    }

    public GameObject[] SkillButton;
    public GameObject SkillGaugeAnimation;
    bool skillbool2 = true;
    void SkillGauageOn()
    {
        for(int i=0;i<2;i++)
        {
            SkillButton[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            SkillIcon[i].color = new Color(1, 1, 1, 1);
        }
        SkillGaugeAnimation.SetActive(true);
    }
    void SkillGauageOff()
    {
        for (int i = 0; i < 2; i++)
        {
            SkillButton[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            SkillIcon[i].color = new Color(1, 1, 1, 0.5f);
        }
        SkillGaugeAnimation.SetActive(false);
    }
    private int[] SkillType = new int[2];
    public void StartSkill(int _Type)
    {
        switch(SkillType[_Type-1])
        {
            case 1:
                NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.SkillClip.LAZER);
                launchLazerBullet();
                SkillGauageOff();
                break;
            case 2:
                NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.SkillClip.SHIELD);
                ShiledOn();
                SkillGauageOff();
                break;
            case 3:
                NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.SkillClip.HEAL);
                SkillHpheal();
                SkillGauageOff();
                break;
            case 4:
                NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.SkillClip.SHOT);
                launchSkillBullet();
                SkillGauageOff();
                break;
        }
    }
    public void launchSkillBullet()
    {
        if (!bulletCooldown&& Skillbool==35)
        {//스킬 샷건 !!파팟
            GetComponent<Animator>().SetTrigger("Santan");
            bulletCooldown = true;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().Angle = 0;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().Angle = 3;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().Angle = 6;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().Angle = -3;
            Instantiate(KHS_Objectmanager.instance.PB, gameObject.transform.position, Quaternion.identity).GetComponent<Playerbullet>().Angle = -6;
            Invoke("startBulletColl", bulletCoolTimenum);
            StartCoroutine("SkillGaugeminus");
        }
    } //스킬쓰기 
    private Transform tr;
    private bool isShakeing = false;
    public Vector3 originalPosition;
    IEnumerator EndCmarea()
    {
        yield return new WaitForSeconds(1.0f);
        isShakeing = false;
        tr.transform.position = new Vector3(0, 0, -10);
    }
    public void launchLazerBullet()
    {
        if (Skillbool == 35)
        {
            isShakeing = true;
            StartCoroutine("EndCmarea");
            StartCoroutine("launchLazer");
            StartCoroutine("SkillGaugeminus");
        }
    }// 레이저 스킬
    public GameObject Lazer;
    IEnumerator launchLazer()
    {
        yield return new WaitForSeconds(0.5f);
        Lazer.SetActive(true);
        Lazer.GetComponent<Animator>().Play("KHS_LazerAnimation");
        yield return new WaitForSeconds(1.0f);
        Lazer.SetActive(false);
    }
    [HideInInspector] public bool inputTouchHit = false;//히트이미지 떴을때 방지하는 현상 
    public Image HillImage;
    public IEnumerator BlankObject(GameObject _object)
    {
        _object.SetActive(true);
        inputTouchHit = true;
        yield return new WaitForSeconds(0.1f);
        inputTouchHit = false;
        _object.SetActive(false);
    }
    private IEnumerator HillObjectBlank()
    {
        int Gop = 1;
        while (true)
        {
            byte Opacity = 0;
            Opacity += (byte)(1* Gop);
            HillImage.color = new Color32(255, 255, 255, Opacity);
            yield return new WaitForSeconds(0.0f);
            if(Opacity>=255)
            {
                Gop = -1;
            }
            else if(Opacity<=0)
            {
                break;
            }
        }
    }
    public Image HpbarImage;
    public void SkillHpheal()
    {
        HillImage.gameObject.SetActive(true);
        HillImage.GetComponent<Animator>().Play(0);
        //StartCoroutine("HillObjectBlank");
       // StartCoroutine(BlankObject(HillImage.gameObject));
        HP++;
        StartCoroutine("Hpbaradd");
    }
    IEnumerator Hpbaradd()
    {
        int x = 30;
        while (true)
        {
            HpbarImage.rectTransform.sizeDelta = new Vector2(HpbarImage.rectTransform.sizeDelta.x + 8.0f, 28.0f);
            yield return new WaitForSeconds(0.0f);
            x--;
            if (x == 0)
                break;
        }
    }

    [HideInInspector] public bool Shiledbool = true;
    private bool ShiledCount = true;//쉴드 한번밖에못쓰니 조건 걸기 
    public GameObject ShiledObject;
    public void ShiledOn()
    {
        if (Skillbool == 35)
        {
            if (ShiledCount)
                StartCoroutine("ShileOnCorutine");
            StartCoroutine("SkillGaugeminus");
        }
      
    }
    public GameObject PlayerShiled;//쉴드 쓰면 나오는효과 
    private IEnumerator ShileOnCorutine()
    {
        ShiledCount = false;
        PlayerShiled.SetActive(true);
        Shiledbool = false; //쉴드 활성화 
        ShiledObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        Shiledbool = true;
        ShiledObject.SetActive(false);
        PlayerShiled.SetActive(false);
    }
    bool Limitbool = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerEbullet"))
        {
            Destroy(collision.gameObject);
            KHS_ScoreManager.instance.Score += 5;
        }
    }
}
