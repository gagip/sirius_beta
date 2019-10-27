//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
///// <summary>
///// NPC와의 대화 기능을 관리하는 스크립트
///// </summary>


//// 사용자 정의 텍스트 위치
//[System.Serializable]
//public class UserDialogueTextPosion
//{
//    public Vector3 txtPlayer = new Vector3(-450.0f, 550.0f, 0.0f); // 플레이어 쪽 텍스트 위치
//    public Vector3 txtNPC = new Vector3(450.0f, 550.0f, 0.0f);// NPC 쪽 텍스트 위치
//}

//public class DialogueSystem : MonoBehaviour
//{
//    public Controll controll;
   
//    // 텍스트 UI를 연동하기 위한 변수
//    public string txtFile; // 스크립트 파일 이름
//    private Image dialogueBox;
//    private Text txt; // 텍스트 오브젝트
//    private Image panel; // 대화 중 다른 기능 금지
//    private GameObject dialogueButton;  // 대화 버튼
//    private GameObject dialogueUI;   // 대화 UI

//    // 인게임 대화에 필요한 변수
//    private GameObject player; // 플레이어 오브젝트 
//    private float error = 8.0f; // 버튼 출력하기 위한 거리
//    [SerializeField] private int count = 0; // 텍스트 문서 단위
//    private bool talking = false; // 텍스트 UI 활성화 트리거
//    [SerializeField] bool allText = false; // 전체 문장이 출력되면 true
//    private string text;
//    Coroutine coroutine;

//    // 텍스트 위치에 필요한 변수 
//    public bool setTextPosition; // 사용자 정의 위치로 설정
//    public UserDialogueTextPosion userDialogueTextPosion; // 사용자 지정 택스트 위치
//    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
//    Vector3 txtNPC; // NPC 쪽 텍스트 위치

//    // 카메라 조작에 필요한 변수
//    //public DollyZoom cameraCtrl;

//    List<Dictionary<string, object>> dialogueData;

//    void Start()
//    {
//        // 메리가 있으면 메리 추가
//        if(GameObject.FindWithTag("Mary") != null)
//        {
//            player = GameObject.FindWithTag("Mary").gameObject;
//            controll = player.GetComponent<Controll>();
//        }
        
 
//        dialogueButton = transform.Find("Dialogue Button").gameObject;
//        dialogueData = CSVReader.Read(txtFile);
//        dialogueUI = GameObject.Find("Dialogue UI");
//        dialogueBox = dialogueUI.transform.Find("Dialogue Box").GetComponent<Image>();
//        txt = dialogueBox.transform.Find("Dialogue Text").GetComponent<Text>();
//        panel = dialogueUI.transform.Find("Dialogue Panel").GetComponent<Image>();

//        print(dialogueData.Count);
//    }

//    // Dialogue UI 활성화 스위치
//    private void OnOff(bool _flag)
//    {
//        panel.gameObject.SetActive(_flag);
//        dialogueBox.gameObject.SetActive(_flag);
//        txt.gameObject.SetActive(_flag);
//        dialogueButton.SetActive(_flag);
//    }

//    // 대화를 시작하고 Dialogue UI를 활성화한다.
//    public void ShowDialogue()
//    {
//        GameManager.instance.SetGameState(GameState.Dialogue);
//        SetDialoguePosition();
//        OnOff(true);
//        count = 0;
//        talking = true;
//        PrintText();
//    }

//    // 대화를 종료하고 Dialogue UI를 종료한다.
//    public void HideDialogue()
//    {
//        GameManager.instance.SetGameState(GameState.InGame);
//        OnOff(false);
//        talking = false;
//    }
    
//    // 다음 대화로 넘어간다.
//    private void NextDialogue()
//    {
//        //string fulltext = (string)dialogue[count]["dialog"];
//        //StartCoroutine(ShowText(fulltext));
//        count++;
//        // if (count >= dialogueData.Count) return;
//        PrintText();
//    }

