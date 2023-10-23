using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHInvisibleWalls : MonoBehaviour
{
	void Start ()
    {
        
    }
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("sdfasdf");
        Destroy(collision.gameObject);
    }
}
