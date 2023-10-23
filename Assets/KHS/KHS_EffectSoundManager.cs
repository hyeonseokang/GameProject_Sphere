using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_EffectSoundManager : MonoBehaviour {
    public static KHS_EffectSoundManager instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource ButtonClickSound;
    public void ButtonClickOn()
    {
        ButtonClickSound.Play();
    }
    public AudioSource BulletBreakSound;
    public void BulletBreakOn()
    {
        // BulletBreakSound.Play();
        AudioSource a = BulletBreakSound;
        a.Play();
    }
    public AudioSource SkillLazerSound;
    public void SkillLazerOn()
    {
        SkillLazerSound.Play();
    }
    public AudioSource SkillShiledSound;
    public void SkillShiledOn()
    {
        SkillShiledSound.Play();
    }
    public AudioSource PlayerShotSound;
    public void PlayerShotOn()
    {
        PlayerShotSound.Play();
    }
}
