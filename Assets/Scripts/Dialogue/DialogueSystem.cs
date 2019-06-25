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
    public GameObject dialogueButton;  // 대화 버튼
    [SerializeField] private float delay = 0.01f;
    [SerializeField] private float error = 8.0f;

    private GameObject player; // 플레이어 오브젝트 
    private int count = 0; // 텍스트 문서 단위
    private bool isDialogue = false; // 텍스트 UI 활성화 트리거

<<<<<<< HEAD
    Vector3 txtPlayer = new Vector3(); // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC = new Vector3(); // NPC 쪽 텍스트 위치
    Vector3 cameraView = new Vector3();
    public float delay = 0.01f;
    string fulltext;

=======
    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC; // NPC 쪽 텍스트 위치
>>>>>>> dd24f380ab19d778a01c6c53bbcea8a8b78a4538

    List<Dictionary<string, object>> dialogue;

    void Start()
    {
        if(GameObject.FindWithTag("Mary") != null)
        {
            player = GameObject.FindWithTag("Mary").gameObject;
        }
        dialogue = CSVReader.Read(txtf);
    }

    private void OnOff(bool _flag)
    {
        panel.gameObject.SetActive(_flag);
        skipButton.gameObject.SetActive(_flag);
        txt.gameObject.SetActive(_flag);
        dialogueButton.SetActive(_flag);
    }

    public void ShowDialogue()
    {
        SetDialoguePosition();
        OnOff(true);
        count = 0;
        NextDialogue();
        isDialogue = true;
    }

    public void HideDialogue()
    {
        OnOff(false);
        isDialogue = false;
    }





    private void NextDialogue()
    {
<<<<<<< HEAD
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
            txt.color = Color.blue;
            txt.fontSize = 14;
            txt.GetComponent<RectTransform>().position = txtNPC;
            print(txt.GetComponent<RectTransform>().position);
        }
        StartCoroutine(ShowText());
=======
        string fulltext = (string)dialogue[count]["dialog"];

        ChangeText();

        StartCoroutine(ShowText(fulltext));
>>>>>>> dd24f380ab19d778a01c6c53bbcea8a8b78a4538
        count++;
    }

    private void SetDialoguePosition()
    {
<<<<<<< HEAD

        cameraView = Camera.main.GetComponent<Transform>().position;
        Vector3 v1 = cameraView;
        Vector3 v2 = GameObject.FindWithTag("Mary").GetComponent<Transform>().position;
        Vector3 v3 = GameObject.FindWithTag("Mary").GetComponent<Transform>().position;
        
        if (v2.x - v3.x < 0) // 플레이어가 왼쪽에 있을 시
        {
            txtPlayer = new Vector3(v2.x - 3f, v2.y);
            txtNPC = new Vector3(v3.x + 3f, v2.y + 4f);
        }
        else
        {
            txtPlayer = new Vector3(v2.x + 3f, v2.y);
            txtNPC = new Vector3(v3.x - 3f, v2.y + 4f);
=======
        Vector3 playerPos = player.transform.position; // Player 위치
        Vector3 npcPos = transform.position; // NPC 위치

        if (playerPos.x - npcPos.x < 0) // 플레이어가 왼쪽에 있을 시
        {
            txtPlayer = new Vector3(playerPos.x - 3f, playerPos.y + 12f);
            txtNPC = new Vector3(npcPos.x + 3f, playerPos.y + 12f);
        }
        else
        {
            txtPlayer = new Vector3(playerPos.x + 3f, playerPos.y + 12f);
            txtNPC = new Vector3(npcPos.x - 3f, playerPos.y + 12f);
>>>>>>> dd24f380ab19d778a01c6c53bbcea8a8b78a4538
        }
    }

    IEnumerator ShowText(string fulltext)
    {
        for (int i = 0; i <= fulltext.Length; i++)    // 글자 하나하나씩 출력
        {
            string currentText = fulltext.Substring(0, i);
            txt.text = currentText;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

   private void ChangeText()
    {
        if ((int)dialogue[count]["name"] == 1) // 메리
        {
            txt.color = Color.red;
            txt.GetComponent<RectTransform>().position = txtPlayer;
        }
        if ((int)dialogue[count]["name"] == 2) // 메들록
        {
            txt.color = Color.black;
            txt.GetComponent<RectTransform>().position = txtNPC;
        }
    }

   public void ShowButton()
    {
        Vector3 playerPos = player.transform.position; // Player 위치
        Vector3 npcPos = transform.position; // NPC 위치

        if (Mathf.Abs(playerPos.x - npcPos.x) < error)
        {
            dialogueButton.SetActive(true);
        }
        else
        {
            dialogueButton.SetActive(false);
        }
    }

   void Update()
    {
        
        if (isDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
<<<<<<< HEAD
                //cameraView = Camera.main.GetComponent<Transform>().position;
=======
>>>>>>> dd24f380ab19d778a01c6c53bbcea8a8b78a4538
                if (count < dialogue.Count)
                {
                    NextDialogue();
                    print(count);
                }
                else
                    HideDialogue();
            }
        }
        else
        {
            ShowButton();
        }
    }
}
