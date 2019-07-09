using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class Controll : MonoBehaviour
{
    static private Controll instance;
    public float speed = 8; // 캐릭터 이동 속도
    public float error = 0.5f; // 캐릭터와 마우스 좌표와의 오차 범위

    private Vector3 targetpos; // 마우스 좌표
    private bool moveit = false; // 이동 가능 여부
    private Vector3 minBound;
    private Vector3 maxBound;

    public BoxCollider2D boundBox;  // 맵 바운더리 지정
    public BoxCollider2D characterBox;// 캐릭터 바운더리 지정

    private float halfWidth;
    private float rightButtonSec;
    private float leftButtonSec;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float screenHeight = Screen.height; // 스크린 높이
        float screenWidth = Screen.width;   // 스크린 넓이


        halfWidth = (characterBox.size.x) / 2f;
        rightButtonSec = (screenWidth / 4) * 3;
        leftButtonSec = screenWidth / 4;
    }

    private void OnLevelWasLoaded(int level)
    {
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
    }

    // UI 위를 클릭시 이동 금지
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void Move()
    {
        float dis = targetpos.x - transform.position.x; // 마우스 좌표 - 현재 캐릭터 좌표
        if (Mathf.Abs(dis) <= error)
        {
            moveit = false;
        }
        if (dis > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (dis < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPointerOverUIObject()) return; // UI창 나오면 클릭 금지

        
        if (Input.GetMouseButtonDown(0))
        {
            targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            moveit = true;
        }
        if (moveit)
        {
            GameManager.instance.characterAction();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.characterAction += Move;
    }

    private void OnDisable()
    {
        GameManager.instance.characterAction -= Move;
    }
}
