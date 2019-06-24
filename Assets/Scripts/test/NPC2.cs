using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
object와 mary가 상호작용 가능한 거리에 있을 때 상호작용 박스 생성
*/
public class NPC2 : MonoBehaviour
{
    public GameObject box;
    private void OnTriggerStay2D(Collider2D col)    // mary가 상호작용 가능 범위 일 때
    {
        if (col.tag == "Mary")
        {
            box.SetActive(true);
        }
     
    }

    private void OnTriggerExit2D(Collider2D col)    // mary가 상호작용 가능 범위 벗어날 때
    {
        if (col.tag == "Mary")
        {
            box.SetActive(false);
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
