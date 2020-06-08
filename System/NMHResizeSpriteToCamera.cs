using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHResizeSpriteToCamera : MonoBehaviour
{
	void Awake ()
    {
        ResizeSprite();
    }

    void ResizeSprite()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        Camera.main.orthographicSize = 1280 / (2 * 100f);

        Screen.SetResolution(576, 1024, false);
    }
}
