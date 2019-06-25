using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCamera : MonoBehaviour
{
    //Vector3 v1 = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        //v1 = Camera.main.GetComponent<RectTransform>().position;
        Canvas canvas = gameObject.GetComponent<Canvas>();
        //GameObject.Find("dial").GetComponent<Canvas>();
        //GameObject myCamera = GameObject.Find("mainCamera");
        //canvas.worldCamera = myCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}