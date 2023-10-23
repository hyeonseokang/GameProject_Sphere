using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSprite : MonoBehaviour
{
    public void Fade(float _fTime, float _fOpacity)
    {
        StartCoroutine(FadeCorutine(_fTime, _fOpacity));
    }

    public void FadeIn(float _fTime, float _fOpacity)
    {
        StartCoroutine(FadeCorutine(_fTime, _fOpacity));
    }
    
    public void FadeOut(float _fTime, float _fOpacity)
    {
        StartCoroutine(FadeCorutine(_fTime, _fOpacity));
    }

    IEnumerator FadeCorutine(float _fTIme, float _fOpacity)
    {
        SpriteRenderer ObjSprR = GetComponent<SpriteRenderer>();

        if(_fOpacity > 1)
        {
            _fOpacity = 1;
        }

        if(_fOpacity < 0)
        {
            _fOpacity = 0;
        }

        float fObjAlpha = ObjSprR.color.a;
        float fTargetAlpha = _fOpacity;

        float fChangePerFrame = (fObjAlpha - fTargetAlpha) / (_fTIme * 60.0f); 

        float fFadingTime = 0;

        while(fFadingTime < _fTIme)
        {
            fObjAlpha -= fChangePerFrame;

            ObjSprR.color = new Color(1, 1, 1, fObjAlpha);

            fFadingTime += 1.0f / 60f;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0f);
    }
}
