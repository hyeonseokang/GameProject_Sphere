using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHEffectSoundManager : MonoBehaviour
{
    public GameObject EffectAudioClipPrefab;

    public static NMHEffectSoundManager instance;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    int nMaxPrefab = 10; //이 크기 만큼 오브젝트를 만들어서 풀링함

    GameObject SoundManagerParent;
    GameObject EffectParent;

    GameObject[] EffectAudioClipObjArr;
    public bool effectonoff = true;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (PlayerPrefs.GetInt("EFFECT") == 0)
            effectonoff = false;
    }

    private void Start()
    {
        InitializeObjs();
        InitializeEffectArr();
    }

    private void InitializeObjs()
    {
        SoundManagerParent = GameObject.Find("OHS_SoundManager");
    }

    private void InitializeEffectArr()
    {
        EffectAudioClipObjArr = new GameObject[nMaxPrefab];

        for (int i = 0; i < nMaxPrefab; i++)
        {
            GameObject EffectClone;

            EffectClone = Instantiate(EffectAudioClipPrefab, SoundManagerParent.transform);
            EffectClone.name = "EffectAudioClip" + i;

            EffectAudioClipObjArr[i] = EffectClone;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //주의사항과 사용 예
    //밑 함수들은 모두 RunEffectAudioClip에 오버로딩 되어있음 -> 함수 하나로 인자만 바꿔서 씀
    //사용법이 완전 달라져서 전에꺼랑 호환이 안됨... 사운드 들어가는 부분 수정 바람
    //NMHEffectAudioClips.~~Clip -> 종류에 따른 이펙트 사운드 리스트
    //RunEffectAudioClip(NMHEffectAudioClips.BulletClip.BREAK);
    //RunEffectAudioClip(NMHEffectAudioClips.ButtonClip.~~~~); 등등
    //이후 사운드 추가할때는 NMHEffectAudioClips 에서 enum 새로 만들던가 있는 enum에 추가하던가 하면됨 
    //enum 새로 만들경우 나한테 밑에 있는 함수처럼 오버로딩된거 하나 더 만들면 될듯 내용은 거의 비슷함 단어하나만 고치면됨

    public void RunEffectAudioClip(NMHEffectAudioClips.BulletClip _nList)
    {
        if (effectonoff)
        {
            for (int i = 0; i < nMaxPrefab; i++)
            {
                AudioSource EffectAudioSrc = EffectAudioClipObjArr[i].GetComponent<AudioSource>();

                if (EffectAudioSrc.isPlaying == false)
                {
                    EffectAudioSrc.clip = EffectAudioClipObjArr[i].GetComponent<NMHEffectAudioClips>().BulletSound[(int)_nList];
                    EffectAudioSrc.Play();

                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void RunEffectAudioClip(NMHEffectAudioClips.ButtonClip _nList)
    {
        if (effectonoff)
        {
            for (int i = 0; i < nMaxPrefab; i++)
            {
                AudioSource EffectAudioSrc = EffectAudioClipObjArr[i].GetComponent<AudioSource>();

                if (EffectAudioSrc.isPlaying == false)
                {
                    EffectAudioSrc.clip = EffectAudioClipObjArr[i].GetComponent<NMHEffectAudioClips>().ButtonSound[(int)_nList];
                    EffectAudioSrc.Play();

                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void RunEffectAudioClip(NMHEffectAudioClips.PlayerControlClip _nList)
    {
        if (effectonoff)
        {
            for (int i = 0; i < nMaxPrefab; i++)
            {
                AudioSource EffectAudioSrc = EffectAudioClipObjArr[i].GetComponent<AudioSource>();

                if (EffectAudioSrc.isPlaying == false)
                {
                    EffectAudioSrc.clip = EffectAudioClipObjArr[i].GetComponent<NMHEffectAudioClips>().PlayerControlSound[(int)_nList];
                    EffectAudioSrc.Play();

                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void RunEffectAudioClip(NMHEffectAudioClips.SkillClip _nList)
    {
        if (effectonoff)
        {
            for (int i = 0; i < nMaxPrefab; i++)
            {
                AudioSource EffectAudioSrc = EffectAudioClipObjArr[i].GetComponent<AudioSource>();

                if (EffectAudioSrc.isPlaying == false)
                {
                    EffectAudioSrc.clip = EffectAudioClipObjArr[i].GetComponent<NMHEffectAudioClips>().SkillSound[(int)_nList];
                    EffectAudioSrc.Play();

                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
