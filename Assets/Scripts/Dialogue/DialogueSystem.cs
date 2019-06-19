using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string txtf;
    public Text txt;
    public Image panel;
    public Image skipButton;

    private int count = 0;
    private bool isDialogue = false;

    List<Dictionary<string, object>> dialogue;

    void Start()
    {
        skipButton.gameObject.SetActive(false);
        dialogue = CSVReader.Read(txtf);
    }


    public void OnOff(bool _flag)
    {
        panel.gameObject.SetActive(_flag);
        skipButton.gameObject.SetActive(_flag);
        txt.gameObject.SetActive(_flag);
    }

    public void ShowDialogue()
    {
        OnOff(true);
        count = 0;
        isDialogue = true;
        NextDialogue();
    }

    public void HideDialogue()
    {
        OnOff(false);
        isDialogue = false;
    }
    
    private void NextDialogue()
    {
        Transform textPos = txt.transform;

        txt.text = (string)dialogue[count]["dialog"];
        if ((int) dialogue[count]["name"] == 1)
        {
            txt.color = Color.red;
            txt.fontSize = 20;
            Vector3 v1 = new Vector3(0, 0, 0);
            //마샤
            Vector3 v2 = GameObject.Find("마샤").GetComponent<Transform>().position;
            //메리
            Vector3 v3 = GameObject.Find("메리").GetComponent<Transform>().position;

            Vector3 b1 = GameObject.Find("Button").GetComponent<Transform>().position;


            v1.x = b1.x + (v3.x-v2.x)* 0.5f;
            v1.y = b1.y;

            txt.GetComponent<RectTransform>().position = v1;

            Debug.Log(v1);

        }
        if ((int)dialogue[count]["name"] == 2)
        {
            txt.color = Color.black;
            txt.fontSize = 20;
            Vector3 v1 = new Vector3(0, 0, 0);

            //버튼
            Vector3 v2 = GameObject.Find("마샤").GetComponent<Transform>().position;

            //메리
            Vector3 v3= GameObject.Find("메리").GetComponent<Transform>().position;

            Vector3 b1 = GameObject.Find("Button").GetComponent<RectTransform>().position;


            v1.x = b1.x - (v3.x - v2.x)*2;
            v1.y = b1.y;

            txt.GetComponent<RectTransform>().position = v1;

            Debug.Log(v1);
        }
        if ((int)dialogue[count]["name"] == 3)
        {
            txt.color = Color.blue;
            txt.fontSize = 15;
            Vector3 v1 = new Vector3(0, 0, 0);

            //버튼
            var direction1 = GameObject.Find("마샤").GetComponent<Transform>().position;

            //메리
            var direction2 = GameObject.Find("메리").GetComponent<Transform>().position;


            v1.x = (direction1.x - direction2.x) * 3;
            v1.y = direction1.y + 2;

            txt.GetComponent<RectTransform>().TransformVector(v1);
            txt.GetComponent<RectTransform>().position = v1;
            Debug.Log(v1);
            //  txt.GetComponent<RectTransform>().position = new Vector3(direction1.x - direction2.x, direction2.y + 4, 0);
        }
        count++;
    }

    public void updateDial()
    {
        Update();
    }
    void Update()
    {

        if (isDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                if (count < dialogue.Count)
                {
                    NextDialogue();
                }
                else
                    HideDialogue();
            }
        }
    }
}