//    // 이전 대화로 되돌아가는 함수
//    public void PreviousDialogue()
//    {
//        count -= 2;
//        PrintText();
//    }
    
//    // 텍스트 출력
//    private void PrintText()
//    {
//        ChangeText();
//        text = (string)dialogueData[count]["dialog"];
//        if (!allText) //
//        {
//            coroutine = StartCoroutine(ShowText(text));
//        }
//        else
//        {
//            txt.text = text;
//            print("출력이 끝났습니다");
//        }
//    }

//    // 텍스트 위치를 설정한다.
//    private void SetDialoguePosition()
//    {
//        // 사용자가 지정한 텍스트 위치로 할 시
//        if (setTextPosition)
//        {
//            txtPlayer = userDialogueTextPosion.txtPlayer;
//            txtNPC = userDialogueTextPosion.txtNPC;
//        }
//        else
//        {
//            Vector3 playerPos = player.transform.position; // Player 위치
//            Vector3 npcPos = transform.position; // NPC 위치

//            if (playerPos.x - npcPos.x < 0) // 플레이어가 왼쪽에 있을 시
//            {
//                txtPlayer = new Vector3(-450.0f, 500.0f, 0.0f);
//                txtNPC = new Vector3(450.0f, 500.0f, 0.0f);
//            }
//            else // 플레이어가 오른쪽에 있을 시
//            {
//                txtPlayer = new Vector3(450.0f, 500.0f, 0.0f);
//                txtNPC = new Vector3(450.0f, 500.0f, 0.0f);
//            }
//        }
//    }

//    // 한 글자씩 출력한다
//    IEnumerator ShowText(string fulltext)
//    {
//        for (int i = 0; i <= fulltext.Length; i++)    // 글자 하나하나씩 출력
//        {
//            string currentText = fulltext.Substring(0, i);
//            txt.text = currentText;
//            yield return new WaitForSecondsRealtime(0.05f);
//        }
//        allText = true;
//    }

//    // 캐릭터별 폰트 스타일과 텍스트 위치를 설정한다.
//    public void ChangeText()
//    {
//        if ((int)dialogueData[count]["name"] == 1) // 메리
//        {
//            txt.color = Color.black;
//            dialogueBox.GetComponent<RectTransform>().localPosition = txtPlayer;
//        }
//        if ((int)dialogueData[count]["name"] == 2) // 메들록
//        {
//            txt.color = Color.red;
//            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
//        }
//        if ((int)dialogueData[count]["name"] == 3) // 마샤
//        {
//            txt.color = Color.blue;
//            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
//        }
//    }

//    // 캐릭터가 NPC 근처에 오면 대화 버튼이 활성화된다.
//    public void ShowButton()
//    {
//        Vector3 playerPos = player.transform.position; // Player 위치
//        Vector3 npcPos = transform.position; // NPC 위치

//        if (Mathf.Abs(playerPos.x - npcPos.x) < error)
//        {
//            dialogueButton.SetActive(true);
//        }
//        else
//        {
//            dialogueButton.SetActive(false);
//        }
//    }

//    public void Talk()
//    {
//        dialogueButton.SetActive(false);
//        if (Input.GetMouseButtonUp(0))  // 출력완료한 상태에서 버튼 클릭
//        {
//            if (count < dialogueData.Count - 1)
//            {
//                if (allText == true) // 한글자씩 출력을 완료
//                {
//                    allText = false;
//                    NextDialogue();
//                }
//                else
//                {
//                    StopCoroutine(coroutine);
//                    allText = true;
//                    PrintText();
//                }
//            }
//            else // 대화 끝날 시
//                HideDialogue();
//        }
//    }

//    void Update()
//    {
//        if (talking) // 대화중
//        {
//            Talk();
//        }
//        else
//        {
//            ShowButton();
//        }
//    }
//}
