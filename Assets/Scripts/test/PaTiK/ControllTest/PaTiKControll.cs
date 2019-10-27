using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class PaTiKControll : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    private PaTiKControll instance = null;


    public bool moveit = false; // 이동 가능 여부
    public bool rightButtonOn;
    public bool leftButtonOn;
    public bool upperButtonOn;

    public BoxCollider2D boundBox;  // 맵 바운더리 지정
    public BoxCollider2D characterBox;// 캐릭터 바운더리 지정

    private Vector3 targetpos; // 마우스 좌표
    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float rightButtonSec;
    private float leftButtonSec;
    public float speed; // 캐릭터 이동 속도
    private float screenHeight;
    private float screenWidth;

    //---------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //---------------------------------------------------------------------------------------------
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;

        halfWidth = (characterBox.size.x) / 2f;

        screenHeight = Screen.height; // 스크린 높이
        screenWidth = Screen.width;   // 스크린 넓이
        //---------------------------------------------------------------------------------------------


        // 텔레포트할 시 도플갱어 삭제
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


        rightButtonSec = (screenWidth / 4) * 3;
        leftButtonSec = screenWidth / 4;

        Debug.Log(leftButtonSec);


    }

    // Update is called once per frame
    void Update()
    {
        //캐릭터의 이동 가능 x축 y축 제한
        float clampedX = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y); 

        transform.position = new Vector3(clampedX, clampedY, 0);    
        //---------------------------------------------------------------------------------------------

        if (EventSystem.current.IsPointerOverGameObject() == true) return; // UI창 나오면 클릭 금지

        //---------------------------------------------------------------------------------------------

        if (Input.GetMouseButton(0))
        {
            targetpos = Input.mousePosition; // 클릭시 스크린에서의 마우스 포지션

            float posX = targetpos.x; // 마우스 X좌표
            float posY = targetpos.y; // 마우스 Y좌표

            if (posX > rightButtonSec)
            {
                moveit = true;
                rightButtonOn = true;
                leftButtonOn = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
                GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed);
            }
            else if (posX < leftButtonSec) // 플레이어 터치 가능위치 제한, 캐릭터 이동위치 제한
            {
                moveit = true;
                rightButtonOn = false;
                leftButtonOn = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
                GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed);
            }
            
            if(moveit && posY > screenHeight / 2)
            {
                upperButtonOn = true;
            }

        }
        else
        {
            moveit = false;

            rightButtonOn = false;
            leftButtonOn = false;
            upperButtonOn = false;
        }
        //---------------------------------------------------------------------------------------------
    }
}