using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{

    public string sceneName = "TestNPC";
    public void changeFirstScene()
    {
        SceneManager.LoadScene(sceneName);
        Camera.main.GetComponent<Status>().teleportTimes++;
    }
    public void changeSecondScene()
    {
        SceneManager.LoadScene("TestNPC");
        Camera.main.GetComponent<Status>().teleportTimes++;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
