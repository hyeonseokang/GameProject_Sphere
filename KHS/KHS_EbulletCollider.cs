using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_EbulletCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pbullet"))
        {
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.BREAK);
            KHS_Objectmanager.instance.Gold++;
            KHS_ScoreManager.instance.Score += 5;
            Instantiate(KHS_Objectmanager.instance.HitEffect,gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Lazer"))
        {
            NMHEffectSoundManager.instance.RunEffectAudioClip(NMHEffectAudioClips.BulletClip.BREAK);
            KHS_Objectmanager.instance.Gold++;
            KHS_ScoreManager.instance.Score += 5;
            Instantiate(KHS_Objectmanager.instance.HitEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
