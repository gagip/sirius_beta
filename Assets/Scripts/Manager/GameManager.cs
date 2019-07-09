using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 게임을 관리해주는 스크립트로써
/// 게임의 상태변화를 관리한다.
/// </summary>

// 게임 상태변화에 대한 리스트
[System.Serializable]
public enum GameState
{
    Menu,
    InGame,
    Dialogue
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState = GameState.Menu; // 현 게임 상태
    public List<string> accessibleScene;

    // 참조할 오브젝트를 가지고 온다
    [SerializeField] private GameObject player;

    // 참조할 스크립트를 가지고 온다

    // 참조할 데이터셋을 가지고 온다


    // 델리게이트
    public Action characterAction; // 캐릭터 행동들을 넣은 함수

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
    }

    private void Start()
    {
        if (GameObject.FindWithTag("Mary") != null) player = GameObject.FindWithTag("Mary");

        SetGameState(GameState.InGame);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (GameObject.FindWithTag("Mary") != null) player = GameObject.FindWithTag("Mary");

        SetGameState(GameState.InGame);
    }

    // 게임 상태변화를 바꾼다
    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        // 다음은 게임 상태에 따른 행동을 정의한다.
        if (currentGameState == GameState.InGame) // 인게임 상태
        {
            // 캐릭터의 행동을 개시
            if (player != null) player.GetComponent<Controll>().enabled = true;
        }
        else if (currentGameState == GameState.Dialogue) // 대화 중일 시
        {
            // 캐릭터의 행동을 멈춘다.
            if (player != null) player.GetComponent<Controll>().enabled = false;
        }
        else
        {
            // 캐릭터의 행동을 멈춘다
            if (player != null) player.GetComponent<Controll>().enabled = false;
        }
    }
}
