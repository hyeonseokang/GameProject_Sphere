using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHTriangleJuniorA : NMHJunior
{
	new void Start ()
    {
        base.Start();
        InitializeJunior();
    }

	new void Update ()
    {
        base.Update();

        UpdateRunJuniorPattern();
    }

    void InitializeJunior()
    {
        StartCoroutine(CallJuniorAutoPattern(5f, 7f));

        HeroObj = GameObject.Find("Player");
        JuniorBulletParent = GameObject.Find("BossBullet");

        LeftUpVec2 = new Vector2(-2f, 5f);
        RightDownVec2 = new Vector2(2f, -0.5f);

        fJuniorAutoMoveSpeed = 0.05f;

        CallJuniorMoveToVec2(new Vector2(CurJuniorVec2.x, Random.Range(4.5f, 0.5f)), fJuniorAutoMoveSpeed);
    }

    void UpdateRunJuniorPattern()
    {
        if (bCallRunPattern && !bIsRunningPattern)
        {
            bCallRunPattern = false;
            bIsRunningPattern = true;

            StartCoroutine(TriangleJuniorAShot());
        }
    }

    IEnumerator TriangleJuniorAShot()
    {
        StartCoroutine(CallJuniorCreateBullet(new Vector2(CurJuniorVec2.x, CurJuniorVec2.y - 0.1f), new Vector2(CurJuniorVec2.x, CurJuniorVec2.y - 10), 0.5f, 0f, 0f));

        yield return new WaitForSeconds(0.1f);

        bIsRunningPattern = false;
    }
}
