using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 대화 이외의 대사를 출력할 때 사용하는 스크립트
/// 인트로 씬 위주로 사용
/// </summary>

[System.Serializable]
public class EventDialogueTextPosion
{
    public Vector3 txtPlayer = new Vector3(-150.0f, 200.0f, 0.0f); // 플레이어 쪽 텍스트 위치
    public Vector3 txtNPC = new Vector3(150.0f, 200.0f, 0.0f);// NPC 쪽 텍스트 위치
}


public class EventDialogueSystem : MonoBehaviour
{

    // 텍스트 UI를 연동하기 위한 변수
    public Scene1DataBase scene1DB; // scene1에 필요한 데이터 추출
    private Image dialogueBox;
    private Text txt; // 텍스트 오브젝트
    [SerializeField] private Image panel; // 대화 중 다른 기능 금지
    [SerializeField] private GameObject dialogueUI;   // 대화 UI

    // 인게임 대화에 필요한 변수
    public int count = 0; // 텍스트 문서 단위
    public bool talking = false; // 텍스트 UI 활성화 트리거

    // 텍스트 위치에 필요한 변수 
    public bool setTextPosition; // 사용자 정의 위치로 설정
    public EventDialogueTextPosion eventDialogueTextPosion;
    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC; // NPC 쪽 텍스트 위치
    
    public List<Dictionary<string, object>> dialogueData;

    public Scene1Manager scene1Manager;

    void Start()
    {
        scene1Manager = FindObjectOfType<Scene1Manager>();
        scene1DB = scene1Manager.scene1DB[scene1Manager.process];
        dialogueData = CSVReader.Read(scene1DB.textFile);
        

        dialogueUI = GameObject.Find("Dialogue UI");
        dialogueBox = dialogueUI.transform.Find("Dialogue Box").GetComponent<Image>();
        txt = dialogueBox.transform.Find("Dialogue Text").GetComponent<Text>();
        panel = dialogueUI.transform.Find("Dialogue Panel").GetComponent<Image>();
    }

    private void OnOff(bool _flag)
    {
        //panel.gameObject.SetActive(_flag);
        dialogueBox.gameObject.SetActive(_flag);
        txt.gameObject.SetActive(_flag);
    }

    public void ShowDialogue()
    {
        SetDialoguePosition();
        OnOff(true);
        count = 0;
        talking = true;
    }

    public void HideDialogue()
    {
        OnOff(false);
        talking = false;
    }

    private void NextDialogue()
    {
        //string fulltext = (string)dialogue[count]["dialog"];

        ChangeText();
        txt.text = (string)dialogueData[count]["dialog"];
        //StartCoroutine(ShowText(fulltext));
        count++;
    }

    private void SetDialoguePosition()
    {
        txtPlayer = eventDialogueTextPosion.txtPlayer;
        txtNPC = eventDialogueTextPosion.txtNPC;
    }

    //IEnumerator ShowText(string fulltext)
    //{
    //    for (int i = 0; i <= fulltext.Length; i++)    // 글자 하나하나씩 출력
    //    {
    //        string currentText = fulltext.Substring(0, i);
    //        txt.text = currentText;
    //        yield return new WaitForSecondsRealtime(delay);
    //    }
    //}

    public void ChangeText()
    {
        if ((int)dialogueData[count]["name"] == 1) // 메리
        {
            txt.color = Color.black;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtPlayer;
        }
        if ((int)dialogueData[count]["name"] == 2) // 메들록
        {
            txt.color = Color.red;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
        }
        if ((int)dialogueData[count]["name"] == 3) // 마샤
        {
            txt.color = Color.blue;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
        }
    }

    void Update()
    {

        if (talking) // 대화중
        {
            //cameraCtrl.dollyZoomIn = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (count < dialogueData.Count)
                {
                    NextDialogue();
                    print(count);
                }
                else
                    HideDialogue();
            }
        }
    }
}
