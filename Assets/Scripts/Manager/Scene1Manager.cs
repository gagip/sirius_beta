using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene1 의 인트로를 관리하는 스크립트
/// 이 스크립트에선 각 씬의 배치, 대화집을 관리한다.
/// </summary>

[System.Serializable]
public class Scene1DataBase
{
    public string dataName; // 해당 데이터의 이름
    public string nextSceneName;
    public string textFile;
}

public class Scene1Manager : MonoBehaviour
{
    private static Scene1Manager instance;

    public Scene1DataBase[] scene1DB;
    public int process; // 진행사항 체크
    public bool showDialogue = false;
    private EventDialogueSystem eventDialogueSystem;
    int clickCount = 0;
    

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        process = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        eventDialogueSystem = FindObjectOfType<EventDialogueSystem>();
    }

    private void OnLevelWasLoaded(int level)
    {
        eventDialogueSystem = FindObjectOfType<EventDialogueSystem>();
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (!showDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                showDialogue = true;
                eventDialogueSystem.ShowDialogue();
            }
        }

        if (eventDialogueSystem.count == eventDialogueSystem.dialogueData.Count && !eventDialogueSystem.talking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // count를 넣는 이유는 대화 종료 후, 한 번 더 클릭하면 다음 씬으로 갈 수 있게 임시방편
                clickCount++;
                if (clickCount >= 1)
                {
                    // 만일 인트로가 끝나가면 Scene1Manager도 삭제한다. 
                    if (process == scene1DB.Length-1) 
                    {
                        SceneChange(scene1DB[process].nextSceneName);
                        Destroy(gameObject);
                    }
                    else
                    {
                        SceneChange(scene1DB[process].nextSceneName);
                        showDialogue = false;
                        clickCount = 0;
                        process++;
                    }
                }
            }
        }
    }
}
