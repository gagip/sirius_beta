using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string txtf; // 스크립트 파일 이름
    public Text txt; // 텍스트 오브젝트
    public Image panel; // 대화 중 다른 기능 금지
    public Image skipButton; // 스킵 버튼

    private int count = 0; // 텍스트 문서 단위
    private bool isDialogue = false; // 텍스트 UI 활성화 트리거

    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC; // NPC 쪽 텍스트 위치

    public float delay = 0.01f;
    string fulltext;


    List<Dictionary<string, object>> dialogue;

    void Start()
    {
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
        SetDialoguePosition(gameObject.tag);
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
        fulltext = (string)dialogue[count]["dialog"];
        
        if ((int) dialogue[count]["name"] == 1) // 메리
        {
            txt.color = Color.red;
            txt.fontSize = 14;
            txt.GetComponent<RectTransform>().position = txtPlayer;
            print(txt.GetComponent<RectTransform>().position);
        }
        if ((int)dialogue[count]["name"] == 2)
        {
            txt.color = Color.black;
            txt.fontSize = 14;
            txt.GetComponent<RectTransform>().position = txtNPC;
            print(txt.GetComponent<RectTransform>().position);
        }
        StartCoroutine(ShowText());
        count++;
    }

    public void SetDialoguePosition(string npcName)
    {
        Vector3 v1 = new Vector3(0, 0);
        Vector3 v2 = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        Vector3 v3 = GameObject.FindWithTag(npcName).GetComponent<Transform>().position;
        
        if (v2.x - v3.x < 0) // 플레이어가 왼쪽에 있을 시
        {
            txtPlayer = new Vector3(v2.x - 3f, v2.y + 4f);
            txtNPC = new Vector3(v3.x + 3f, v2.y + 4f);
        }
        else
        {
            txtPlayer = new Vector3(v2.x + 3f, v2.y + 4f);
            txtNPC = new Vector3(v3.x - 3f, v2.y + 4f);
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fulltext.Length; i+=3)    // 글자 하나하나씩 출력
        {
            string currentText = fulltext.Substring(0, i);
            txt.text = currentText;
            yield return new WaitForSecondsRealtime(delay);
        }
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
