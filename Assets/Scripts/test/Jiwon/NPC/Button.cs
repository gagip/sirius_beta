using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameObject npc;

    // Start is called before the first frame update
    void Start()
    {
        npc = transform.root.gameObject;
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
       npc.GetComponent<DialogueSystem>().ShowDialogue();
        GameManager.instance.accessibleScene.Add("Hall");
        //gameObject.SetActive(false);
    }
    
}
