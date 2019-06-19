﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class Controll : MonoBehaviour
{
    public float speed; // 캐릭터 이동 속도
    public float error; // 캐릭터와 마우스 좌표와의 오차 범위

    private Vector3 targetpos; // 마우스 좌표
    private bool moveit = false; // 이동 가능 여부
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            moveit = true;
        }
        if (moveit)
        {
            float dis = targetpos.x - transform.position.x; // 마우스 좌표 - 현재 캐릭터 좌표
            if (Mathf.Abs(dis) <= error)
            {
                moveit = false;
            }
            if (dis > 0)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (dis < 0)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}