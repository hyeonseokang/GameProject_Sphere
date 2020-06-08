using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Inputmanager : MonoBehaviour {
    public KHS_PlayerControl PC;//PlayerControl 플레이어 컨트롤
    public KHS_PlayerCollider PColider;
    private Vector2 _Screen;
    private Vector2 prevPositoin;

    private bool isShakeing = false;
    public Vector3 originalPosition;
    private Transform tr;
    private void Start()
    {
        tr = GameObject.Find("Main Camera").GetComponent<Transform>();
        _Screen.x = Screen.width;
        _Screen.y = Screen.height;
    }
    IEnumerator EndCmarea()
    {
        yield return new WaitForSeconds(1.0f);
        isShakeing = false;
        tr.transform.position = new Vector3(0, 0, -10);
    }
    void Update () {
        if (isShakeing)
        {
            tr.transform.position = originalPosition + Random.insideUnitSphere * 0.15f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
            PC.playerMoving(DIRECTION.UP);//위로 움직이기
        if(Input.GetKey(KeyCode.DownArrow))
            PC.playerMoving(DIRECTION.DOWN);//아래로 움직이기
        if (Input.GetKey(KeyCode.LeftArrow))
            PC.playerMoving(DIRECTION.LEFT);//왼쪽으로 움직이기
        if (Input.GetKey(KeyCode.RightArrow))
            PC.playerMoving(DIRECTION.RIGHT);//오른쪽으로 움직이기

        if (Input.GetKeyDown(KeyCode.Z))
            PC.launchBullet();
        if (Input.GetKeyDown(KeyCode.X))
        {
            PC.StartSkill(1);
            //isShakeing = true;
          //  StartCoroutine("EndCmarea");
        }
        if (Input.GetKeyDown(KeyCode.C))
            PC.StartSkill(2);
        if (Input.GetKeyDown(KeyCode.V))
            PC.SkillHpheal();


      
        if (Input.touchCount>0)
        {//터치 모바일환경에서 움직이기 
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                prevPositoin = touch.position;
            if (touch.phase==TouchPhase.Moved||Input.GetTouch(1).phase==TouchPhase.Moved)
            {
                if (!(EventSystem.current.IsPointerOverGameObject(0)) || PC.inputTouchHit == true)
                {
                    PC.playerMovePosition(touch.position - prevPositoin);
                    prevPositoin = touch.position;
                }            
            }
            if(Input.GetTouch(1).phase==TouchPhase.Began)
            {
                if (!(EventSystem.current.IsPointerOverGameObject(1)))
                    PC.launchBullet();
            }
        }
       
    }
}
