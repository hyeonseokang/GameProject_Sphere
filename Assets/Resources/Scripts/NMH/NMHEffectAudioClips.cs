using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHEffectAudioClips : MonoBehaviour
{
    public AudioClip[] ButtonSound;
    public AudioClip[] SkillSound;
    public AudioClip[] PlayerControlSound;
    public AudioClip[] BulletSound;

    public enum ButtonClip
    {
        CLICK
    }

    public enum SkillClip
    {
        LAZER,
        SHIELD,
        SHOT,
        HEAL
    }

    public enum PlayerControlClip
    {
        SHOT,
        DEAD
    }

    public enum BulletClip
    {
        BREAK,
        ELECTRONIC
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
