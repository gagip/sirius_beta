using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{


    public float perspectiveZoomSpeed = 0.25f;
    public float zoomSpeed = 0.25f;

    Vector2 initMidpoint = new Vector2();
    Vector2 initScreenMidPoint = new Vector2();


    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;



            // Camera part
            float cameraScale = ((GetComponent<Camera>().orthographicSize) / ((float)Screen.height / 2));

            float cameraPositionX = GetComponent<Camera>().transform.position.x;
            float cameraPositionY = GetComponent<Camera>().transform.position.y;

            //터치 확인
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {

                // 손가락 사이 중간지점 지정 
                initMidpoint = new Vector2(((touchOne.position.x + touchZero.position.x) / 2) - Screen.width / 2,
                                               ((touchOne.position.y + touchZero.position.y) / 2) - Screen.height / 2);

                // 중간지점을 씬에서 조정
                initScreenMidPoint = new Vector2((cameraPositionX + (initMidpoint.x * cameraScale)),
                                                    (cameraPositionY + (initMidpoint.y * cameraScale)));
            }


            // 스크린에서의 손가락 사이 중간지점 불러오기
            Vector2 midpoint = new Vector2(((touchOne.position.x + touchZero.position.x) / 2) - Screen.width / 2,
                                           ((touchOne.position.y + touchZero.position.y) / 2) - Screen.height / 2);

            // 손가락 이동에 따른 카메라 줌 변화
            GetComponent<Camera>().transform.position = new Vector3(initScreenMidPoint.x - (midpoint.x * cameraScale),
                                                                        initScreenMidPoint.y - (midpoint.y * cameraScale),
                                                                        0);

            //카메라 타입에 따른 줌 변화
            if (GetComponent<Camera>().orthographic)
            {
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * zoomSpeed;

                GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);

            }
            else
            {
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }

        }
    }
}


