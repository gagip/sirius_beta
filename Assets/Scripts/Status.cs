using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    public int MarsaChatTimes;
    public int BenChatTimes;
    public int teleportTimes;

    // Start is called before the first frame update
    void Start()
    {
        MarsaChatTimes = 0;
        BenChatTimes = 0;
        teleportTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (BenChatTimes > 3)
        {
            Debug.Log(MarsaChatTimes);
            Debug.Log(BenChatTimes);
            Debug.Log(teleportTimes);
        }
        else
        {

            Debug.Log(teleportTimes);
        }
    }
}
