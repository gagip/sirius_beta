using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject box;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Mary")
        {
            box.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ben")
        {
            box.SetActive(false);
            Camera.main.GetComponent<Status>().BenChatTimes++;
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
