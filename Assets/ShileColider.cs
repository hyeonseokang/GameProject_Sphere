using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShileColider : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ebullet")||collision.gameObject.CompareTag("PlayerEbullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
