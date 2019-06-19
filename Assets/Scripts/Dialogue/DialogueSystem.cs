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
            txt.fontSize = 30;
            textPos.localPosition = new Vector3(-5, 0, 0);  
        }
        if ((int)dialogue[count]["name"] == 2)
        {
            txt.color = Color.green;
            txt.fontSize = 30;
            textPos.localPosition = new Vector3(-10, 0, 0);
        }
        if ((int)dialogue[count]["name"] == 3)
        {
            txt.color = Color.white;
            txt.fontSize = 30;
            textPos.localPosition = new Vector3(-10, 0, 0);
        }
        count++;
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
