using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OHSBackGroundMove : MonoBehaviour {
    public float scoll;
    public Material Scrol;
   
    Vector2 Scro = Vector2.zero;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Scro = Scrol.mainTextureOffset;
        Scro.x += (scoll * Time.deltaTime);
        Scrol.mainTextureOffset = Scro;
    }
}
