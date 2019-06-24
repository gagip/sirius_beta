using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{

    public string activeTag;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody r = other.gameObject.GetComponent<Rigidbody>();

        Vector3 direction = transform.position - other.gameObject.transform.position;
        direction.Normalize();
        //nomarlize 을 벡터에 적용하면 크기를 1로 정규화.

        if (other.gameObject.tag == activeTag)
        {
            r.velocity *= 0.9f;
            r.AddForce(direction * r.mass * 20.0f);
        }
        else
        {
            r.AddForce(-direction * r.mass * 80.0f);
        }
    }

    //접촉감지 
    bool fallIn;

    public bool isFallIn()
    {
        return fallIn;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == activeTag)
        {
            fallIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == activeTag)
        {
            fallIn = false;
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
