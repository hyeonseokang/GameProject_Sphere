using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_MeteoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("RemoveMeteo", 0.5f);
	}
	private void RemoveMeteo()
    {
        Destroy(gameObject);
    }
}
