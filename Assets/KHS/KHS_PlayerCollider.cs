using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_PlayerCollider : MonoBehaviour {
    public KHS_PlayerControl PC;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ebullet") || collision.gameObject.CompareTag("PlayerEbullet"))//적 총알
        {
            Instantiate(KHS_Objectmanager.instance.PdownHitEffect, new Vector2(collision.gameObject.transform.position.x, -4.86f), Quaternion.identity);
            StartCoroutine(PC.BlankObject(PlayeHitImage.gameObject));
            PC.HP--;
            Destroy(collision.gameObject);
            StartCoroutine("Hpbarminus");// 줄어드는 애니메이션 활성 
        }
    }
    public Image PlayeHitImage;
    IEnumerator Hpbarminus()
    {
        int x = 30;
        while (true)
        {
            PC.HpbarImage.rectTransform.sizeDelta = new Vector2(PC.HpbarImage.rectTransform.sizeDelta.x - 8.0f, 28.0f);
            yield return new WaitForSeconds(0.0f);
            x--;
            if (x == 0)
                break;
        }
    }
}
