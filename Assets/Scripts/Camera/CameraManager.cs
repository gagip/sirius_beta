using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟을 따라가는 카메라 스크립트
/// </summary>
public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상.

    private Vector3 targetPosition; // 대상의 현재 위치 
    private Vector3 minBound;
    private Vector3 maxBound;

    public float moveSpeed; // 카메라 속도
    private float halfWidth;
    private float halfHeight;

    public BoxCollider2D boundBox; // 카메라 활동 영역

    private Camera getCamera;

    

    public void SetBound(BoxCollider2D newBound)
    {
        boundBox = newBound;
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;
    }

    private void Awake()
    {
        // 도플갱어 삭제
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Mary");
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();

        getCamera = GetComponent<Camera>();
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;
        halfHeight = getCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void OnLevelWasLoaded(int level)
    {
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
        target = GameObject.FindGameObjectWithTag("Mary");

        getCamera = GetComponent<Camera>();
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;
        halfHeight = getCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        // 타겟 따라가기
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);       // clap() 함수로 영역 제한
        }
    }
}