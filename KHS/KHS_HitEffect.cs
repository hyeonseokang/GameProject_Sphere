using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_HitEffect : MonoBehaviour {
    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine("RemoveObject");
    }

}
