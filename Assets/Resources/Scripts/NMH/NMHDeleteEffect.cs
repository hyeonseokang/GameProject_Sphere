using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHDeleteEffect : MonoBehaviour
{
    void DeleteEffect()
    {
        Destroy(GetComponentInParent<Transform>().gameObject);
        Destroy(this.gameObject);
    }
}
