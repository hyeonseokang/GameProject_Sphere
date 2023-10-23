using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour {

	void Start () {
        StartCoroutine(DestroyParticle());
	}
	

    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(this.gameObject);
    }

    
}


