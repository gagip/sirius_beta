using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{

    public Collider2D upStair;
    public Collider2D upStairTrigger;
    public Collider2D downStairTrigger;
    public float stairSpeed;

    private void Start()
    {
        upStair.gameObject.SetActive(false);
        upStairTrigger.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "RightStairs")
        {
                if (this.GetComponent<PaTiKControll>().rightButtonOn)
                {
                    
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    GetComponent<Rigidbody2D>().AddForce(Vector3.right * stairSpeed);
                }
                    
                else if (this.GetComponent<PaTiKControll>().leftButtonOn)
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    GetComponent<Rigidbody2D>().AddForce(Vector3.down * stairSpeed/2);
                }

                else 
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }  
        }

        else if (other.gameObject.tag == "LeftStairs")

        {
            if (this.GetComponent<PaTiKControll>().rightButtonOn)
            {

                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().AddForce(Vector3.down * stairSpeed/2);
            }

            else if (this.GetComponent<PaTiKControll>().leftButtonOn)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().AddForce(Vector3.left * stairSpeed);
            }

            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "UpOrDown")
        {
            if (this.GetComponent<PaTiKControll>().upperButtonOn)
            {
                upStair.gameObject.SetActive(true);
                upStairTrigger.gameObject.SetActive(true);
                downStairTrigger.gameObject.SetActive(false);
            }
            else
            {
                upStair.gameObject.SetActive(false);
                upStairTrigger.gameObject.SetActive(false);
                downStairTrigger.gameObject.SetActive(true);
            }
        }

    }

}